using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading;
using System.Net;
using System.Web;
using System.IO;

namespace HTTPStressTest
{
    public class TestRunner
    {
        public class TestEvent
        {
            public EventType    Type     { get; set; }
            public string       Message  { get; set; }

            public TestEvent( EventType type, string message ) { Type = type; Message = message; }
        }

        public delegate void TestEventHandler( TestEvent test_event );
        public enum EventType { Request, Response, Error, Success };

        private BackgroundWorker        worker = new BackgroundWorker();
        private TestEventHandler        report_event;
        private Config                  config;
        private volatile bool           shudown_in_progress;

        public TestRunner( Config config, TestEventHandler report_event )
        {
            this.config                         = config;
            this.report_event                   = report_event;

            worker.WorkerReportsProgress        = true;
            worker.WorkerSupportsCancellation   = true;
            worker.DoWork                       += new DoWorkEventHandler( DoWork );
            worker.ProgressChanged              += new ProgressChangedEventHandler( ProgressChanged );
        }

        public bool Start()
        {
            if ( worker.IsBusy ) return false;

            shudown_in_progress = false;
            worker.RunWorkerAsync();
            return true;
        }

        public void Stop()
        {
            if ( worker.IsBusy )
            {
                shudown_in_progress = true;
                worker.CancelAsync();
                while ( shudown_in_progress ) Thread.Sleep( 100 );
            }
        }

        private void ReportEvent( TestEvent test_event )
        {
            worker.ReportProgress( 0, test_event );
        }

        private string RequestResponse( string request_str )
        {
            WebRequest request  = HttpWebRequest.Create( request_str );
            if ( request.GetType() == typeof( HttpWebRequest ) )
            {
                ( (HttpWebRequest)request ).CookieContainer = new CookieContainer();
            }
            string response_str = "";
            request.Timeout = 60000;
            DateTime request_timestamp = DateTime.Now;
            using ( WebResponse response = request.GetResponse() )
            {
                ReportEvent( new TestEvent( EventType.Success, DateTime.Now.Subtract( request_timestamp ).TotalSeconds.ToString() + " s" ) );
                using ( StreamReader reader = new StreamReader( response.GetResponseStream() ) )
                {
                    response_str = reader.ReadToEnd();
                }
            }
            return response_str;
        }

        private void DoWork( object sender, DoWorkEventArgs e )
        {

            string request = "";
            string response = "";
            while ( !worker.CancellationPending )
            {
                request = config.URL + config.Request;
                ReportEvent( new TestEvent( EventType.Request, request ) );
                try
                {
                    response = RequestResponse( request );
                }
                catch ( Exception exception )
                {
                    ReportEvent( new TestEvent( EventType.Error, exception.Message ) );
                    continue;
                }
                ReportEvent( new TestEvent( EventType.Response, response ) );
            }
            shudown_in_progress = false;
        }

        private void ProgressChanged( object sender, ProgressChangedEventArgs e )
        {
            report_event( (TestEvent)e.UserState );
        }
    }
}
