using Extplorer.FilesSytems;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Extplorer
{
    public partial class ExtractionProgressBar : Form
    {
        protected List< File >        files_to_extract;
        protected BackgroundWorker    worker = new BackgroundWorker();
        protected int                 total_file_count = 0;
        protected int                 current_file = 0;
        protected string              path;
        protected bool                cancelled = false;

        public ExtractionProgressBar( List< File > files_to_extract, string path )
        {
            InitializeComponent();
            this.files_to_extract   = files_to_extract;
            this.path               = path;
            Text                    = "Extracting to: " + path;

            int file_list_process_pointer = 0;
            List< File > file_count_list = new List<File>( files_to_extract );
            while ( file_list_process_pointer < file_count_list.Count )
            {
                if ( file_count_list[ file_list_process_pointer ].Type == FileType.Directory )
                {
                    List< File > files;
                    List< File > dirs;
                    file_count_list[ file_list_process_pointer ].List( out files, out dirs );
                    file_count_list.AddRange( dirs );
                    file_count_list.AddRange( files );
                }
                file_list_process_pointer++;
            }
            total_file_count = file_count_list.Count;

            if ( total_file_count <= 1 )
            {
                pb_total_progress.Visible   = false;
                l_item.Visible              = false;
                l_total_progress.Visible    = false;
            }
            else
            {
                pb_total_progress.Maximum   = total_file_count;
                pb_total_progress.Value     = 1;
                l_total_progress.Text       = "1 / " + total_file_count.ToString();
            }

            worker.WorkerReportsProgress        = true;
            worker.WorkerSupportsCancellation   = true;
            worker.DoWork               += new DoWorkEventHandler( DoWork );
            worker.ProgressChanged      += new ProgressChangedEventHandler( ProgressChanged );
            worker.RunWorkerCompleted   += new RunWorkerCompletedEventHandler( RunWorkerCompleted );
            worker.RunWorkerAsync();
        }

        private void DoWork( object sender, DoWorkEventArgs e )
        {
            foreach ( File f in files_to_extract )
            {
                f.Save( path, worker );
                if ( worker.CancellationPending )
                {
                    cancelled = true;
                    return;
                }
            }
        }

        private void ProgressChanged( object sender, ProgressChangedEventArgs e )
        {
            if ( e.UserState != null )
            {
                l_progress.Text         = e.UserState.ToString(); ;
                current_file++;
                pb_total_progress.Value = current_file;
                l_total_progress.Text   = current_file.ToString() + " / " + total_file_count.ToString();
            }

            pb_progress.Value       = e.ProgressPercentage;
        }

        private void RunWorkerCompleted( object sender, RunWorkerCompletedEventArgs e )
        {
            if ( cancelled ) DialogResult = DialogResult.Cancel;
            else DialogResult = DialogResult.OK;
            Close();
        }

        private void b_cancel_Click( object sender, EventArgs e )
        {
            worker.CancelAsync();
        }
    }
}
