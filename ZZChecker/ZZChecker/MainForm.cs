using System;
using System.IO;
using System.Windows.Forms;

namespace ZZChecker
{
    public partial class MainForm : Form
    {
        enum ApplicationState { PreLogon, Auth, Query };

        private Timer               timer                   = new Timer();

        public class Config
        {
            public string           User                    = "";
            public string           Password                = "";
            public string           Url                     = "zz.hu";
            public int              RefreshTime             = timer_interval_one_day;
            public bool             OneShot                 = true;
            public bool             SaveCredentials         = true;
        }
        private Config              config                  = new Config();

        private string              config_file_name        = "config.xml";
        private ApplicationState    application_state       = ApplicationState.PreLogon;
        private const int           timer_interval_one_day  = 24*60*60*1000;
        private CredentialsForm     credentials_form        = new CredentialsForm();

        public MainForm()
        {
            InitializeComponent();

            Utils.Deserialize( ref config, config_file_name );

            if ( config.User.Length == 0 || config.Password.Length == 0 )
            {
                credentials_form.ShowDialog( this, ref config );
            }

            tray_icon.Visible   = true;
            timer.Interval      = config.RefreshTime;
            timer.Tick          += new EventHandler( timer_Tick );

            timer.Start();
            browser.Navigate( config.Url );
        }

        void timer_Tick( object sender, EventArgs e )
        {
            application_state = ApplicationState.PreLogon;
            browser.Navigate( "zz.hu" );
        }

        private void browser_DocumentCompleted( object sender, WebBrowserDocumentCompletedEventArgs e )
        {
            if ( application_state == ApplicationState.PreLogon )
            {
                browser.Document.GetElementByName( "azon" ).InnerText = config.User;
                browser.Document.GetElementByName( "pswd" ).InnerText = config.Password;
                browser.Document.GetElementsByTagName( "button" )[0].InvokeMember( "Click" );
                application_state = ApplicationState.Auth;
            }
            else if ( application_state == ApplicationState.Auth )
            {
                if ( browser.Document.Body.InnerText.Contains( "Pámer Bálint" ) )
                {
                    application_state = ApplicationState.Query;
                    browser.Navigate( "http://zz.hu/web/online.php?menu=egyenleg" );
                }
            }
            else if ( application_state == ApplicationState.Query )
            {
                using ( StreamWriter writer = new StreamWriter( "egyenleg_" + DateTime.Now.ToString( "yyyy.MM.dd___HH_mm_ss" ) + ".html" ) )
                {
                    writer.Write( browser.Document.Body.InnerHtml );
                    if ( config.OneShot )
                    {
                        Close();
                    }
                }
                //browser.Document.InvokeScript( "down", new object[] { 1 } );
            }
        }

        private void quitToolStripMenuItem_Click( object sender, EventArgs e )
        {
            Close();
        }

        private void tray_icon_MouseDoubleClick( object sender, MouseEventArgs e )
        {
            Show();
            WindowState = FormWindowState.Normal;
            ShowInTaskbar = true;
            Activate();
        }

        private void Form1_Resize( object sender, EventArgs e )
        {
            if ( WindowState == FormWindowState.Minimized )
            {
                Hide();
                ShowInTaskbar = false;
            }
        }

        private void credentialsToolStripMenuItem_Click( object sender, EventArgs e )
        {
            credentials_form.ShowDialog( this, ref config );
            timer.Interval = config.RefreshTime;
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
