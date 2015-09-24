namespace HTTPStressTest
{
    partial class MainForm
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
            this.tlp_controls = new System.Windows.Forms.TableLayoutPanel();
            this.flp_controls = new System.Windows.Forms.FlowLayoutPanel();
            this.l_threads = new System.Windows.Forms.Label();
            this.c_threads = new System.Windows.Forms.NumericUpDown();
            this.b_start_stop = new System.Windows.Forms.Button();
            this.cb_show_response = new System.Windows.Forms.CheckBox();
            this.tc_test_container = new System.Windows.Forms.TabControl();
            this.tlp_controls.SuspendLayout();
            this.flp_controls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c_threads)).BeginInit();
            this.SuspendLayout();
            // 
            // tlp_controls
            // 
            this.tlp_controls.ColumnCount = 1;
            this.tlp_controls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_controls.Controls.Add(this.flp_controls, 0, 0);
            this.tlp_controls.Controls.Add(this.tc_test_container, 0, 1);
            this.tlp_controls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_controls.Location = new System.Drawing.Point(0, 0);
            this.tlp_controls.Margin = new System.Windows.Forms.Padding(0);
            this.tlp_controls.Name = "tlp_controls";
            this.tlp_controls.RowCount = 2;
            this.tlp_controls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlp_controls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_controls.Size = new System.Drawing.Size(565, 326);
            this.tlp_controls.TabIndex = 0;
            // 
            // flp_controls
            // 
            this.flp_controls.Controls.Add(this.l_threads);
            this.flp_controls.Controls.Add(this.c_threads);
            this.flp_controls.Controls.Add(this.b_start_stop);
            this.flp_controls.Controls.Add(this.cb_show_response);
            this.flp_controls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flp_controls.Location = new System.Drawing.Point(0, 0);
            this.flp_controls.Margin = new System.Windows.Forms.Padding(0);
            this.flp_controls.Name = "flp_controls";
            this.flp_controls.Size = new System.Drawing.Size(565, 30);
            this.flp_controls.TabIndex = 0;
            // 
            // l_threads
            // 
            this.l_threads.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.l_threads.AutoSize = true;
            this.l_threads.Location = new System.Drawing.Point(3, 8);
            this.l_threads.Margin = new System.Windows.Forms.Padding(3);
            this.l_threads.Name = "l_threads";
            this.l_threads.Size = new System.Drawing.Size(49, 13);
            this.l_threads.TabIndex = 0;
            this.l_threads.Text = "Threads:";
            this.l_threads.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // c_threads
            // 
            this.c_threads.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.c_threads.AutoSize = true;
            this.c_threads.Location = new System.Drawing.Point(58, 4);
            this.c_threads.Name = "c_threads";
            this.c_threads.ReadOnly = true;
            this.c_threads.Size = new System.Drawing.Size(45, 20);
            this.c_threads.TabIndex = 1;
            this.c_threads.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.c_threads.ValueChanged += new System.EventHandler(this.c_threads_ValueChanged);
            // 
            // b_start_stop
            // 
            this.b_start_stop.Location = new System.Drawing.Point(109, 3);
            this.b_start_stop.Name = "b_start_stop";
            this.b_start_stop.Size = new System.Drawing.Size(75, 23);
            this.b_start_stop.TabIndex = 2;
            this.b_start_stop.Text = "Start";
            this.b_start_stop.UseVisualStyleBackColor = true;
            this.b_start_stop.Click += new System.EventHandler(this.b_start_stop_Click);
            // 
            // cb_show_response
            // 
            this.cb_show_response.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cb_show_response.AutoSize = true;
            this.cb_show_response.Checked = true;
            this.cb_show_response.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_show_response.Location = new System.Drawing.Point(190, 6);
            this.cb_show_response.Name = "cb_show_response";
            this.cb_show_response.Size = new System.Drawing.Size(99, 17);
            this.cb_show_response.TabIndex = 3;
            this.cb_show_response.Text = "Show response";
            this.cb_show_response.UseVisualStyleBackColor = true;
            this.cb_show_response.CheckedChanged += new System.EventHandler(this.cb_show_response_CheckedChanged);
            // 
            // tc_test_container
            // 
            this.tc_test_container.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tc_test_container.Location = new System.Drawing.Point(3, 33);
            this.tc_test_container.Name = "tc_test_container";
            this.tc_test_container.SelectedIndex = 0;
            this.tc_test_container.Size = new System.Drawing.Size(559, 290);
            this.tc_test_container.TabIndex = 1;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(565, 326);
            this.Controls.Add(this.tlp_controls);
            this.Name = "MainForm";
            this.Text = "HTTP StressTest";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.tlp_controls.ResumeLayout(false);
            this.flp_controls.ResumeLayout(false);
            this.flp_controls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c_threads)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlp_controls;
        private System.Windows.Forms.FlowLayoutPanel flp_controls;
        private System.Windows.Forms.Label l_threads;
        private System.Windows.Forms.NumericUpDown c_threads;
        private System.Windows.Forms.Button b_start_stop;
        private System.Windows.Forms.TabControl tc_test_container;
        private System.Windows.Forms.CheckBox cb_show_response;
    }
}

