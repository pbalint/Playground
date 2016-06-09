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
            this.tlpControls = new System.Windows.Forms.TableLayoutPanel();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.menuStripMenu = new System.Windows.Forms.MenuStrip();
            this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSave = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mouseTrackerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.comboBoxAction = new System.Windows.Forms.ComboBox();
            this.buttonPlay = new System.Windows.Forms.Button();
            this.panelActionParameters = new InputPlayback.ParameterPanel();
            this.listViewSteps = new System.Windows.Forms.ListView();
            this.columnHeaderIndex = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderActon = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderParameters = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tlpControls.SuspendLayout();
            this.menuStripMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpControls
            // 
            this.tlpControls.ColumnCount = 4;
            this.tlpControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tlpControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tlpControls.Controls.Add(this.buttonDelete, 0, 3);
            this.tlpControls.Controls.Add(this.buttonUpdate, 0, 2);
            this.tlpControls.Controls.Add(this.menuStripMenu, 0, 0);
            this.tlpControls.Controls.Add(this.buttonAdd, 0, 1);
            this.tlpControls.Controls.Add(this.comboBoxAction, 1, 1);
            this.tlpControls.Controls.Add(this.buttonPlay, 0, 5);
            this.tlpControls.Controls.Add(this.panelActionParameters, 2, 1);
            this.tlpControls.Controls.Add(this.listViewSteps, 1, 2);
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
            this.tlpControls.Size = new System.Drawing.Size(592, 269);
            this.tlpControls.TabIndex = 2;
            // 
            // buttonDelete
            // 
            this.buttonDelete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonDelete.Location = new System.Drawing.Point(0, 72);
            this.buttonDelete.Margin = new System.Windows.Forms.Padding(0);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(50, 24);
            this.buttonDelete.TabIndex = 2;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonUpdate.Enabled = false;
            this.buttonUpdate.Location = new System.Drawing.Point(0, 48);
            this.buttonUpdate.Margin = new System.Windows.Forms.Padding(0);
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Size = new System.Drawing.Size(50, 24);
            this.buttonUpdate.TabIndex = 1;
            this.buttonUpdate.Text = "Update";
            this.buttonUpdate.UseVisualStyleBackColor = true;
            this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
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
            this.menuStripMenu.TabIndex = 7;
            // 
            // menuFile
            // 
            this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemOpen,
            this.menuItemSave,
            this.menuItemExit});
            this.menuFile.Name = "menuFile";
            this.menuFile.Size = new System.Drawing.Size(35, 20);
            this.menuFile.Text = "&File";
            // 
            // menuItemOpen
            // 
            this.menuItemOpen.Image = ((System.Drawing.Image)(resources.GetObject("menuItemOpen.Image")));
            this.menuItemOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.menuItemOpen.Name = "menuItemOpen";
            this.menuItemOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.menuItemOpen.Size = new System.Drawing.Size(140, 22);
            this.menuItemOpen.Text = "&Open";
            this.menuItemOpen.Click += new System.EventHandler(this.menuItemOpen_Click);
            // 
            // menuItemSave
            // 
            this.menuItemSave.Image = ((System.Drawing.Image)(resources.GetObject("menuItemSave.Image")));
            this.menuItemSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.menuItemSave.Name = "menuItemSave";
            this.menuItemSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.menuItemSave.Size = new System.Drawing.Size(140, 22);
            this.menuItemSave.Text = "&Save";
            this.menuItemSave.Click += new System.EventHandler(this.menuItemSave_Click);
            // 
            // menuItemExit
            // 
            this.menuItemExit.Name = "menuItemExit";
            this.menuItemExit.Size = new System.Drawing.Size(140, 22);
            this.menuItemExit.Text = "E&xit";
            this.menuItemExit.Click += new System.EventHandler(this.menuItemExit_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mouseTrackerToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // mouseTrackerToolStripMenuItem
            // 
            this.mouseTrackerToolStripMenuItem.Name = "mouseTrackerToolStripMenuItem";
            this.mouseTrackerToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.mouseTrackerToolStripMenuItem.Text = "&Mouse tracker";
            this.mouseTrackerToolStripMenuItem.Click += new System.EventHandler(this.mouseTrackerToolStripMenuItem_Click);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonAdd.Location = new System.Drawing.Point(0, 24);
            this.buttonAdd.Margin = new System.Windows.Forms.Padding(0);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(50, 24);
            this.buttonAdd.TabIndex = 0;
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
            this.comboBoxAction.Sorted = true;
            this.comboBoxAction.TabIndex = 4;
            this.comboBoxAction.SelectedValueChanged += new System.EventHandler(this.comboBoxAction_SelectedValueChanged);
            // 
            // buttonPlay
            // 
            this.buttonPlay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonPlay.Location = new System.Drawing.Point(0, 229);
            this.buttonPlay.Margin = new System.Windows.Forms.Padding(0);
            this.buttonPlay.Name = "buttonPlay";
            this.buttonPlay.Size = new System.Drawing.Size(50, 40);
            this.buttonPlay.TabIndex = 3;
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
            this.panelActionParameters.TabIndex = 5;
            // 
            // listViewSteps
            // 
            this.listViewSteps.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderIndex,
            this.columnHeaderActon,
            this.columnHeaderParameters});
            this.tlpControls.SetColumnSpan(this.listViewSteps, 3);
            this.listViewSteps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewSteps.FullRowSelect = true;
            this.listViewSteps.GridLines = true;
            this.listViewSteps.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewSteps.HideSelection = false;
            this.listViewSteps.Location = new System.Drawing.Point(53, 51);
            this.listViewSteps.MultiSelect = false;
            this.listViewSteps.Name = "listViewSteps";
            this.tlpControls.SetRowSpan(this.listViewSteps, 4);
            this.listViewSteps.Size = new System.Drawing.Size(536, 215);
            this.listViewSteps.TabIndex = 6;
            this.listViewSteps.UseCompatibleStateImageBehavior = false;
            this.listViewSteps.View = System.Windows.Forms.View.Details;
            this.listViewSteps.SelectedIndexChanged += new System.EventHandler(this.listViewSteps_SelectedIndexChanged);
            // 
            // columnHeaderIndex
            // 
            this.columnHeaderIndex.Text = "#";
            this.columnHeaderIndex.Width = 20;
            // 
            // columnHeaderActon
            // 
            this.columnHeaderActon.Text = "Action";
            this.columnHeaderActon.Width = 100;
            // 
            // columnHeaderParameters
            // 
            this.columnHeaderParameters.Text = "Parameters";
            this.columnHeaderParameters.Width = 250;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 269);
            this.Controls.Add(this.tlpControls);
            this.Name = "MainForm";
            this.Text = "Input Playback";
            this.tlpControls.ResumeLayout(false);
            this.tlpControls.PerformLayout();
            this.menuStripMenu.ResumeLayout(false);
            this.menuStripMenu.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpControls;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonUpdate;
        private System.Windows.Forms.MenuStrip menuStripMenu;
        private System.Windows.Forms.ToolStripMenuItem menuFile;
        private System.Windows.Forms.ToolStripMenuItem menuItemOpen;
        private System.Windows.Forms.ToolStripMenuItem menuItemSave;
        private System.Windows.Forms.ToolStripMenuItem menuItemExit;
        private System.Windows.Forms.ComboBox comboBoxAction;
        private System.Windows.Forms.Button buttonPlay;
        private ParameterPanel panelActionParameters;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mouseTrackerToolStripMenuItem;
        private System.Windows.Forms.ListView listViewSteps;
        private System.Windows.Forms.ColumnHeader columnHeaderIndex;
        private System.Windows.Forms.ColumnHeader columnHeaderActon;
        private System.Windows.Forms.ColumnHeader columnHeaderParameters;
    }
}

