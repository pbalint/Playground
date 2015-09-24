using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace HTTPStressTest
{
    public partial class MainForm : Form
    {
        private static string   config_file_name    = "config.xml";
        private Config          config              = new Config();
        private bool            test_running        = false;

        private int             ThreadCount         { get { return Convert.ToInt32( c_threads.Value ); } }
        List< bool >            thread_running      = new List< bool >();

        public MainForm()
        {
            InitializeComponent();
            config.UseDefaults();
            Utils.Deserialize< Config >( ref config, config_file_name );
            cb_show_response.Checked = config.ShowResponse;
        }

        private void MainForm_FormClosed( object sender, FormClosedEventArgs e )
        {
            Utils.Serialize< Config >( config, config_file_name );
        }

        private void c_threads_ValueChanged( object sender, EventArgs e )
        {
            while ( ThreadCount > tc_test_container.TabPages.Count )
            {
                TestUI new_tab = new TestUI( config );
                tc_test_container.TabPages.Add( new_tab );
                if ( test_running )
                {
                    new_tab.Start();
                }
            }
            while ( ThreadCount < tc_test_container.TabPages.Count )
            {
                TestUI page = (TestUI)tc_test_container.TabPages[ tc_test_container.TabPages.Count - 1 ];
                page.Stop();
                tc_test_container.TabPages.Remove( page );
            }
        }

        private void b_start_stop_Click( object sender, EventArgs e )
        {
            if ( test_running )
            {
                b_start_stop.Enabled = false;
                foreach ( TestUI page in tc_test_container.TabPages )
                {
                    page.Stop();
                }
                b_start_stop.Text   = "Start";
                b_start_stop.Enabled = true;
            }
            else
            {
                b_start_stop.Enabled = false;
                foreach ( TestUI page in tc_test_container.TabPages )
                {
                    page.Start();
                }
                b_start_stop.Text   = "Stop";
                b_start_stop.Enabled = true;
            }
            test_running = !test_running;
        }

        private void cb_show_response_CheckedChanged( object sender, EventArgs e )
        {
            config.ShowResponse = cb_show_response.Checked;
        }
    }
}
