namespace Extplorer
{
    partial class MainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.tlp_controls = new System.Windows.Forms.TableLayoutPanel();
            this.main_menu = new System.Windows.Forms.MenuStrip();
            this.menu_file = new System.Windows.Forms.ToolStripMenuItem();
            this.mi_open = new System.Windows.Forms.ToolStripMenuItem();
            this.mi_extract = new System.Windows.Forms.ToolStripMenuItem();
            this.mi_refresh = new System.Windows.Forms.ToolStripMenuItem();
            this.mi_exit = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_edit = new System.Windows.Forms.ToolStripMenuItem();
            this.cut = new System.Windows.Forms.ToolStripMenuItem();
            this.copy = new System.Windows.Forms.ToolStripMenuItem();
            this.paste = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_help = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolbar = new System.Windows.Forms.ToolStrip();
            this.ts_refresh = new System.Windows.Forms.ToolStripButton();
            this.ts_cut = new System.Windows.Forms.ToolStripButton();
            this.ts_copy = new System.Windows.Forms.ToolStripButton();
            this.ts_paste = new System.Windows.Forms.ToolStripButton();
            this.ts_open = new System.Windows.Forms.ToolStripButton();
            this.ts_extract = new System.Windows.Forms.ToolStripButton();
            this.statusbar = new System.Windows.Forms.StatusBar();
            this.sc_views = new System.Windows.Forms.SplitContainer();
            this.tv_dirs = new System.Windows.Forms.TreeView();
            this.cms_file_menu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.context_extract = new System.Windows.Forms.ToolStripMenuItem();
            this.il_tv_images = new System.Windows.Forms.ImageList(this.components);
            this.lv_files = new System.Windows.Forms.ListView();
            this.tlp_controls.SuspendLayout();
            this.main_menu.SuspendLayout();
            this.toolbar.SuspendLayout();
            this.sc_views.Panel1.SuspendLayout();
            this.sc_views.Panel2.SuspendLayout();
            this.sc_views.SuspendLayout();
            this.cms_file_menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlp_controls
            // 
            this.tlp_controls.ColumnCount = 1;
            this.tlp_controls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_controls.Controls.Add(this.main_menu, 0, 0);
            this.tlp_controls.Controls.Add(this.toolbar, 0, 1);
            this.tlp_controls.Controls.Add(this.statusbar, 0, 3);
            this.tlp_controls.Controls.Add(this.sc_views, 0, 2);
            this.tlp_controls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_controls.Location = new System.Drawing.Point(0, 0);
            this.tlp_controls.Margin = new System.Windows.Forms.Padding(0);
            this.tlp_controls.Name = "tlp_controls";
            this.tlp_controls.RowCount = 4;
            this.tlp_controls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tlp_controls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tlp_controls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_controls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlp_controls.Size = new System.Drawing.Size(556, 377);
            this.tlp_controls.TabIndex = 0;
            // 
            // main_menu
            // 
            this.main_menu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.main_menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_file,
            this.menu_edit,
            this.menu_help});
            this.main_menu.Location = new System.Drawing.Point(0, 0);
            this.main_menu.Name = "main_menu";
            this.main_menu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.main_menu.Size = new System.Drawing.Size(556, 24);
            this.main_menu.TabIndex = 7;
            // 
            // menu_file
            // 
            this.menu_file.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mi_open,
            this.mi_extract,
            this.mi_refresh,
            this.mi_exit});
            this.menu_file.Name = "menu_file";
            this.menu_file.Size = new System.Drawing.Size(35, 20);
            this.menu_file.Text = "&File";
            // 
            // mi_open
            // 
            this.mi_open.Name = "mi_open";
            this.mi_open.Size = new System.Drawing.Size(152, 22);
            this.mi_open.Text = "&Open disk image";
            this.mi_open.Click += new System.EventHandler(this.ts_open_Click);
            // 
            // mi_extract
            // 
            this.mi_extract.Name = "mi_extract";
            this.mi_extract.Size = new System.Drawing.Size(152, 22);
            this.mi_extract.Text = "E&xtract";
            this.mi_extract.Click += new System.EventHandler(this.ts_extract_Click);
            // 
            // mi_refresh
            // 
            this.mi_refresh.Name = "mi_refresh";
            this.mi_refresh.Size = new System.Drawing.Size(152, 22);
            this.mi_refresh.Text = "&Refresh";
            this.mi_refresh.Click += new System.EventHandler(this.ts_refresh_Click);
            // 
            // mi_exit
            // 
            this.mi_exit.Name = "mi_exit";
            this.mi_exit.Size = new System.Drawing.Size(152, 22);
            this.mi_exit.Text = "&Exit";
            this.mi_exit.Click += new System.EventHandler(this.mi_exit_Click);
            // 
            // menu_edit
            // 
            this.menu_edit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cut,
            this.copy,
            this.paste});
            this.menu_edit.Name = "menu_edit";
            this.menu_edit.Size = new System.Drawing.Size(37, 20);
            this.menu_edit.Text = "&Edit";
            this.menu_edit.Visible = false;
            // 
            // cut
            // 
            this.cut.Name = "cut";
            this.cut.Size = new System.Drawing.Size(101, 22);
            this.cut.Text = "Cu&t";
            // 
            // copy
            // 
            this.copy.Name = "copy";
            this.copy.Size = new System.Drawing.Size(101, 22);
            this.copy.Text = "&Copy";
            // 
            // paste
            // 
            this.paste.Name = "paste";
            this.paste.Size = new System.Drawing.Size(101, 22);
            this.paste.Text = "&Paste";
            // 
            // menu_help
            // 
            this.menu_help.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.menu_help.Name = "menu_help";
            this.menu_help.Size = new System.Drawing.Size(40, 20);
            this.menu_help.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // toolbar
            // 
            this.toolbar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolbar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ts_refresh,
            this.ts_cut,
            this.ts_copy,
            this.ts_paste,
            this.ts_open,
            this.ts_extract});
            this.toolbar.Location = new System.Drawing.Point(0, 24);
            this.toolbar.Name = "toolbar";
            this.toolbar.Padding = new System.Windows.Forms.Padding(0);
            this.toolbar.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolbar.Size = new System.Drawing.Size(556, 24);
            this.toolbar.TabIndex = 6;
            // 
            // ts_refresh
            // 
            this.ts_refresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ts_refresh.Image = ((System.Drawing.Image)(resources.GetObject("ts_refresh.Image")));
            this.ts_refresh.Name = "ts_refresh";
            this.ts_refresh.Size = new System.Drawing.Size(23, 21);
            this.ts_refresh.ToolTipText = "Refresh";
            this.ts_refresh.Click += new System.EventHandler(this.ts_refresh_Click);
            // 
            // ts_cut
            // 
            this.ts_cut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ts_cut.Enabled = false;
            this.ts_cut.Image = ((System.Drawing.Image)(resources.GetObject("ts_cut.Image")));
            this.ts_cut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_cut.Name = "ts_cut";
            this.ts_cut.Size = new System.Drawing.Size(23, 21);
            this.ts_cut.Text = "C&ut";
            this.ts_cut.Visible = false;
            // 
            // ts_copy
            // 
            this.ts_copy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ts_copy.Enabled = false;
            this.ts_copy.Image = ((System.Drawing.Image)(resources.GetObject("ts_copy.Image")));
            this.ts_copy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_copy.Name = "ts_copy";
            this.ts_copy.Size = new System.Drawing.Size(23, 21);
            this.ts_copy.Text = "&Copy";
            this.ts_copy.Visible = false;
            // 
            // ts_paste
            // 
            this.ts_paste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ts_paste.Enabled = false;
            this.ts_paste.Image = ((System.Drawing.Image)(resources.GetObject("ts_paste.Image")));
            this.ts_paste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_paste.Name = "ts_paste";
            this.ts_paste.Size = new System.Drawing.Size(23, 21);
            this.ts_paste.Text = "&Paste";
            this.ts_paste.Visible = false;
            // 
            // ts_open
            // 
            this.ts_open.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ts_open.Image = ((System.Drawing.Image)(resources.GetObject("ts_open.Image")));
            this.ts_open.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.ts_open.Name = "ts_open";
            this.ts_open.Size = new System.Drawing.Size(23, 21);
            this.ts_open.Text = "&Open disk image";
            this.ts_open.Click += new System.EventHandler(this.ts_open_Click);
            // 
            // ts_extract
            // 
            this.ts_extract.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ts_extract.Image = ((System.Drawing.Image)(resources.GetObject("ts_extract.Image")));
            this.ts_extract.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.ts_extract.Name = "ts_extract";
            this.ts_extract.Size = new System.Drawing.Size(23, 21);
            this.ts_extract.Text = "&Save item";
            this.ts_extract.Click += new System.EventHandler(this.ts_extract_Click);
            // 
            // statusbar
            // 
            this.statusbar.Location = new System.Drawing.Point(0, 362);
            this.statusbar.Margin = new System.Windows.Forms.Padding(0);
            this.statusbar.Name = "statusbar";
            this.statusbar.Size = new System.Drawing.Size(556, 15);
            this.statusbar.SizingGrip = false;
            this.statusbar.TabIndex = 5;
            // 
            // sc_views
            // 
            this.sc_views.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sc_views.Location = new System.Drawing.Point(3, 51);
            this.sc_views.Name = "sc_views";
            // 
            // sc_views.Panel1
            // 
            this.sc_views.Panel1.Controls.Add(this.tv_dirs);
            // 
            // sc_views.Panel2
            // 
            this.sc_views.Panel2.Controls.Add(this.lv_files);
            this.sc_views.Size = new System.Drawing.Size(550, 303);
            this.sc_views.SplitterDistance = 211;
            this.sc_views.SplitterWidth = 3;
            this.sc_views.TabIndex = 3;
            this.sc_views.TabStop = false;
            // 
            // tv_dirs
            // 
            this.tv_dirs.AllowDrop = true;
            this.tv_dirs.CausesValidation = false;
            this.tv_dirs.ContextMenuStrip = this.cms_file_menu;
            this.tv_dirs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tv_dirs.HideSelection = false;
            this.tv_dirs.ImageIndex = 0;
            this.tv_dirs.ImageList = this.il_tv_images;
            this.tv_dirs.Location = new System.Drawing.Point(0, 0);
            this.tv_dirs.Name = "tv_dirs";
            this.tv_dirs.PathSeparator = "/";
            this.tv_dirs.SelectedImageIndex = 0;
            this.tv_dirs.Size = new System.Drawing.Size(211, 303);
            this.tv_dirs.TabIndex = 0;
            this.tv_dirs.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.tv_dirs_AfterCollapse);
            this.tv_dirs.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.tv_dirs_BeforeExpand);
            this.tv_dirs.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tv_dirs_AfterSelect);
            this.tv_dirs.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tv_dirs_NodeMouseClick);
            // 
            // cms_file_menu
            // 
            this.cms_file_menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.context_extract});
            this.cms_file_menu.Name = "cms_file_menu";
            this.cms_file_menu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.cms_file_menu.Size = new System.Drawing.Size(110, 26);
            // 
            // context_extract
            // 
            this.context_extract.Name = "context_extract";
            this.context_extract.Size = new System.Drawing.Size(109, 22);
            this.context_extract.Text = "E&xtract";
            this.context_extract.Click += new System.EventHandler(this.ts_extract_Click);
            // 
            // il_tv_images
            // 
            this.il_tv_images.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("il_tv_images.ImageStream")));
            this.il_tv_images.TransparentColor = System.Drawing.Color.Transparent;
            this.il_tv_images.Images.SetKeyName(0, "disk.ico");
            this.il_tv_images.Images.SetKeyName(1, "dir.ico");
            this.il_tv_images.Images.SetKeyName(2, "dir_selected.ico");
            this.il_tv_images.Images.SetKeyName(3, "file.ico");
            // 
            // lv_files
            // 
            this.lv_files.ContextMenuStrip = this.cms_file_menu;
            this.lv_files.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lv_files.FullRowSelect = true;
            this.lv_files.HideSelection = false;
            this.lv_files.LabelWrap = false;
            this.lv_files.Location = new System.Drawing.Point(0, 0);
            this.lv_files.Name = "lv_files";
            this.lv_files.ShowGroups = false;
            this.lv_files.Size = new System.Drawing.Size(336, 303);
            this.lv_files.SmallImageList = this.il_tv_images;
            this.lv_files.TabIndex = 0;
            this.lv_files.UseCompatibleStateImageBehavior = false;
            this.lv_files.View = System.Windows.Forms.View.List;
            this.lv_files.DoubleClick += new System.EventHandler(this.lv_files_DoubleClick);
            // 
            // MainWindow
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(556, 377);
            this.Controls.Add(this.tlp_controls);
            this.DoubleBuffered = true;
            this.MainMenuStrip = this.main_menu;
            this.Name = "MainWindow";
            this.Text = "Extplorer";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainWindow_FormClosed);
            this.tlp_controls.ResumeLayout(false);
            this.tlp_controls.PerformLayout();
            this.main_menu.ResumeLayout(false);
            this.main_menu.PerformLayout();
            this.toolbar.ResumeLayout(false);
            this.toolbar.PerformLayout();
            this.sc_views.Panel1.ResumeLayout(false);
            this.sc_views.Panel2.ResumeLayout(false);
            this.sc_views.ResumeLayout(false);
            this.cms_file_menu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlp_controls;
        private System.Windows.Forms.SplitContainer sc_views;
        private System.Windows.Forms.TreeView tv_dirs;
        private System.Windows.Forms.ListView lv_files;
        private System.Windows.Forms.ToolStrip toolbar;
        private System.Windows.Forms.ToolStripButton ts_refresh;
        private System.Windows.Forms.ContextMenuStrip cms_file_menu;
        private System.Windows.Forms.MenuStrip main_menu;
        private System.Windows.Forms.ToolStripMenuItem menu_file;
        private System.Windows.Forms.ToolStripMenuItem mi_exit;
        private System.Windows.Forms.ToolStripMenuItem menu_edit;
        private System.Windows.Forms.ToolStripMenuItem cut;
        private System.Windows.Forms.ToolStripMenuItem copy;
        private System.Windows.Forms.ToolStripMenuItem paste;
        private System.Windows.Forms.ToolStripMenuItem menu_help;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mi_extract;
        private System.Windows.Forms.ToolStripMenuItem context_extract;
        private System.Windows.Forms.ToolStripMenuItem mi_refresh;
        private System.Windows.Forms.ToolStripMenuItem mi_open;
        private System.Windows.Forms.ImageList il_tv_images;
        private System.Windows.Forms.ToolStripButton ts_extract;
        private System.Windows.Forms.ToolStripButton ts_cut;
        private System.Windows.Forms.ToolStripButton ts_copy;
        private System.Windows.Forms.ToolStripButton ts_paste;
        private System.Windows.Forms.ToolStripButton ts_open;
        private System.Windows.Forms.StatusBar statusbar;

    }
}