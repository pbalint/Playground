using System;
using System.Windows.Forms;

namespace PMCFiller
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
            cb_save_credentials.Checked     = data.SaveCredentials;

            DialogResult result = ShowDialog( owner );

            if ( result == DialogResult.OK )
            {
                data.User               = tb_user.Text;
                data.Password           = tb_password.Text;
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
