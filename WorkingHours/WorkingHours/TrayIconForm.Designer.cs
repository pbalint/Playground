namespace WorkingHours
{
    partial class TrayIconForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrayIconForm));
            this.tray_icon = new System.Windows.Forms.NotifyIcon(this.components);
            this.tray_menu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tray_menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // tray_icon
            // 
            this.tray_icon.ContextMenuStrip = this.tray_menu;
            this.tray_icon.Icon = ((System.Drawing.Icon)(resources.GetObject("tray_icon.Icon")));
            this.tray_icon.Text = "Working Hours";
            this.tray_icon.Visible = true;
            this.tray_icon.DoubleClick += new System.EventHandler(this.showToolStripMenuItem_Click);
            // 
            // tray_menu
            // 
            this.tray_menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showToolStripMenuItem,
            this.configureToolStripMenuItem,
            this.quitToolStripMenuItem});
            this.tray_menu.Name = "TrayMenu";
            this.tray_menu.Size = new System.Drawing.Size(128, 70);
            // 
            // showToolStripMenuItem
            // 
            this.showToolStripMenuItem.Name = "showToolStripMenuItem";
            this.showToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.showToolStripMenuItem.Text = "&Show";
            this.showToolStripMenuItem.Click += new System.EventHandler(this.showToolStripMenuItem_Click);
            // 
            // configureToolStripMenuItem
            // 
            this.configureToolStripMenuItem.Name = "configureToolStripMenuItem";
            this.configureToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.configureToolStripMenuItem.Text = "&Configure";
            this.configureToolStripMenuItem.Click += new System.EventHandler(this.configureToolStripMenuItem_Click);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.quitToolStripMenuItem.Text = "&Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // TrayIconForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(216, 168);
            this.ControlBox = false;
            this.Enabled = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "TrayIconForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Dummy";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Activated += new System.EventHandler(this.TrayIconForm_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TrayIconForm_FormClosing);
            this.tray_menu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon tray_icon;
        private System.Windows.Forms.ContextMenuStrip tray_menu;
        private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
    }
}

