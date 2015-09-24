using System;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.Win32;
using Shared;

namespace EventExtractor
{
    static class Program
    {

        private static string GetDomain( EventLogEntry entry )
        {
            switch ( entry.InstanceId )
            {
                case 682:
                case 683:
                case 4779:
                case 4778:
                    return entry.ReplacementStrings[1];
                case 528:
                    return "1";
                case 538:
                    return "2";
                case 4624:
                    return entry.ReplacementStrings[6];
                case 4647:
                case 4634:
                    return entry.ReplacementStrings[2];
                default:
                    throw new Exception("Did you forget to add the new event here?");
            }
        }

        private static string GetUserName( EventLogEntry entry )
        {
            switch (entry.InstanceId)
            {
                case 682:
                case 683:
                case 4779:
                case 4778:
                    return entry.ReplacementStrings[0];
                case 528:
                    return "1";
                case 538:
                    return "2";
                case 4624:
                    return entry.ReplacementStrings[5];
                case 4647:
                case 4634:
                    return entry.ReplacementStrings[1];
                default:
                    throw new Exception( "Did you forget to add the new event here?" );
            }
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            List<EventEntry> events = new List<EventEntry>();
            string domain = "";
            string name = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            if ( name.IndexOf( '\\' ) != -1 )
            {
                domain = name.Split( '\\' )[ 0 ];
                name = name.Split( '\\' )[ 1 ];
            }


            ISet<long> event_id_logon  = new HashSet<long>() { 528, 682, 4624, 4779 };
            ISet<long> event_id_logoff = new HashSet<long>() { 538, 683, 4634, 4647, 4778 };

            EventLog event_log = new EventLog( "Security" );
            int logon_count = 0;
            foreach ( EventLogEntry entry in event_log.Entries )
            {
                if ( event_id_logon.Contains( entry.InstanceId ) )
                {
                    if ( name == GetUserName( entry ) &&
                         ( domain == "" || domain != "" && domain == GetDomain( entry ) ) )
                    {
                        if ( logon_count == 0 )
                        {
                            logon_count = 1;
                            events.Add( new EventEntry( entry.TimeGenerated, SessionSwitchReason.SessionLogon ) );
                        }
                    }
                }
                else if ( event_id_logoff.Contains( entry.InstanceId ) )
                {
                    if ( name == GetUserName( entry ) &&
                         ( domain == "" || domain != "" && domain == GetDomain( entry ) ) )
                    {
                        if ( logon_count > 0 )
                        {
                            // handle work sessions that span to the next day
                            // prev session is a logon, but it's on the prev day, while curr session is a logoff, on curr day
                            if ( ( events.Count > 0 && events[ events.Count - 1 ].event_type == SessionSwitchReason.SessionLogon ) 
                                    &&
                                    ( events[ events.Count - 1 ].date.MondayFirstDoW() < entry.TimeGenerated.MondayFirstDoW() 
                                        ||
                                        ( events[ events.Count - 1 ].date.MondayFirstDoW() == 6 //sunday
                                            && 
                                            entry.TimeGenerated.MondayFirstDoW() == 1 // monday
                                        )
                                    )
                               )
                            {
                                DateTime prev_midnight = events[ events.Count - 1 ].date.AddSeconds( 24 * 60 * 60 - 1 - events[ events.Count - 1 ].date.TimeOfDay.TotalSeconds );
                                DateTime curr_morning  = entry.TimeGenerated.Subtract( entry.TimeGenerated.TimeOfDay );
                                // workaround: user logged off at prev day midnight, and logs on on curr day at 0:00
                                events.Add( new EventEntry( prev_midnight, SessionSwitchReason.SessionLogoff ) );
                                events.Add( new EventEntry( curr_morning, SessionSwitchReason.SessionLogon ) );
                            }
                            logon_count = 0;
                            events.Add( new EventEntry( entry.TimeGenerated, SessionSwitchReason.SessionLogoff ) );
                        }
                    }
                }
            }
            Utils.Serialize( events, "events.xml" );
        }
    }
}
