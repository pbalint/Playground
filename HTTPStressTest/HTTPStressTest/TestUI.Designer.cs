namespace HTTPStressTest
{
    partial class TestUI
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.sc_controls = new System.Windows.Forms.SplitContainer();
            this.tb_request = new System.Windows.Forms.TextBox();
            this.tb_response = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.sc_controls)).BeginInit();
            this.sc_controls.Panel1.SuspendLayout();
            this.sc_controls.Panel2.SuspendLayout();
            this.sc_controls.SuspendLayout();
            this.SuspendLayout();
            // 
            // sc_controls
            // 
            this.sc_controls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sc_controls.Location = new System.Drawing.Point(0, 0);
            this.sc_controls.Name = "sc_controls";
            this.sc_controls.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // sc_controls.Panel1
            // 
            this.sc_controls.Panel1.Controls.Add(this.tb_request);
            // 
            // sc_controls.Panel2
            // 
            this.sc_controls.Panel2.Controls.Add(this.tb_response);
            this.sc_controls.Size = new System.Drawing.Size(391, 250);
            this.sc_controls.SplitterDistance = 54;
            this.sc_controls.TabIndex = 1;
            // 
            // tb_request
            // 
            this.tb_request.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_request.Location = new System.Drawing.Point(0, 0);
            this.tb_request.Multiline = true;
            this.tb_request.Name = "tb_request";
            this.tb_request.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tb_request.Size = new System.Drawing.Size(391, 54);
            this.tb_request.TabIndex = 1;
            this.tb_request.WordWrap = false;
            // 
            // tb_response
            // 
            this.tb_response.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_response.Location = new System.Drawing.Point(0, 0);
            this.tb_response.Multiline = true;
            this.tb_response.Name = "tb_response";
            this.tb_response.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tb_response.Size = new System.Drawing.Size(391, 192);
            this.tb_response.TabIndex = 2;
            this.tb_response.WordWrap = false;
            // 
            // TestUI
            // 
            this.Controls.Add(this.sc_controls);
            this.Size = new System.Drawing.Size(391, 250);
            this.sc_controls.Panel1.ResumeLayout(false);
            this.sc_controls.Panel1.PerformLayout();
            this.sc_controls.Panel2.ResumeLayout(false);
            this.sc_controls.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sc_controls)).EndInit();
            this.sc_controls.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer sc_controls;
        private System.Windows.Forms.TextBox tb_request;
        private System.Windows.Forms.TextBox tb_response;
    }
}
