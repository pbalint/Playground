namespace WorkingHours
{
    partial class HoursForm
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
            this.tlp_controls = new System.Windows.Forms.TableLayoutPanel();
            this.flp_weeks = new System.Windows.Forms.FlowLayoutPanel();
            this.tlp_controls.SuspendLayout();
            this.SuspendLayout();
            // 
            // b_ok
            // 
            this.b_ok.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.b_ok.Location = new System.Drawing.Point(229, 398);
            this.b_ok.Name = "b_ok";
            this.b_ok.Size = new System.Drawing.Size(75, 23);
            this.b_ok.TabIndex = 0;
            this.b_ok.Text = "OK";
            this.b_ok.UseVisualStyleBackColor = true;
            this.b_ok.Click += new System.EventHandler(this.b_ok_Click);
            // 
            // tlp_controls
            // 
            this.tlp_controls.ColumnCount = 1;
            this.tlp_controls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_controls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlp_controls.Controls.Add(this.b_ok, 0, 1);
            this.tlp_controls.Controls.Add(this.flp_weeks, 0, 0);
            this.tlp_controls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_controls.Location = new System.Drawing.Point(0, 0);
            this.tlp_controls.Margin = new System.Windows.Forms.Padding(0);
            this.tlp_controls.Name = "tlp_controls";
            this.tlp_controls.RowCount = 2;
            this.tlp_controls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_controls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlp_controls.Size = new System.Drawing.Size(534, 424);
            this.tlp_controls.TabIndex = 1;
            // 
            // flp_weeks
            // 
            this.flp_weeks.AutoScroll = true;
            this.flp_weeks.BackColor = System.Drawing.SystemColors.Control;
            this.flp_weeks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flp_weeks.Location = new System.Drawing.Point(0, 0);
            this.flp_weeks.Margin = new System.Windows.Forms.Padding(0);
            this.flp_weeks.Name = "flp_weeks";
            this.flp_weeks.Size = new System.Drawing.Size(534, 394);
            this.flp_weeks.TabIndex = 2;
            // 
            // HoursForm
            // 
            this.AcceptButton = this.b_ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 424);
            this.Controls.Add(this.tlp_controls);
            this.Name = "HoursForm";
            this.Text = "Hours";
            this.tlp_controls.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button b_ok;
        private System.Windows.Forms.TableLayoutPanel tlp_controls;
        private System.Windows.Forms.FlowLayoutPanel flp_weeks;
    }
}