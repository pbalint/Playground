namespace InputPlayback
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.statusStripStatus = new System.Windows.Forms.StatusStrip();
            this.statusLabelMouseCoordinates = new System.Windows.Forms.ToolStripStatusLabel();
            this.listViewsteps = new System.Windows.Forms.ListView();
            this.columnHeaderIndex = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tlpControls = new System.Windows.Forms.TableLayoutPanel();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.menuStripMenu = new System.Windows.Forms.MenuStrip();
            this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSave = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.comboBoxAction = new System.Windows.Forms.ComboBox();
            this.buttonPlay = new System.Windows.Forms.Button();
            this.panelActionParameters = new System.Windows.Forms.FlowLayoutPanel();
            this.mouseTrackerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStripStatus.SuspendLayout();
            this.tlpControls.SuspendLayout();
            this.menuStripMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStripStatus
            // 
            this.statusStripStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabelMouseCoordinates});
            this.statusStripStatus.Location = new System.Drawing.Point(0, 247);
            this.statusStripStatus.Name = "statusStripStatus";
            this.statusStripStatus.Size = new System.Drawing.Size(592, 22);
            this.statusStripStatus.TabIndex = 0;
            this.statusStripStatus.Text = "statusStrip1";
            // 
            // statusLabelMouseCoordinates
            // 
            this.statusLabelMouseCoordinates.Name = "statusLabelMouseCoordinates";
            this.statusLabelMouseCoordinates.Size = new System.Drawing.Size(0, 17);
            // 
            // listViewsteps
            // 
            this.listViewsteps.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderIndex,
            this.columnHeaderDescription});
            this.tlpControls.SetColumnSpan(this.listViewsteps, 3);
            this.listViewsteps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewsteps.FullRowSelect = true;
            this.listViewsteps.GridLines = true;
            this.listViewsteps.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewsteps.Location = new System.Drawing.Point(53, 51);
            this.listViewsteps.Name = "listViewsteps";
            this.tlpControls.SetRowSpan(this.listViewsteps, 4);
            this.listViewsteps.Size = new System.Drawing.Size(536, 193);
            this.listViewsteps.TabIndex = 0;
            this.listViewsteps.UseCompatibleStateImageBehavior = false;
            this.listViewsteps.View = System.Windows.Forms.View.Details;
            this.listViewsteps.SelectedIndexChanged += new System.EventHandler(this.listViewsteps_SelectedIndexChanged);
            // 
            // columnHeaderIndex
            // 
            this.columnHeaderIndex.Text = "#";
            // 
            // columnHeaderDescription
            // 
            this.columnHeaderDescription.Text = "";
            // 
            // tlpControls
            // 
            this.tlpControls.ColumnCount = 4;
            this.tlpControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tlpControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tlpControls.Controls.Add(this.buttonDelete, 0, 3);
            this.tlpControls.Controls.Add(this.buttonEdit, 0, 2);
            this.tlpControls.Controls.Add(this.menuStripMenu, 0, 0);
            this.tlpControls.Controls.Add(this.listViewsteps, 1, 2);
            this.tlpControls.Controls.Add(this.buttonAdd, 0, 1);
            this.tlpControls.Controls.Add(this.comboBoxAction, 1, 1);
            this.tlpControls.Controls.Add(this.buttonPlay, 0, 5);
            this.tlpControls.Controls.Add(this.panelActionParameters, 2, 1);
            this.tlpControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpControls.Location = new System.Drawing.Point(0, 0);
            this.tlpControls.Margin = new System.Windows.Forms.Padding(0);
            this.tlpControls.Name = "tlpControls";
            this.tlpControls.RowCount = 6;
            this.tlpControls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tlpControls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tlpControls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tlpControls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tlpControls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpControls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpControls.Size = new System.Drawing.Size(592, 247);
            this.tlpControls.TabIndex = 2;
            // 
            // buttonDelete
            // 
            this.buttonDelete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonDelete.Location = new System.Drawing.Point(0, 72);
            this.buttonDelete.Margin = new System.Windows.Forms.Padding(0);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(50, 24);
            this.buttonDelete.TabIndex = 4;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonEdit
            // 
            this.buttonEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonEdit.Enabled = false;
            this.buttonEdit.Location = new System.Drawing.Point(0, 48);
            this.buttonEdit.Margin = new System.Windows.Forms.Padding(0);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(50, 24);
            this.buttonEdit.TabIndex = 3;
            this.buttonEdit.Text = "Edit";
            this.buttonEdit.UseVisualStyleBackColor = true;
            this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
            // 
            // menuStripMenu
            // 
            this.tlpControls.SetColumnSpan(this.menuStripMenu, 4);
            this.menuStripMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.toolsToolStripMenuItem});
            this.menuStripMenu.Location = new System.Drawing.Point(0, 0);
            this.menuStripMenu.Name = "menuStripMenu";
            this.menuStripMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuStripMenu.Size = new System.Drawing.Size(592, 24);
            this.menuStripMenu.TabIndex = 2;
            // 
            // menuFile
            // 
            this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemOpen,
            this.menuItemSave,
            this.menuItemExit});
            this.menuFile.Name = "menuFile";
            this.menuFile.Size = new System.Drawing.Size(37, 20);
            this.menuFile.Text = "&File";
            // 
            // menuItemOpen
            // 
            this.menuItemOpen.Image = ((System.Drawing.Image)(resources.GetObject("menuItemOpen.Image")));
            this.menuItemOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.menuItemOpen.Name = "menuItemOpen";
            this.menuItemOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.menuItemOpen.Size = new System.Drawing.Size(146, 22);
            this.menuItemOpen.Text = "&Open";
            // 
            // menuItemSave
            // 
            this.menuItemSave.Image = ((System.Drawing.Image)(resources.GetObject("menuItemSave.Image")));
            this.menuItemSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.menuItemSave.Name = "menuItemSave";
            this.menuItemSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.menuItemSave.Size = new System.Drawing.Size(146, 22);
            this.menuItemSave.Text = "&Save";
            // 
            // menuItemExit
            // 
            this.menuItemExit.Name = "menuItemExit";
            this.menuItemExit.Size = new System.Drawing.Size(146, 22);
            this.menuItemExit.Text = "E&xit";
            this.menuItemExit.Click += new System.EventHandler(this.menuItemExit_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mouseTrackerToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // buttonAdd
            // 
            this.buttonAdd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonAdd.Location = new System.Drawing.Point(0, 24);
            this.buttonAdd.Margin = new System.Windows.Forms.Padding(0);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(50, 24);
            this.buttonAdd.TabIndex = 1;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // comboBoxAction
            // 
            this.comboBoxAction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAction.FormattingEnabled = true;
            this.comboBoxAction.Location = new System.Drawing.Point(53, 27);
            this.comboBoxAction.Name = "comboBoxAction";
            this.comboBoxAction.Size = new System.Drawing.Size(144, 21);
            this.comboBoxAction.TabIndex = 5;
            this.comboBoxAction.SelectedValueChanged += new System.EventHandler(this.comboBoxAction_SelectedValueChanged);
            // 
            // buttonPlay
            // 
            this.buttonPlay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonPlay.Location = new System.Drawing.Point(0, 207);
            this.buttonPlay.Margin = new System.Windows.Forms.Padding(0);
            this.buttonPlay.Name = "buttonPlay";
            this.buttonPlay.Size = new System.Drawing.Size(50, 40);
            this.buttonPlay.TabIndex = 7;
            this.buttonPlay.Text = "Play";
            this.buttonPlay.UseVisualStyleBackColor = true;
            this.buttonPlay.Click += new System.EventHandler(this.buttonPlay_Click);
            // 
            // panelActionParameters
            // 
            this.tlpControls.SetColumnSpan(this.panelActionParameters, 2);
            this.panelActionParameters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelActionParameters.Location = new System.Drawing.Point(200, 24);
            this.panelActionParameters.Margin = new System.Windows.Forms.Padding(0);
            this.panelActionParameters.Name = "panelActionParameters";
            this.panelActionParameters.Size = new System.Drawing.Size(392, 24);
            this.panelActionParameters.TabIndex = 8;
            // 
            // mouseTrackerToolStripMenuItem
            // 
            this.mouseTrackerToolStripMenuItem.Name = "mouseTrackerToolStripMenuItem";
            this.mouseTrackerToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.mouseTrackerToolStripMenuItem.Text = "&Mouse tracker";
            this.mouseTrackerToolStripMenuItem.Click += new System.EventHandler(this.mouseTrackerToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 269);
            this.Controls.Add(this.tlpControls);
            this.Controls.Add(this.statusStripStatus);
            this.Name = "MainForm";
            this.Text = "Input Playback";
            this.statusStripStatus.ResumeLayout(false);
            this.statusStripStatus.PerformLayout();
            this.tlpControls.ResumeLayout(false);
            this.tlpControls.PerformLayout();
            this.menuStripMenu.ResumeLayout(false);
            this.menuStripMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStripStatus;
        private System.Windows.Forms.ToolStripStatusLabel statusLabelMouseCoordinates;
        private System.Windows.Forms.ListView listViewsteps;
        private System.Windows.Forms.ColumnHeader columnHeaderIndex;
        private System.Windows.Forms.ColumnHeader columnHeaderDescription;
        private System.Windows.Forms.TableLayoutPanel tlpControls;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.MenuStrip menuStripMenu;
        private System.Windows.Forms.ToolStripMenuItem menuFile;
        private System.Windows.Forms.ToolStripMenuItem menuItemOpen;
        private System.Windows.Forms.ToolStripMenuItem menuItemSave;
        private System.Windows.Forms.ToolStripMenuItem menuItemExit;
        private System.Windows.Forms.ComboBox comboBoxAction;
        private System.Windows.Forms.Button buttonPlay;
        private System.Windows.Forms.FlowLayoutPanel panelActionParameters;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mouseTrackerToolStripMenuItem;
    }
}

