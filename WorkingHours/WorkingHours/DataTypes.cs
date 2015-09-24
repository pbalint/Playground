using System;
using System.Drawing;
using System.Xml.Serialization;

namespace WorkingHours
{
    public class WorkingHoursConfiguration
    {
        [XmlIgnore] public Color    work_bar_color                          = Color.Green;
        [XmlIgnore] public Color    latest_work_bar_color                   = Color.YellowGreen;
        [XmlIgnore] public Color    unlocked_bar_color                      = Color.Blue;
        [XmlIgnore] public Color    latest_unlocked_bar_color               = Color.RoyalBlue;
                    public double   work_hours                              = 8.5;     // first unlock/logon of the day - last lock/logoff of the day
                    public double   unlocked_hours                          = 8;       // total unlocked time
                    public int      time_start                              = 8;
                    public int      time_end                                = 20;
                    public int      zoom_amount                             = 10;
        [XmlIgnore] public TimeSpan work_start                              = new TimeSpan( 9, 0, 0 );
        [XmlIgnore] public TimeSpan work_end                                = new TimeSpan( 18, 0, 0 );
                    public bool     show_work_hours                         = true;
                    public bool     show_unlocked_hours                     = true;
                    public bool     full_background_lines                   = true;
                    public int      max_weeks_per_row                       = 4;
                    public int      max_rows                                = 1;
                    public bool     show_detailed_weeks                     = true;
                    public bool     log_program_start                       = true;
                    public bool     log_program_shutdown                    = true;

        // helper properties for xml serialization
        [XmlElement( "work_bar_color" )]            public string WorkBarColor { get { return work_bar_color.ToArgb().ToString( "X" ); } set { work_bar_color = Color.FromArgb( Convert.ToInt32( value, 16 ) ); } }
        [XmlElement( "latest_work_bar_color" )]     public string LatestWorkBarColor { get { return latest_work_bar_color.ToArgb().ToString( "X" ); } set { latest_work_bar_color = Color.FromArgb( Convert.ToInt32( value, 16 ) ); } }
        [XmlElement( "unlocked_bar_color" )]        public string UnlockedBarColor { get { return unlocked_bar_color.ToArgb().ToString( "X" ); } set { unlocked_bar_color = Color.FromArgb( Convert.ToInt32( value, 16 ) ); } }
        [XmlElement( "latest_unlocked_bar_color" )] public string LatestUnlockedBarColor { get { return latest_unlocked_bar_color.ToArgb().ToString( "X" ); } set { latest_unlocked_bar_color = Color.FromArgb( Convert.ToInt32( value, 16 ) ); } }
        [XmlElement( "work_start" )]                public string WorkStart { get { return work_start.ToString(); } set { work_start = TimeSpan.Parse( value ); } }
        [XmlElement( "work_end" )]                  public string WorkEnd { get { return work_end.ToString(); } set { work_end = TimeSpan.Parse( value ); } }

                    public int GraphHours { get { return time_end - time_start; } }
    }
}
