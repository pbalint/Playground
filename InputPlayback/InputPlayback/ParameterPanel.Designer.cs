namespace InputPlayback
{
    partial class ParameterPanel
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
            this.panelParameters = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // panelParameters
            // 
            this.panelParameters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelParameters.Location = new System.Drawing.Point(0, 0);
            this.panelParameters.Name = "panelParameters";
            this.panelParameters.Size = new System.Drawing.Size(150, 150);
            this.panelParameters.TabIndex = 0;
            // 
            // ParameterPanel
            // 
            this.Controls.Add(this.panelParameters);
            this.Name = "ParameterPanel";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel panelParameters;
    }
}
