using System;
using System.Windows.Forms;

namespace ZZChecker
{
    public partial class CredentialsForm : Form
    {
        public CredentialsForm()
        {
            InitializeComponent();
        }

        public DialogResult ShowDialog( IWin32Window owner, ref MainForm.Config data )
        {
            tb_user.Text                    = data.User;
            tb_password.Text                = data.Password;
            c_refresh_time.Value            = data.RefreshTime / 1000;
            cb_oneshot.Checked              = data.OneShot;
            cb_save_credentials.Checked     = data.SaveCredentials;

            DialogResult result = ShowDialog( owner );

            if ( result == DialogResult.OK )
            {
                data.User               = tb_user.Text;
                data.Password           = tb_password.Text;
                data.RefreshTime        = Convert.ToInt32( c_refresh_time.Value * 1000 );
                data.OneShot            = cb_oneshot.Checked;
                data.SaveCredentials    = cb_save_credentials.Checked;

            }

            return result;
        }

        private void b_ok_Click( object sender, EventArgs e )
        {
            DialogResult = DialogResult.OK;
        }

        private void b_cancel_Click( object sender, EventArgs e )
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
