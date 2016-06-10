using System;
using System.Windows.Forms;

namespace Extplorer
{
    public partial class FileSelector : Form
    {
        protected string file_name = "";

        public string Path { get { return tb_path.Text; } }
        public string FileName { get { return file_name; } }
        public UInt32 SectorSize { get { return Convert.ToUInt32( cb_sector_size.SelectedItem.ToString() ); } }

        public FileSelector()
        {
            InitializeComponent();

            cb_sector_size.SelectedIndex = 0;
        }

        private void b_cd_Click( object sender, EventArgs e )
        {
            OpenFileDialog fd   = new OpenFileDialog();
            fd.Multiselect      = false;
            if ( fd.ShowDialog( this ) == DialogResult.OK )
            {
                tb_path.Text            = fd.FileName;
                tb_path.SelectionLength = 0;
                tb_path.SelectionStart  = tb_path.Text.Length - 1;
                file_name               = fd.SafeFileName;
            }
        }

        private void b_ok_Click( object sender, EventArgs e )
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void b_cancel_Click( object sender, EventArgs e )
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
