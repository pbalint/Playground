namespace InputPlayback
{
    partial class MouseTracker
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lMousePosition = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lMousePosition
            // 
            this.lMousePosition.AutoSize = true;
            this.lMousePosition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lMousePosition.Location = new System.Drawing.Point(0, 0);
            this.lMousePosition.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lMousePosition.Name = "lMousePosition";
            this.lMousePosition.Size = new System.Drawing.Size(0, 13);
            this.lMousePosition.TabIndex = 0;
            this.lMousePosition.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MouseTracker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(114, 13);
            this.Controls.Add(this.lMousePosition);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.MaximizeBox = false;
            this.Name = "MouseTracker";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "MouseTracker";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MouseTracker_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lMousePosition;
    }
}