namespace PMCFiller
{
    partial class CredentialsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if ( disposing && ( components != null ) )
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.b_ok = new System.Windows.Forms.Button();
            this.b_cancel = new System.Windows.Forms.Button();
            this.tb_user = new System.Windows.Forms.TextBox();
            this.tb_password = new System.Windows.Forms.TextBox();
            this.l_user = new System.Windows.Forms.Label();
            this.l_password = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.cb_save_credentials = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // b_ok
            // 
            this.b_ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.b_ok.Location = new System.Drawing.Point(51, 90);
            this.b_ok.Name = "b_ok";
            this.b_ok.Size = new System.Drawing.Size(75, 21);
            this.b_ok.TabIndex = 5;
            this.b_ok.Text = "&OK";
            this.b_ok.UseVisualStyleBackColor = true;
            this.b_ok.Click += new System.EventHandler(this.b_ok_Click);
            // 
            // b_cancel
            // 
            this.b_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.b_cancel.Location = new System.Drawing.Point(132, 90);
            this.b_cancel.Name = "b_cancel";
            this.b_cancel.Size = new System.Drawing.Size(75, 21);
            this.b_cancel.TabIndex = 6;
            this.b_cancel.Text = "&Cancel";
            this.b_cancel.UseVisualStyleBackColor = true;
            this.b_cancel.Click += new System.EventHandler(this.b_cancel_Click);
            // 
            // tb_user
            // 
            this.tb_user.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_user.Location = new System.Drawing.Point(132, 3);
            this.tb_user.Name = "tb_user";
            this.tb_user.Size = new System.Drawing.Size(123, 20);
            this.tb_user.TabIndex = 0;
            // 
            // tb_password
            // 
            this.tb_password.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_password.Location = new System.Drawing.Point(132, 32);
            this.tb_password.Name = "tb_password";
            this.tb_password.Size = new System.Drawing.Size(123, 20);
            this.tb_password.TabIndex = 1;
            this.tb_password.UseSystemPasswordChar = true;
            // 
            // l_user
            // 
            this.l_user.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.l_user.AutoSize = true;
            this.l_user.Location = new System.Drawing.Point(3, 8);
            this.l_user.Name = "l_user";
            this.l_user.Size = new System.Drawing.Size(29, 13);
            this.l_user.TabIndex = 4;
            this.l_user.Text = "User";
            // 
            // l_password
            // 
            this.l_password.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.l_password.AutoSize = true;
            this.l_password.Location = new System.Drawing.Point(3, 37);
            this.l_password.Name = "l_password";
            this.l_password.Size = new System.Drawing.Size(53, 13);
            this.l_password.TabIndex = 5;
            this.l_password.Text = "Password";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.l_user, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tb_password, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.l_password, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tb_user, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.b_ok, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.b_cancel, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.cb_save_credentials, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(258, 116);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // cb_save_credentials
            // 
            this.cb_save_credentials.AutoSize = true;
            this.cb_save_credentials.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.tableLayoutPanel1.SetColumnSpan(this.cb_save_credentials, 2);
            this.cb_save_credentials.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cb_save_credentials.Location = new System.Drawing.Point(3, 61);
            this.cb_save_credentials.Name = "cb_save_credentials";
            this.cb_save_credentials.Size = new System.Drawing.Size(252, 23);
            this.cb_save_credentials.TabIndex = 4;
            this.cb_save_credentials.Text = "Save credentials";
            this.cb_save_credentials.UseVisualStyleBackColor = true;
            // 
            // CredentialsForm
            // 
            this.AcceptButton = this.b_ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.b_cancel;
            this.ClientSize = new System.Drawing.Size(258, 116);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "CredentialsForm";
            this.Text = "Enter your credentials";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button b_ok;
        private System.Windows.Forms.Button b_cancel;
        private System.Windows.Forms.TextBox tb_user;
        private System.Windows.Forms.TextBox tb_password;
        private System.Windows.Forms.Label l_user;
        private System.Windows.Forms.Label l_password;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.CheckBox cb_save_credentials;
    }
}