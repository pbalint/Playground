namespace Extplorer
{
    partial class ExtractionProgressBar
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
            this.b_cancel = new System.Windows.Forms.Button();
            this.pb_progress = new System.Windows.Forms.ProgressBar();
            this.l_extracting_to = new System.Windows.Forms.Label();
            this.tlp_controls = new System.Windows.Forms.TableLayoutPanel();
            this.pb_total_progress = new System.Windows.Forms.ProgressBar();
            this.l_item = new System.Windows.Forms.Label();
            this.l_total_progress = new System.Windows.Forms.Label();
            this.l_progress = new System.Windows.Forms.Label();
            this.tlp_controls.SuspendLayout();
            this.SuspendLayout();
            // 
            // b_cancel
            // 
            this.b_cancel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tlp_controls.SetColumnSpan( this.b_cancel, 2 );
            this.b_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.b_cancel.Location = new System.Drawing.Point( 121, 79 );
            this.b_cancel.Name = "b_cancel";
            this.b_cancel.Size = new System.Drawing.Size( 75, 24 );
            this.b_cancel.TabIndex = 0;
            this.b_cancel.Text = "Cancel";
            this.b_cancel.UseVisualStyleBackColor = true;
            this.b_cancel.Click += new System.EventHandler( this.b_cancel_Click );
            // 
            // pb_progress
            // 
            this.tlp_controls.SetColumnSpan( this.pb_progress, 2 );
            this.pb_progress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pb_progress.Location = new System.Drawing.Point( 0, 38 );
            this.pb_progress.Margin = new System.Windows.Forms.Padding( 0 );
            this.pb_progress.Name = "pb_progress";
            this.pb_progress.Size = new System.Drawing.Size( 318, 19 );
            this.pb_progress.Step = 1;
            this.pb_progress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pb_progress.TabIndex = 1;
            // 
            // l_extracting_to
            // 
            this.l_extracting_to.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.l_extracting_to.AutoSize = true;
            this.l_extracting_to.Location = new System.Drawing.Point( 3, 60 );
            this.l_extracting_to.Name = "l_extracting_to";
            this.l_extracting_to.Size = new System.Drawing.Size( 69, 13 );
            this.l_extracting_to.TabIndex = 2;
            this.l_extracting_to.Text = "Extracting to:";
            // 
            // tlp_controls
            // 
            this.tlp_controls.AutoSize = true;
            this.tlp_controls.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlp_controls.ColumnCount = 2;
            this.tlp_controls.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle( System.Windows.Forms.SizeType.Percent, 23.89937F ) );
            this.tlp_controls.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle( System.Windows.Forms.SizeType.Percent, 76.10063F ) );
            this.tlp_controls.Controls.Add( this.pb_total_progress, 0, 0 );
            this.tlp_controls.Controls.Add( this.l_extracting_to, 0, 3 );
            this.tlp_controls.Controls.Add( this.pb_progress, 0, 2 );
            this.tlp_controls.Controls.Add( this.l_item, 0, 1 );
            this.tlp_controls.Controls.Add( this.b_cancel, 0, 4 );
            this.tlp_controls.Controls.Add( this.l_total_progress, 1, 1 );
            this.tlp_controls.Controls.Add( this.l_progress, 1, 3 );
            this.tlp_controls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_controls.Location = new System.Drawing.Point( 0, 0 );
            this.tlp_controls.Name = "tlp_controls";
            this.tlp_controls.RowCount = 5;
            this.tlp_controls.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 25F ) );
            this.tlp_controls.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 25F ) );
            this.tlp_controls.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 25F ) );
            this.tlp_controls.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 25F ) );
            this.tlp_controls.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Absolute, 30F ) );
            this.tlp_controls.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Absolute, 20F ) );
            this.tlp_controls.Size = new System.Drawing.Size( 318, 106 );
            this.tlp_controls.TabIndex = 3;
            // 
            // pb_total_progress
            // 
            this.tlp_controls.SetColumnSpan( this.pb_total_progress, 2 );
            this.pb_total_progress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pb_total_progress.Location = new System.Drawing.Point( 0, 0 );
            this.pb_total_progress.Margin = new System.Windows.Forms.Padding( 0 );
            this.pb_total_progress.Name = "pb_total_progress";
            this.pb_total_progress.Size = new System.Drawing.Size( 318, 19 );
            this.pb_total_progress.Step = 1;
            this.pb_total_progress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pb_total_progress.TabIndex = 3;
            // 
            // l_item
            // 
            this.l_item.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.l_item.AutoSize = true;
            this.l_item.Location = new System.Drawing.Point( 3, 22 );
            this.l_item.Name = "l_item";
            this.l_item.Size = new System.Drawing.Size( 33, 13 );
            this.l_item.TabIndex = 4;
            this.l_item.Text = "Item: ";
            // 
            // l_total_progress
            // 
            this.l_total_progress.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.l_total_progress.AutoSize = true;
            this.l_total_progress.Location = new System.Drawing.Point( 291, 22 );
            this.l_total_progress.Name = "l_total_progress";
            this.l_total_progress.Size = new System.Drawing.Size( 24, 13 );
            this.l_total_progress.TabIndex = 4;
            this.l_total_progress.Text = "0/0";
            // 
            // l_progress
            // 
            this.l_progress.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.l_progress.AutoEllipsis = true;
            this.l_progress.AutoSize = true;
            this.l_progress.BackColor = System.Drawing.SystemColors.Control;
            this.l_progress.Location = new System.Drawing.Point( 253, 60 );
            this.l_progress.Name = "l_progress";
            this.l_progress.Size = new System.Drawing.Size( 62, 13 );
            this.l_progress.TabIndex = 2;
            this.l_progress.Text = "placeholder";
            // 
            // ExtractionProgressBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.CancelButton = this.b_cancel;
            this.ClientSize = new System.Drawing.Size( 318, 106 );
            this.ControlBox = false;
            this.Controls.Add( this.tlp_controls );
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExtractionProgressBar";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Extracting...";
            this.TopMost = true;
            this.tlp_controls.ResumeLayout( false );
            this.tlp_controls.PerformLayout();
            this.ResumeLayout( false );
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button b_cancel;
        private System.Windows.Forms.ProgressBar pb_progress;
        private System.Windows.Forms.Label l_extracting_to;
        private System.Windows.Forms.TableLayoutPanel tlp_controls;
        private System.Windows.Forms.ProgressBar pb_total_progress;
        private System.Windows.Forms.Label l_item;
        private System.Windows.Forms.Label l_total_progress;
        private System.Windows.Forms.Label l_progress;
    }
}