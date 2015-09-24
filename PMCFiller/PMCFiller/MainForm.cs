using System;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace PMCFiller
{
    public partial class MainForm : Form
    {
        enum ApplicationState { PreLogon, LoggedOn, OnTimeJournal, Finished };

        public class Config
        {
            public string           User                    = "";
            [XmlIgnore]
            public string           Password                = "";
            [XmlElement( "Password" )]
            public string EncodedPassword
            {
                get
                {
                    return Crypto.EncryptStringAES( Password, User );
                }
                set
                {
                    Password = Crypto.DecryptStringAES( value, User );
                }
            }
            public string           Url                     = "https://pmcbud.epam.com/pmc/";
            public bool             SaveCredentials         = true;
            public int              AppCloseTimeout         = 10000;
        }
        private Config              config                  = new Config();

        private string              config_file_name        = "config.xml";
        private ApplicationState    application_state       = ApplicationState.PreLogon;
        private CredentialsForm     credentials_form        = new CredentialsForm();
        private Timer               app_close_timer         = new Timer();

        public MainForm()
        {
            InitializeComponent();

            Utils.Deserialize( ref config, config_file_name );

            if ( config.User.Length == 0 || config.Password.Length == 0 )
            {
                credentials_form.ShowDialog( this, ref config );
            }
            app_close_timer.Interval = config.AppCloseTimeout;
            app_close_timer.Tick += new EventHandler( app_close_timer_Tick );

            browser.Navigate( config.Url );
        }

        private void app_close_timer_Tick( object sender, EventArgs e )
        {
            Close();
        }

        private void browser_DocumentCompleted( object sender, WebBrowserDocumentCompletedEventArgs e )
        {
            if ( browser.ReadyState != WebBrowserReadyState.Complete ) return;

            if ( application_state == ApplicationState.PreLogon )
            {
                browser.Document.GetElementById( "username" ).InnerText = config.User;
                browser.Document.GetElementById( "password" ).InnerText = config.Password;
                browser.Document.GetElementByName( "Login" ).InvokeMember( "Click" );
                application_state = ApplicationState.LoggedOn;
            }
            else if ( application_state == ApplicationState.LoggedOn )
            {
                if ( !browser.Document.Body.InnerText.Contains( "Logged in as" ) )
                {
                    MessageBox.Show( "Couldn't find \"Logged in as\" on page. Login was probably incorrect!" );
                    return;
                }

                if ( browser.Document.Body.InnerText.Contains( "Time Journal for" ) )
                {
                    application_state = ApplicationState.OnTimeJournal;
                    browser_DocumentCompleted( sender, e );
                }
                else
                {
                    browser.Navigate( "https://pmcbud.epam.com/pmc/timejournal/list.do" );
                }
            }
            else if ( application_state == ApplicationState.OnTimeJournal )
            {
                string cell_name = "formTimeJournal" + ( Convert.ToInt32( DateTime.Now.DayOfWeek ) + 1 ).ToString();
                HtmlElement current_cell = browser.Document.GetElementByName( cell_name );
                if ( current_cell != null )
                {
                    current_cell.InnerText = "8";
                    current_cell.InvokeMember( "onChange" );
                    foreach ( HtmlElement element in browser.Document.GetElementsByTagName( "input" ) )
                    {
                        if ( element.OuterHtml.Contains( "Save Changes" ) )
                        {
                            element.InvokeMember( "Click" );
                            application_state = ApplicationState.Finished;
                            app_close_timer.Start();
                            break;
                        }
                    }
                }
            }
        }

        private void quitToolStripMenuItem_Click( object sender, EventArgs e )
        {
            Close();
        }

        private void MainForm_FormClosing( object sender, FormClosingEventArgs e )
        {
            if ( config.SaveCredentials == false )
            {
                config.User     = "";
                config.Password = "";
            }
            Utils.Serialize( config, config_file_name );
        }
    }
}
