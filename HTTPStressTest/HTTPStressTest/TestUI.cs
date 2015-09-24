using System;
using System.Windows.Forms;

namespace HTTPStressTest
{
    public partial class TestUI : TabPage
    {
        private TestRunner  runner;
        private int         query_count = 0;
        private Config      config;

        public TestUI( Config config )
        {
            InitializeComponent();

            this.config = config;
            Text        = query_count.ToString();
            runner      = new TestRunner( config, ReportFunction );
        }

        private void ReportFunction( TestRunner.TestEvent test_event )
        {
            Text = query_count.ToString();
            if ( test_event.Type == TestRunner.EventType.Request )
            {
                tb_request.Text += test_event.Message + Environment.NewLine;
            }
            else if ( test_event.Type == TestRunner.EventType.Success )
            {
                query_count++;
                tb_request.Text += "\t" + test_event.Message + Environment.NewLine;
            }
            else if ( test_event.Type == TestRunner.EventType.Error )
            {
                tb_request.Text += "\t" + test_event.Message + Environment.NewLine;
            }
            else if ( test_event.Type == TestRunner.EventType.Response && config.ShowResponse )
            {
                tb_response.Text = test_event.Message + Environment.NewLine;
            }
        }

        public bool Start()
        {
            return runner.Start();
        }

        public void Stop()
        {
            runner.Stop();
        }
    }
}
