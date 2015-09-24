using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Win32;
using Shared;

namespace WorkingHours
{
    public partial class WeekGraph : UserControl
    {
        private Pen      pen_gray       = new Pen( Brushes.Gray );
        private Pen      pen_lightgray  = new Pen( Brushes.LightGray );
        private Font     font_normal    = new Font( FontFamily.GenericSansSerif, 8 );
        private Font     font_small     = new Font( FontFamily.GenericSansSerif, 7 );

        public WeekGraph( WorkingHoursConfiguration configuration, List< EventEntry >[] days, DateTime start_of_week, DateTime end_of_week )
        {
            InitializeComponent();

            DrawHours( configuration, days, start_of_week, end_of_week );
        }

        protected void DrawHours( WorkingHoursConfiguration config, List< EventEntry >[] days, DateTime start_of_week, DateTime end_of_week )
        {
            pb_image.Image      = new Bitmap( 20 + 15*10, config.GraphHours * config.zoom_amount + 60 );
            pb_image.Size       = pb_image.Image.Size;
            Graphics gr         = Graphics.FromImage( pb_image.Image );
            gr.Clear( Color.White );

            DrawBackground( gr, config );

            double work_avg     = 0;
            double unlocked_avg = 0;
            DrawBars( gr, config, days, start_of_week, end_of_week, out work_avg, out unlocked_avg);

            // draw dates
            for ( int i=0; i<days.Length; i++ )
            {
                if ( i > 4 )
                {
                    gr.DrawString( start_of_week.AddDays( i ).Day.ToString(), font_small, Brushes.OrangeRed, 27 + i*20, config.GraphHours * config.zoom_amount + 20 );
                }
                else
                {
                    gr.DrawString( start_of_week.AddDays( i ).Day.ToString(), font_small, Brushes.Black,     27 + i*20, config.GraphHours * config.zoom_amount + 20 );
                }
            }
            if ( !config.show_detailed_weeks )
            {
                gr.DrawString( "W: " + work_avg.ToHourMinuteString(),
                                font_normal,
                                Brushes.Black,
                                pb_image.Width / 2 - 70,
                                config.GraphHours * config.zoom_amount + 40 );
            }
            gr.DrawString( start_of_week.ToString( "MMM" ), font_normal, Brushes.Black, pb_image.Width / 2 - 10, config.GraphHours * config.zoom_amount + 40 );
        }

        protected void DrawBar( Graphics gr, WorkingHoursConfiguration config, Bar bar )
        {
            if ( bar == null ) return;

            float height    = (float)bar.Height;
            float start     = (float)config.time_end - (float)bar.start.TimeOfDay.TotalHours - height;
            float x_offset  = 0;

            // center for lone bars
            if (     config.show_unlocked_hours && !config.show_work_hours || 
                    !config.show_unlocked_hours &&  config.show_work_hours )
            {
                x_offset = 2;
            }
            else if ( bar.is_unlocked ) // right for the second bar (unlocked bar)
            {
                x_offset = 8;
            }

            if ( config.show_detailed_weeks )
            {
                gr.FillRectangle( bar.color, 25 + bar.day_of_week*20 + x_offset, 10 + start * config.zoom_amount, 7, height * config.zoom_amount );
            }
            else
            {
                if ( !bar.is_unlocked && bar.Height > 0 )
                {
                    gr.DrawString( bar.Height.ToHourMinuteString(), font_small, Brushes.Black, 21 + bar.day_of_week*20 + x_offset, -5 + (float)( config.GraphHours - bar.Height ) * config.zoom_amount );
                }
                gr.FillRectangle( bar.color, 25 + bar.day_of_week*20 + x_offset, 10 + (float)(config.GraphHours - bar.Height) * config.zoom_amount, 7, (float)bar.Height * config.zoom_amount );
            }
        }

        protected void DrawBars(    Graphics                    gr, 
                                    WorkingHoursConfiguration   config, 
                                    List< EventEntry >[]        days, 
                                    DateTime                    start_of_week, 
                                    DateTime                    end_of_week, 
                                    out double                  work_hours_week, 
                                    out double                  unlocked_hours_week )
        {
            double work_hours_day       = 0;
            double unlocked_hours_day   = 0;
            int work_days               = 0;
            work_hours_week             = 0;
            unlocked_hours_week         = 0;
            BarCreator bar_creator      = new BarCreator( config, start_of_week, end_of_week );

            for ( int i = 0; i < days.Length; i++ )
            {
                work_hours_day     = 0;
                unlocked_hours_day = 0;

                foreach ( EventEntry ee in days[ i ] )
                {
                    Bar bar = bar_creator.GetBar( ee );
                    if ( config.show_detailed_weeks || bar == null )
                    {
                        DrawBar( gr, config, bar );
                    }
                    else
                    {
                        if ( bar.is_unlocked )
                        {
                            unlocked_hours_day += bar.Height;
                        }
                        else
                        {
                            work_hours_day += bar.Height;
                        }
                    }
                }

                foreach ( Bar b in bar_creator.GetFinalBarsForDay() ) // last unlock - now
                {
                    if ( config.show_detailed_weeks )
                    {
                        DrawBar( gr, config, b );
                    }
                    else
                    {
                        if ( b.is_unlocked )
                        {
                            unlocked_hours_day += b.Height;
                        }
                        else
                        {
                            work_hours_day += b.Height;
                        }
                    }
                }
                if ( !config.show_detailed_weeks )
                {
                    Bar b1 = new Bar( new SolidBrush( config.work_bar_color     ), i, false, start_of_week.AddDays( i ), start_of_week.AddDays( i + work_hours_day     / 24 ) );
                    Bar b2 = new Bar( new SolidBrush( config.unlocked_bar_color ), i, true,  start_of_week.AddDays( i ), start_of_week.AddDays( i + unlocked_hours_day / 24 ) );
                    DrawBar( gr, config, b1 );
                    DrawBar( gr, config, b2 );
                }

                if ( work_hours_day > 0 || unlocked_hours_day > 0 )
                {
                    work_days++;
                }

                work_hours_week     += work_hours_day;
                unlocked_hours_week += unlocked_hours_day;
            }
            work_hours_week     /= work_days;
            unlocked_hours_week /= work_days;
        }

        protected void DrawBackground( Graphics gr, WorkingHoursConfiguration config )
        {
            gr.DrawLine( pen_gray, 15, 10, 15, 10 + config.GraphHours * config.zoom_amount );
            for ( int   i = 0, hours = config.time_end;
                        i <= config.time_end - config.time_start;
                        i++, hours-- )
            {
                if ( config.full_background_lines )
                {
                    gr.DrawLine( pen_lightgray, 20, 10 + i*config.zoom_amount, 160, 10 + i*config.zoom_amount );
                }
                gr.DrawLine( pen_gray, 15, 10 + i*config.zoom_amount, 20, 10 + i*config.zoom_amount );

                if ( config.show_detailed_weeks )
                {
                    if ( hours < config.work_start.Hours || hours > config.work_end.Hours )
                    {
                        gr.DrawString( hours.ToString(), font_small, Brushes.LightGray, 1, 5 + i * config.zoom_amount );
                    }
                    else
                    {
                        gr.DrawString( hours.ToString(), font_small, Brushes.Black, 1, 5 + i * config.zoom_amount );
                    }
                }
                else
                {
                    if ( config.GraphHours - i <= config.work_hours )
                    {
                        gr.DrawString( ( config.GraphHours - i ).ToString(), font_small, Brushes.Black, 1, 5 + i * config.zoom_amount );
                    }
                    else
                    {
                        gr.DrawString( ( config.GraphHours - i ).ToString(), font_small, Brushes.Red, 1, 5 + i * config.zoom_amount );
                    }
                }
            }

            // bar showing expected working hours
            if ( !config.show_detailed_weeks )
            {
                int y = (int)(10 + ( config.GraphHours - config.work_hours ) * config.zoom_amount);
                if ( config.full_background_lines )
                {
                    gr.DrawLine( new Pen( Color.LightSkyBlue, 3 ), 20, y, 160, y );
                }
                gr.DrawLine( new Pen( Color.DodgerBlue, 3 ), 15, y, 20, y );
            }
        }
    }

    public class Bar
    {
        public Brush    color;
        public int      day_of_week;
        public bool     is_unlocked;
        public DateTime start;
        public DateTime end;

        public double   Height { get { return end.Subtract( start ).TotalHours; } }

        public Bar( Brush color, int day_of_week, bool is_unlocked, DateTime start, DateTime end )
        {
            this.color         = color;
            this.day_of_week   = day_of_week;
            this.is_unlocked   = is_unlocked;
            this.start         = start;
            this.end           = end;
        }
    }

    public class BarCreator
    {
        protected WorkingHoursConfiguration     config;
        protected DateTime                      week_start;
        protected DateTime                      week_end;
        protected DateTime                      first_unlock_date;
        protected DateTime                      last_unlock_date;
        protected DateTime                      last_lock_date;
        protected Brush                         work_color;
        protected Brush                         latest_work_color;
        protected Brush                         unlocked_color;
        protected Brush                         latest_unlocked_color;
        protected DateTime                      invalid_date = DateTime.MinValue;

        protected bool Valid( DateTime date )
        {
            return date.CompareTo( invalid_date ) != 0;
        }

        public BarCreator( WorkingHoursConfiguration config, DateTime week_start, DateTime week_end )
        {
            this.config             = config;
            this.week_start         = week_start;
            this.week_end           = week_end;

            work_color              = new SolidBrush( config.work_bar_color );
            latest_work_color       = new SolidBrush( config.latest_work_bar_color );
            unlocked_color          = new SolidBrush( config.unlocked_bar_color );
            latest_unlocked_color   = new SolidBrush( config.latest_unlocked_bar_color );

            first_unlock_date       = invalid_date;
            last_unlock_date        = invalid_date;
            last_lock_date          = invalid_date;
        }

        public Bar GetBar( EventEntry e )
        {
            Bar bar = null;

            if ( e.event_type == SessionSwitchReason.SessionLogon )
            {
                if ( !Valid( first_unlock_date ) )
                {
                    first_unlock_date = e.date;
                }
                last_unlock_date = e.date;
            }
            else if ( e.event_type == SessionSwitchReason.SessionLogoff )
            {
                last_lock_date = e.date;
                if ( Valid( last_unlock_date ) && config.show_unlocked_hours )
                {
                    bar = new Bar( unlocked_color, e.date.MondayFirstDoW(), true, last_unlock_date, last_lock_date );
                }
            }
            
            return bar;
        }

        public List< Bar > GetFinalBarsForDay()
        {
            List< Bar > bars = new List< Bar >();
            // draw dark&light green bars, light blue bar
            if ( Valid( first_unlock_date ) )
            {
                if ( config.show_work_hours )
                {
                    if ( Valid( last_lock_date ) )
                    {
                        bars.Add( new Bar( work_color, first_unlock_date.MondayFirstDoW(), false, first_unlock_date, last_lock_date ) );
                    }
                    if ( DateTime.Now.Subtract( DateTime.Now.TimeOfDay ) == last_unlock_date.Subtract( last_unlock_date.TimeOfDay ) )
                    {
                        if ( Valid( last_lock_date ) )
                        {
                            bars.Add( new Bar( latest_work_color, last_lock_date.MondayFirstDoW(), false, last_lock_date, DateTime.Now ) );
                        }
                        else
                        {
                            bars.Add( new Bar( latest_work_color, last_unlock_date.MondayFirstDoW(), false, last_unlock_date, DateTime.Now ) );
                        }
                    }
                }
            }

            if (    Valid( last_unlock_date ) && config.show_unlocked_hours &&
                    DateTime.Now.Subtract( DateTime.Now.TimeOfDay ) == last_unlock_date.Subtract( last_unlock_date.TimeOfDay ) )
            {
                bars.Add( new Bar( latest_unlocked_color, last_unlock_date.MondayFirstDoW(), true, last_unlock_date, DateTime.Now ) );
            }

            first_unlock_date       = invalid_date;
            last_unlock_date        = invalid_date;
            last_lock_date          = invalid_date;
            return bars;
        }
    }
}

