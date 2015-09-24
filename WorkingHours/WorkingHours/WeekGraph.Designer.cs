namespace WorkingHours
{
    partial class WeekGraph
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
            this.pb_image = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pb_image)).BeginInit();
            this.SuspendLayout();
            // 
            // pb_image
            // 
            this.pb_image.BackColor = System.Drawing.SystemColors.Control;
            this.pb_image.Location = new System.Drawing.Point(0, 0);
            this.pb_image.Margin = new System.Windows.Forms.Padding(0);
            this.pb_image.Name = "pb_image";
            this.pb_image.Size = new System.Drawing.Size(256, 256);
            this.pb_image.TabIndex = 0;
            this.pb_image.TabStop = false;
            // 
            // WeekGraph
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.pb_image);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "WeekGraph";
            this.Size = new System.Drawing.Size(259, 259);
            ((System.ComponentModel.ISupportInitialize)(this.pb_image)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pb_image;
    }
}
