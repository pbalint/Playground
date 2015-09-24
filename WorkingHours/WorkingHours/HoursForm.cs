using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Shared;

namespace WorkingHours
{
    public partial class HoursForm : Form
    {
        public HoursForm()
        {
            InitializeComponent();
        }

        public void ShowDialog( IWin32Window owner, WorkingHoursConfiguration configuration, List< EventEntry > events )
        {
            if ( events.Count > 0 )
            {
                DateTime d              = events[ 0 ].date;
                DateTime start_of_week  = d.Subtract( new TimeSpan( (int)( d.DayOfWeek )-1, d.Hour, d.Minute, d.Second, d.Millisecond ) );
                DateTime end_of_week    = start_of_week.AddDays( 7 );
                int today               = d.MondayFirstDoW();

                // partition the events into weeks = array of days
                // then create 1 WeekGraph control for every week in the event list
                List< EventEntry >[] days = new List<EventEntry>[ 7 ]; for ( int i=0; i<7; i++ ) days[ i ] = new List<EventEntry>();
                foreach ( EventEntry ee in events )
                {
                    if ( ee.date.CompareTo( end_of_week ) >= 0 )
                    {   // the next week has begun
                        flp_weeks.Controls.Add( new WeekGraph( configuration, days, start_of_week, end_of_week ) );
                        days            = new List<EventEntry>[ 7 ]; for ( int i=0; i<7; i++ ) days[ i ] = new List< EventEntry >();
                        start_of_week   = ee.date.Subtract( new TimeSpan( (int)( ee.date.DayOfWeek )-1, ee.date.Hour, ee.date.Minute, ee.date.Second, ee.date.Millisecond ) );
                        end_of_week     = start_of_week.AddDays( 7 );
                    }
                    days[ ee.date.MondayFirstDoW() ].Add( ee );
                }
                Control last_weekgraph = new WeekGraph( configuration, days, start_of_week, end_of_week );
                flp_weeks.Controls.Add( last_weekgraph );

                // fit the parent form's area to the flowlayoutpanel as closely as possible...
                Rectangle working_area  = Screen.GetWorkingArea( this );
                int controls_per_row    = Math.Min( configuration.max_weeks_per_row,
                                                    Math.Min( working_area.Width / flp_weeks.Controls[ 0 ].Width, flp_weeks.Controls.Count )
                                                  );
                int target_width        = controls_per_row * flp_weeks.Controls[ 0 ].Width;
                int target_height       = 0;
                int control_rows        = ( ( flp_weeks.Controls.Count + controls_per_row - 1 ) / controls_per_row );  // rounded up
                bool scrollbar_appeared = false;
                if ( working_area.Height > 1.5* control_rows * flp_weeks.Controls[ 0 ].Height )
                {
                    target_height = ( (flp_weeks.Controls.Count + controls_per_row - 1 ) / controls_per_row ) * flp_weeks.Controls[ 0 ].Height;
                }
                else if ( working_area.Height > flp_weeks.Controls[ 0 ].Height )
                {
                    target_height = flp_weeks.Controls[ 0 ].Height;
                    scrollbar_appeared = true;
                }
                else
                {
                    target_height = working_area.Height * 8 / 10;
                    scrollbar_appeared = true;
                }
                if ( scrollbar_appeared )
                {
                    target_width += SystemInformation.VerticalScrollBarWidth;
                }

                this.Width  = 10 + target_width;
                this.Height = 10 + target_height + b_ok.Height + this.Height - this.ClientRectangle.Height;

                flp_weeks.ScrollControlIntoView( last_weekgraph );
            }

            ShowDialog( owner );
        }

        private void b_ok_Click( object sender, EventArgs e )
        {
            this.DialogResult = DialogResult.OK;
        }

    }
}
