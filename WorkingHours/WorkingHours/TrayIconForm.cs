using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Win32;
using Shared;

namespace WorkingHours
{
    public partial class TrayIconForm : Form
    {
        const string                config_file_name    = "config.xml";
        const string                events_file_name    = "events.xml";
        WorkingHoursConfiguration   configuration       = new WorkingHoursConfiguration();
        List< EventEntry >          events              = new List< EventEntry >();
        ConfigurationForm           form_configuration  = null;
        HoursForm                   form_hours          = null;

        public TrayIconForm()
        {
            InitializeComponent();

            SystemEvents.SessionSwitch += new SessionSwitchEventHandler( SessionSwitch );

            Utils.Deserialize( ref configuration, config_file_name );
            Utils.Deserialize( ref events, events_file_name );
            if ( configuration.log_program_start )
            {
                events.Add( new EventEntry( DateTime.Now, SessionSwitchReason.SessionLogon ) );
            }
        }

        private void TrayIconForm_FormClosing( object sender, FormClosingEventArgs e )
        {
            if ( configuration.log_program_shutdown )
            {
                events.Add( new EventEntry( DateTime.Now, SessionSwitchReason.SessionLogoff ) );
            }
            SystemEvents.SessionSwitch -= new SessionSwitchEventHandler( SessionSwitch );

            Utils.Serialize( configuration, config_file_name );
            Utils.Serialize( events, events_file_name );
        }

        private void quitToolStripMenuItem_Click( object sender, EventArgs e )
        {
            Close();
        }

        private void configureToolStripMenuItem_Click( object sender, EventArgs e )
        {
            if ( form_configuration == null )
            {
                form_configuration = new ConfigurationForm();
                form_configuration.ShowDialog( this, configuration );
                form_configuration = null;
            }
            else
            {
                form_configuration.Activate();
            }
        }

        private void showToolStripMenuItem_Click( object sender, EventArgs e )
        {
            if ( form_hours == null )
            {
                Activate();
                form_hours = new HoursForm();
                form_hours.ShowDialog( this, configuration, events );
                form_hours = null;
                GC.Collect(); // the hoursform creates images, possibly consuming many megabytes of memory...
            }
            else
            {
                form_hours.Activate();
            }
        }

        private void SessionSwitch( object sender, SessionSwitchEventArgs e )
        {
            bool refresh = false;
            switch ( e.Reason )
            {
                case SessionSwitchReason.SessionLogon:
                case SessionSwitchReason.SessionUnlock:
                    events.Add( new EventEntry( DateTime.Now, SessionSwitchReason.SessionLogon ) );
                    refresh = true;
                    break;
                case SessionSwitchReason.SessionLogoff:
                case SessionSwitchReason.SessionLock:
                    events.Add( new EventEntry( DateTime.Now, SessionSwitchReason.SessionLogoff ) );
                    refresh = true;
                    break;
            }
            if ( refresh )
            {
                Utils.Serialize( events, events_file_name );
            }
        }

        private void TrayIconForm_Activated( object sender, EventArgs e )
        {
            // it would otherwise be possible to raise the dummy window via doubleclicking on the icon,
            // closing the pop-up window, then pressing enter
            Hide();
        }

    }
}
