namespace Extplorer
{
    partial class FileSelector
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
            this.tb_path = new System.Windows.Forms.TextBox();
            this.b_cd = new System.Windows.Forms.Button();
            this.b_ok = new System.Windows.Forms.Button();
            this.b_cancel = new System.Windows.Forms.Button();
            this.cb_sector_size = new System.Windows.Forms.ComboBox();
            this.l_sector_size = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tb_path
            // 
            this.tb_path.Location = new System.Drawing.Point( 12, 12 );
            this.tb_path.Name = "tb_path";
            this.tb_path.Size = new System.Drawing.Size( 222, 20 );
            this.tb_path.TabIndex = 0;
            // 
            // b_cd
            // 
            this.b_cd.Location = new System.Drawing.Point( 240, 12 );
            this.b_cd.Name = "b_cd";
            this.b_cd.Size = new System.Drawing.Size( 24, 20 );
            this.b_cd.TabIndex = 1;
            this.b_cd.Text = "...";
            this.b_cd.UseVisualStyleBackColor = true;
            this.b_cd.Click += new System.EventHandler( this.b_cd_Click );
            // 
            // b_ok
            // 
            this.b_ok.Location = new System.Drawing.Point( 147, 40 );
            this.b_ok.Name = "b_ok";
            this.b_ok.Size = new System.Drawing.Size( 56, 23 );
            this.b_ok.TabIndex = 2;
            this.b_ok.Text = "OK";
            this.b_ok.UseVisualStyleBackColor = true;
            this.b_ok.Click += new System.EventHandler( this.b_ok_Click );
            // 
            // b_cancel
            // 
            this.b_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.b_cancel.Location = new System.Drawing.Point( 209, 40 );
            this.b_cancel.Name = "b_cancel";
            this.b_cancel.Size = new System.Drawing.Size( 55, 23 );
            this.b_cancel.TabIndex = 3;
            this.b_cancel.Text = "Cancel";
            this.b_cancel.UseVisualStyleBackColor = true;
            this.b_cancel.Click += new System.EventHandler( this.b_cancel_Click );
            // 
            // cb_sector_size
            // 
            this.cb_sector_size.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_sector_size.FormattingEnabled = true;
            this.cb_sector_size.Items.AddRange( new object[] {
            "512",
            "2048"} );
            this.cb_sector_size.Location = new System.Drawing.Point( 80, 41 );
            this.cb_sector_size.Name = "cb_sector_size";
            this.cb_sector_size.Size = new System.Drawing.Size( 61, 21 );
            this.cb_sector_size.TabIndex = 4;
            // 
            // l_sector_size
            // 
            this.l_sector_size.AutoSize = true;
            this.l_sector_size.Location = new System.Drawing.Point( 12, 44 );
            this.l_sector_size.Name = "l_sector_size";
            this.l_sector_size.Size = new System.Drawing.Size( 62, 13 );
            this.l_sector_size.TabIndex = 5;
            this.l_sector_size.Text = "Sector size:";
            // 
            // FileSelector
            // 
            this.AcceptButton = this.b_ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.b_cancel;
            this.ClientSize = new System.Drawing.Size( 272, 75 );
            this.ControlBox = false;
            this.Controls.Add( this.l_sector_size );
            this.Controls.Add( this.cb_sector_size );
            this.Controls.Add( this.b_cancel );
            this.Controls.Add( this.b_ok );
            this.Controls.Add( this.b_cd );
            this.Controls.Add( this.tb_path );
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FileSelector";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Select a disk image...";
            this.TopMost = true;
            this.ResumeLayout( false );
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tb_path;
        private System.Windows.Forms.Button b_cd;
        private System.Windows.Forms.Button b_ok;
        private System.Windows.Forms.Button b_cancel;
        private System.Windows.Forms.ComboBox cb_sector_size;
        private System.Windows.Forms.Label l_sector_size;
    }
}