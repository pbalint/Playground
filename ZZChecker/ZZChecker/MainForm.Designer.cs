namespace ZZChecker
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.browser = new System.Windows.Forms.WebBrowser();
            this.tray_icon = new System.Windows.Forms.NotifyIcon(this.components);
            this.tray_menu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.credentialsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tray_menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // browser
            // 
            this.browser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.browser.Location = new System.Drawing.Point(0, 0);
            this.browser.MinimumSize = new System.Drawing.Size(20, 20);
            this.browser.Name = "browser";
            this.browser.Size = new System.Drawing.Size(716, 613);
            this.browser.TabIndex = 0;
            this.browser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.browser_DocumentCompleted);
            // 
            // tray_icon
            // 
            this.tray_icon.ContextMenuStrip = this.tray_menu;
            this.tray_icon.Icon = ((System.Drawing.Icon)(resources.GetObject("tray_icon.Icon")));
            this.tray_icon.Visible = true;
            this.tray_icon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tray_icon_MouseDoubleClick);
            // 
            // tray_menu
            // 
            this.tray_menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.credentialsToolStripMenuItem,
            this.quitToolStripMenuItem});
            this.tray_menu.Name = "tray_menu";
            this.tray_menu.ShowImageMargin = false;
            this.tray_menu.Size = new System.Drawing.Size(109, 48);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.quitToolStripMenuItem.Text = "&Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // credentialsToolStripMenuItem
            // 
            this.credentialsToolStripMenuItem.Name = "credentialsToolStripMenuItem";
            this.credentialsToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.credentialsToolStripMenuItem.Text = "&Credentials";
            this.credentialsToolStripMenuItem.Click += new System.EventHandler(this.credentialsToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(716, 613);
            this.Controls.Add(this.browser);
            this.Name = "MainForm";
            this.ShowInTaskbar = false;
            this.Text = "ZZ Checker";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.tray_menu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser browser;
        private System.Windows.Forms.NotifyIcon tray_icon;
        private System.Windows.Forms.ContextMenuStrip tray_menu;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem credentialsToolStripMenuItem;
    }
}

