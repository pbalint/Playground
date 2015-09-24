using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;

namespace WorkingHours
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool newly_created;
            using ( new Mutex( true, "WorkingHoursMutex", out newly_created ) )
            {
                if ( newly_created )
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault( false );
                    Application.Run( new TrayIconForm() );
                }
                else
                {
                    MessageBox.Show( "Another application instance is already running!" );
                }
            }
        }
    }
}
