namespace Capture
{
    partial class CaptureForm
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
            this.tlpControls = new System.Windows.Forms.TableLayoutPanel();
            this.cbDevices = new System.Windows.Forms.ComboBox();
            this.pbImage = new System.Windows.Forms.PictureBox();
            this.bPlay = new System.Windows.Forms.Button();
            this.cbFormat = new System.Windows.Forms.ComboBox();
            this.tlpControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
            this.SuspendLayout();
            // 
            // tlpControls
            // 
            this.tlpControls.ColumnCount = 3;
            this.tlpControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 44.88119F));
            this.tlpControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55.11881F));
            this.tlpControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 71F));
            this.tlpControls.Controls.Add(this.cbDevices, 0, 0);
            this.tlpControls.Controls.Add(this.pbImage, 0, 1);
            this.tlpControls.Controls.Add(this.bPlay, 2, 0);
            this.tlpControls.Controls.Add(this.cbFormat, 1, 0);
            this.tlpControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpControls.Location = new System.Drawing.Point(0, 0);
            this.tlpControls.Name = "tlpControls";
            this.tlpControls.RowCount = 2;
            this.tlpControls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tlpControls.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpControls.Size = new System.Drawing.Size(577, 383);
            this.tlpControls.TabIndex = 0;
            // 
            // cbDevices
            // 
            this.cbDevices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbDevices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDevices.FormattingEnabled = true;
            this.cbDevices.Location = new System.Drawing.Point(3, 3);
            this.cbDevices.Name = "cbDevices";
            this.cbDevices.Size = new System.Drawing.Size(221, 21);
            this.cbDevices.TabIndex = 0;
            this.cbDevices.SelectedValueChanged += new System.EventHandler(this.cbDevices_SelectedValueChanged);
            // 
            // pbImage
            // 
            this.tlpControls.SetColumnSpan(this.pbImage, 3);
            this.pbImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbImage.Location = new System.Drawing.Point(3, 35);
            this.pbImage.Name = "pbImage";
            this.pbImage.Size = new System.Drawing.Size(571, 345);
            this.pbImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbImage.TabIndex = 2;
            this.pbImage.TabStop = false;
            this.pbImage.DoubleClick += new System.EventHandler(this.pbImage_DoubleClick);
            // 
            // bPlay
            // 
            this.bPlay.Location = new System.Drawing.Point(508, 3);
            this.bPlay.Name = "bPlay";
            this.bPlay.Size = new System.Drawing.Size(64, 23);
            this.bPlay.TabIndex = 1;
            this.bPlay.Text = "Play";
            this.bPlay.UseVisualStyleBackColor = true;
            this.bPlay.Click += new System.EventHandler(this.bPlay_Click);
            // 
            // cbFormat
            // 
            this.cbFormat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFormat.FormattingEnabled = true;
            this.cbFormat.Location = new System.Drawing.Point(230, 3);
            this.cbFormat.Name = "cbFormat";
            this.cbFormat.Size = new System.Drawing.Size(272, 21);
            this.cbFormat.TabIndex = 3;
            // 
            // CaptureForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(577, 383);
            this.Controls.Add(this.tlpControls);
            this.Name = "CaptureForm";
            this.Text = "Capture";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.tlpControls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpControls;
        private System.Windows.Forms.ComboBox cbDevices;
        private System.Windows.Forms.Button bPlay;
        private System.Windows.Forms.PictureBox pbImage;
        private System.Windows.Forms.ComboBox cbFormat;
    }
}

