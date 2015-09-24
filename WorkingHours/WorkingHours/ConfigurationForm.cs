using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WorkingHours
{
    public partial class ConfigurationForm : Form
    {
        public ConfigurationForm()
        {
            InitializeComponent();
        }

        public void ShowDialog( IWin32Window owner, WorkingHoursConfiguration config )
        {
            b_work_bar_color.BackColor              = config.work_bar_color;
            b_unlocked_bar_color.BackColor          = config.unlocked_bar_color;
            b_latest_work_bar_color.BackColor       = config.latest_work_bar_color;
            b_latest_unlocked_bar_color.BackColor   = config.latest_unlocked_bar_color;
            c_zoom_amount.Value                     = config.zoom_amount;
            c_time_start.Value                      = config.time_start;
            c_time_end.Value                        = config.time_end;
            c_unlocked_hours.Value                  = Convert.ToDecimal( config.unlocked_hours );
            c_work_hours.Value                      = Convert.ToDecimal( config.work_hours );
            cb_show_detailed_weeks.Checked          = config.show_detailed_weeks;
            cb_show_work_hours.Checked              = config.show_work_hours;
            cb_show_unlocked_hours.Checked          = config.show_unlocked_hours;
            cb_full_background_lines.Checked        = config.full_background_lines;
            dtp_work_start.Value                    = DateTime.Today.Add( config.work_start );
            dtp_work_end.Value                      = DateTime.Today.Add( config.work_end );
            cb_log_program_start.Checked            = config.log_program_start;
            cb_log_program_shutdown.Checked         = config.log_program_shutdown;

            if ( ShowDialog( owner ) == DialogResult.OK )
            {
                config.work_bar_color               = b_work_bar_color.BackColor;
                config.unlocked_bar_color           = b_unlocked_bar_color.BackColor;
                config.latest_work_bar_color        = b_latest_work_bar_color.BackColor;
                config.latest_unlocked_bar_color    = b_latest_unlocked_bar_color.BackColor;
                config.zoom_amount                  = Convert.ToInt32( c_zoom_amount.Value );
                config.time_start                   = Convert.ToInt32( c_time_start.Value );
                config.time_end                     = Convert.ToInt32( c_time_end.Value );
                config.unlocked_hours               = Convert.ToDouble( c_unlocked_hours.Value );
                config.work_hours                   = Convert.ToDouble( c_work_hours.Value );
                config.show_detailed_weeks          = cb_show_detailed_weeks.Checked;
                config.show_work_hours              = cb_show_work_hours.Checked;
                config.show_unlocked_hours          = cb_show_unlocked_hours.Checked;
                config.full_background_lines        = cb_full_background_lines.Checked;
                config.work_start                   = dtp_work_start.Value.Subtract( dtp_work_start.Value.Subtract( dtp_work_start.Value.TimeOfDay ) );
                config.work_end                     = dtp_work_end.Value.Subtract( dtp_work_end.Value.Subtract( dtp_work_end.Value.TimeOfDay ) );
                config.log_program_start            = cb_log_program_start.Checked;
                config.log_program_shutdown         = cb_log_program_shutdown.Checked;
            }
        }

        private void b_bar_color_Click( object sender, EventArgs e )
        {
            ColorDialog color_dialog = new ColorDialog();
            if ( color_dialog.ShowDialog( this ) == System.Windows.Forms.DialogResult.OK )
            {
                ((Button)sender).BackColor = color_dialog.Color;
            }
        }

        private void b_process_eventlog_Click( object sender, EventArgs e )
        {
            /* Launch new process with elevated privileges, and wait for events gathered by it to be transferred via WCF*/

            /*ProcessStartInfo process_info = new ProcessStartInfo( "EventExtractor" );
            process_info.Verb = "runas";
            Process.Start( process_info );*/
        }
    }
}
