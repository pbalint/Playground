namespace WorkingHours
{
    partial class ConfigurationForm
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
            this.b_ok = new System.Windows.Forms.Button();
            this.b_cancel = new System.Windows.Forms.Button();
            this.c_work_hours = new System.Windows.Forms.NumericUpDown();
            this.c_unlocked_hours = new System.Windows.Forms.NumericUpDown();
            this.c_time_start = new System.Windows.Forms.NumericUpDown();
            this.c_time_end = new System.Windows.Forms.NumericUpDown();
            this.c_zoom_amount = new System.Windows.Forms.NumericUpDown();
            this.cb_show_unlocked_hours = new System.Windows.Forms.CheckBox();
            this.cb_show_work_hours = new System.Windows.Forms.CheckBox();
            this.cb_full_background_lines = new System.Windows.Forms.CheckBox();
            this.dtp_work_start = new System.Windows.Forms.DateTimePicker();
            this.dtp_work_end = new System.Windows.Forms.DateTimePicker();
            this.cb_show_detailed_weeks = new System.Windows.Forms.CheckBox();
            this.l_work_hours = new System.Windows.Forms.Label();
            this.l_unlocked_hours = new System.Windows.Forms.Label();
            this.l_work_start = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.l_time_start = new System.Windows.Forms.Label();
            this.l_time_end = new System.Windows.Forms.Label();
            this.l_zoom = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.b_process_eventlog = new System.Windows.Forms.Button();
            this.cb_log_program_start = new System.Windows.Forms.CheckBox();
            this.cb_log_program_shutdown = new System.Windows.Forms.CheckBox();
            this.flp_1 = new System.Windows.Forms.FlowLayoutPanel();
            this.l_unlocked_bar_color = new System.Windows.Forms.Label();
            this.b_unlocked_bar_color = new System.Windows.Forms.Button();
            this.flp_2 = new System.Windows.Forms.FlowLayoutPanel();
            this.l_work_bar_color = new System.Windows.Forms.Label();
            this.b_work_bar_color = new System.Windows.Forms.Button();
            this.flp_3 = new System.Windows.Forms.FlowLayoutPanel();
            this.l_latest_unlocked_bar_color = new System.Windows.Forms.Label();
            this.b_latest_unlocked_bar_color = new System.Windows.Forms.Button();
            this.flp_4 = new System.Windows.Forms.FlowLayoutPanel();
            this.l_last_work_bar_color = new System.Windows.Forms.Label();
            this.b_latest_work_bar_color = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.c_work_hours)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c_unlocked_hours)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c_time_start)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c_time_end)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c_zoom_amount)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.flp_1.SuspendLayout();
            this.flp_2.SuspendLayout();
            this.flp_3.SuspendLayout();
            this.flp_4.SuspendLayout();
            this.SuspendLayout();
            // 
            // b_ok
            // 
            this.b_ok.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.tableLayoutPanel1.SetColumnSpan(this.b_ok, 2);
            this.b_ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.b_ok.Location = new System.Drawing.Point(142, 352);
            this.b_ok.Name = "b_ok";
            this.b_ok.Size = new System.Drawing.Size(75, 23);
            this.b_ok.TabIndex = 0;
            this.b_ok.Text = "Ok";
            this.b_ok.UseVisualStyleBackColor = true;
            // 
            // b_cancel
            // 
            this.b_cancel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.b_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.b_cancel.Location = new System.Drawing.Point(223, 352);
            this.b_cancel.Name = "b_cancel";
            this.b_cancel.Size = new System.Drawing.Size(75, 23);
            this.b_cancel.TabIndex = 1;
            this.b_cancel.Text = "Cancel";
            this.b_cancel.UseVisualStyleBackColor = true;
            // 
            // c_work_hours
            // 
            this.c_work_hours.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.c_work_hours.DecimalPlaces = 1;
            this.c_work_hours.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.c_work_hours.Location = new System.Drawing.Point(170, 199);
            this.c_work_hours.Name = "c_work_hours";
            this.c_work_hours.Size = new System.Drawing.Size(47, 20);
            this.c_work_hours.TabIndex = 2;
            // 
            // c_unlocked_hours
            // 
            this.c_unlocked_hours.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.c_unlocked_hours.DecimalPlaces = 1;
            this.c_unlocked_hours.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.c_unlocked_hours.Location = new System.Drawing.Point(170, 237);
            this.c_unlocked_hours.Name = "c_unlocked_hours";
            this.c_unlocked_hours.Size = new System.Drawing.Size(47, 20);
            this.c_unlocked_hours.TabIndex = 3;
            // 
            // c_time_start
            // 
            this.c_time_start.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.c_time_start.Location = new System.Drawing.Point(170, 85);
            this.c_time_start.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.c_time_start.Name = "c_time_start";
            this.c_time_start.Size = new System.Drawing.Size(47, 20);
            this.c_time_start.TabIndex = 4;
            // 
            // c_time_end
            // 
            this.c_time_end.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.c_time_end.Location = new System.Drawing.Point(170, 123);
            this.c_time_end.Maximum = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.c_time_end.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.c_time_end.Name = "c_time_end";
            this.c_time_end.Size = new System.Drawing.Size(47, 20);
            this.c_time_end.TabIndex = 5;
            this.c_time_end.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // c_zoom_amount
            // 
            this.c_zoom_amount.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.c_zoom_amount.Location = new System.Drawing.Point(170, 161);
            this.c_zoom_amount.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.c_zoom_amount.Name = "c_zoom_amount";
            this.c_zoom_amount.Size = new System.Drawing.Size(47, 20);
            this.c_zoom_amount.TabIndex = 6;
            this.c_zoom_amount.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // cb_show_unlocked_hours
            // 
            this.cb_show_unlocked_hours.AutoSize = true;
            this.cb_show_unlocked_hours.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.tableLayoutPanel1.SetColumnSpan(this.cb_show_unlocked_hours, 2);
            this.cb_show_unlocked_hours.Dock = System.Windows.Forms.DockStyle.Right;
            this.cb_show_unlocked_hours.Location = new System.Drawing.Point(269, 41);
            this.cb_show_unlocked_hours.Name = "cb_show_unlocked_hours";
            this.cb_show_unlocked_hours.Size = new System.Drawing.Size(129, 32);
            this.cb_show_unlocked_hours.TabIndex = 8;
            this.cb_show_unlocked_hours.Text = "Show unlocked hours";
            this.cb_show_unlocked_hours.UseVisualStyleBackColor = true;
            // 
            // cb_show_work_hours
            // 
            this.cb_show_work_hours.AutoSize = true;
            this.cb_show_work_hours.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.tableLayoutPanel1.SetColumnSpan(this.cb_show_work_hours, 2);
            this.cb_show_work_hours.Dock = System.Windows.Forms.DockStyle.Right;
            this.cb_show_work_hours.Location = new System.Drawing.Point(290, 3);
            this.cb_show_work_hours.Name = "cb_show_work_hours";
            this.cb_show_work_hours.Size = new System.Drawing.Size(108, 32);
            this.cb_show_work_hours.TabIndex = 9;
            this.cb_show_work_hours.Text = "Show work hours";
            this.cb_show_work_hours.UseVisualStyleBackColor = true;
            // 
            // cb_full_background_lines
            // 
            this.cb_full_background_lines.AutoSize = true;
            this.cb_full_background_lines.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.tableLayoutPanel1.SetColumnSpan(this.cb_full_background_lines, 2);
            this.cb_full_background_lines.Dock = System.Windows.Forms.DockStyle.Right;
            this.cb_full_background_lines.Location = new System.Drawing.Point(272, 117);
            this.cb_full_background_lines.Name = "cb_full_background_lines";
            this.cb_full_background_lines.Size = new System.Drawing.Size(126, 32);
            this.cb_full_background_lines.TabIndex = 10;
            this.cb_full_background_lines.Text = "Full background lines";
            this.cb_full_background_lines.UseVisualStyleBackColor = true;
            // 
            // dtp_work_start
            // 
            this.dtp_work_start.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.dtp_work_start.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtp_work_start.Location = new System.Drawing.Point(133, 9);
            this.dtp_work_start.Name = "dtp_work_start";
            this.dtp_work_start.ShowUpDown = true;
            this.dtp_work_start.Size = new System.Drawing.Size(84, 20);
            this.dtp_work_start.TabIndex = 11;
            // 
            // dtp_work_end
            // 
            this.dtp_work_end.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.dtp_work_end.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtp_work_end.Location = new System.Drawing.Point(133, 47);
            this.dtp_work_end.Name = "dtp_work_end";
            this.dtp_work_end.ShowUpDown = true;
            this.dtp_work_end.Size = new System.Drawing.Size(84, 20);
            this.dtp_work_end.TabIndex = 12;
            // 
            // cb_show_detailed_weeks
            // 
            this.cb_show_detailed_weeks.AutoSize = true;
            this.cb_show_detailed_weeks.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.tableLayoutPanel1.SetColumnSpan(this.cb_show_detailed_weeks, 2);
            this.cb_show_detailed_weeks.Dock = System.Windows.Forms.DockStyle.Right;
            this.cb_show_detailed_weeks.Location = new System.Drawing.Point(271, 79);
            this.cb_show_detailed_weeks.Name = "cb_show_detailed_weeks";
            this.cb_show_detailed_weeks.Size = new System.Drawing.Size(127, 32);
            this.cb_show_detailed_weeks.TabIndex = 17;
            this.cb_show_detailed_weeks.Text = "Show detailed weeks";
            this.cb_show_detailed_weeks.UseVisualStyleBackColor = true;
            // 
            // l_work_hours
            // 
            this.l_work_hours.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.l_work_hours.AutoSize = true;
            this.l_work_hours.Location = new System.Drawing.Point(3, 202);
            this.l_work_hours.Name = "l_work_hours";
            this.l_work_hours.Size = new System.Drawing.Size(103, 13);
            this.l_work_hours.TabIndex = 18;
            this.l_work_hours.Text = "Work hours required";
            // 
            // l_unlocked_hours
            // 
            this.l_unlocked_hours.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.l_unlocked_hours.AutoSize = true;
            this.l_unlocked_hours.Location = new System.Drawing.Point(3, 240);
            this.l_unlocked_hours.Name = "l_unlocked_hours";
            this.l_unlocked_hours.Size = new System.Drawing.Size(123, 13);
            this.l_unlocked_hours.TabIndex = 19;
            this.l_unlocked_hours.Text = "Unlocked hours required";
            // 
            // l_work_start
            // 
            this.l_work_start.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.l_work_start.AutoSize = true;
            this.l_work_start.Location = new System.Drawing.Point(3, 12);
            this.l_work_start.Name = "l_work_start";
            this.l_work_start.Size = new System.Drawing.Size(61, 13);
            this.l_work_start.TabIndex = 20;
            this.l_work_start.Text = "Work starts";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "Work ends";
            // 
            // l_time_start
            // 
            this.l_time_start.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.l_time_start.AutoSize = true;
            this.l_time_start.Location = new System.Drawing.Point(3, 88);
            this.l_time_start.Name = "l_time_start";
            this.l_time_start.Size = new System.Drawing.Size(64, 13);
            this.l_time_start.TabIndex = 22;
            this.l_time_start.Text = "Graph starts";
            // 
            // l_time_end
            // 
            this.l_time_end.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.l_time_end.AutoSize = true;
            this.l_time_end.Location = new System.Drawing.Point(3, 126);
            this.l_time_end.Name = "l_time_end";
            this.l_time_end.Size = new System.Drawing.Size(62, 13);
            this.l_time_end.TabIndex = 23;
            this.l_time_end.Text = "Graph ends";
            // 
            // l_zoom
            // 
            this.l_zoom.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.l_zoom.AutoSize = true;
            this.l_zoom.Location = new System.Drawing.Point(3, 164);
            this.l_zoom.Name = "l_zoom";
            this.l_zoom.Size = new System.Drawing.Size(34, 13);
            this.l_zoom.TabIndex = 24;
            this.l_zoom.Text = "Zoom";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 51F));
            this.tableLayoutPanel1.Controls.Add(this.c_unlocked_hours, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.c_zoom_amount, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.c_work_hours, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.dtp_work_end, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.c_time_end, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.l_work_start, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.c_time_start, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.dtp_work_start, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.l_time_start, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.l_time_end, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.l_zoom, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.l_work_hours, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.l_unlocked_hours, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.b_cancel, 2, 9);
            this.tableLayoutPanel1.Controls.Add(this.cb_show_detailed_weeks, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.cb_show_work_hours, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.cb_show_unlocked_hours, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.cb_full_background_lines, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.b_ok, 0, 9);
            this.tableLayoutPanel1.Controls.Add(this.b_process_eventlog, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.cb_log_program_start, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.cb_log_program_shutdown, 2, 8);
            this.tableLayoutPanel1.Controls.Add(this.flp_1, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.flp_2, 2, 5);
            this.tableLayoutPanel1.Controls.Add(this.flp_3, 2, 6);
            this.tableLayoutPanel1.Controls.Add(this.flp_4, 2, 7);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 10;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(401, 385);
            this.tableLayoutPanel1.TabIndex = 29;
            // 
            // b_process_eventlog
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.b_process_eventlog, 2);
            this.b_process_eventlog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.b_process_eventlog.Location = new System.Drawing.Point(3, 269);
            this.b_process_eventlog.Name = "b_process_eventlog";
            this.b_process_eventlog.Size = new System.Drawing.Size(214, 32);
            this.b_process_eventlog.TabIndex = 29;
            this.b_process_eventlog.Text = "Process eventlog";
            this.b_process_eventlog.UseVisualStyleBackColor = true;
            this.b_process_eventlog.Click += new System.EventHandler(this.b_process_eventlog_Click);
            // 
            // cb_log_program_start
            // 
            this.cb_log_program_start.AutoSize = true;
            this.cb_log_program_start.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.tableLayoutPanel1.SetColumnSpan(this.cb_log_program_start, 2);
            this.cb_log_program_start.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cb_log_program_start.Location = new System.Drawing.Point(3, 307);
            this.cb_log_program_start.Name = "cb_log_program_start";
            this.cb_log_program_start.Size = new System.Drawing.Size(214, 32);
            this.cb_log_program_start.TabIndex = 30;
            this.cb_log_program_start.Text = "Count program start as logon";
            this.cb_log_program_start.UseVisualStyleBackColor = true;
            // 
            // cb_log_program_shutdown
            // 
            this.cb_log_program_shutdown.AutoSize = true;
            this.cb_log_program_shutdown.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.tableLayoutPanel1.SetColumnSpan(this.cb_log_program_shutdown, 2);
            this.cb_log_program_shutdown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cb_log_program_shutdown.Location = new System.Drawing.Point(223, 307);
            this.cb_log_program_shutdown.Name = "cb_log_program_shutdown";
            this.cb_log_program_shutdown.Size = new System.Drawing.Size(175, 32);
            this.cb_log_program_shutdown.TabIndex = 31;
            this.cb_log_program_shutdown.Text = "Count program exit as logoff";
            this.cb_log_program_shutdown.UseVisualStyleBackColor = true;
            // 
            // flp_1
            // 
            this.flp_1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.flp_1.AutoSize = true;
            this.flp_1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.SetColumnSpan(this.flp_1, 2);
            this.flp_1.Controls.Add(this.l_unlocked_bar_color);
            this.flp_1.Controls.Add(this.b_unlocked_bar_color);
            this.flp_1.Location = new System.Drawing.Point(281, 163);
            this.flp_1.Name = "flp_1";
            this.flp_1.Size = new System.Drawing.Size(117, 16);
            this.flp_1.TabIndex = 32;
            // 
            // l_unlocked_bar_color
            // 
            this.l_unlocked_bar_color.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.l_unlocked_bar_color.AutoSize = true;
            this.l_unlocked_bar_color.Location = new System.Drawing.Point(0, 1);
            this.l_unlocked_bar_color.Margin = new System.Windows.Forms.Padding(0);
            this.l_unlocked_bar_color.Name = "l_unlocked_bar_color";
            this.l_unlocked_bar_color.Size = new System.Drawing.Size(101, 13);
            this.l_unlocked_bar_color.TabIndex = 27;
            this.l_unlocked_bar_color.Text = "Unlocked time color";
            // 
            // b_unlocked_bar_color
            // 
            this.b_unlocked_bar_color.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.b_unlocked_bar_color.FlatAppearance.BorderSize = 0;
            this.b_unlocked_bar_color.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.b_unlocked_bar_color.Location = new System.Drawing.Point(101, 0);
            this.b_unlocked_bar_color.Margin = new System.Windows.Forms.Padding(0);
            this.b_unlocked_bar_color.Name = "b_unlocked_bar_color";
            this.b_unlocked_bar_color.Size = new System.Drawing.Size(16, 16);
            this.b_unlocked_bar_color.TabIndex = 26;
            this.b_unlocked_bar_color.UseVisualStyleBackColor = true;
            // 
            // flp_2
            // 
            this.flp_2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.flp_2.AutoSize = true;
            this.flp_2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.SetColumnSpan(this.flp_2, 2);
            this.flp_2.Controls.Add(this.l_work_bar_color);
            this.flp_2.Controls.Add(this.b_work_bar_color);
            this.flp_2.Location = new System.Drawing.Point(301, 201);
            this.flp_2.Name = "flp_2";
            this.flp_2.Size = new System.Drawing.Size(97, 16);
            this.flp_2.TabIndex = 33;
            // 
            // l_work_bar_color
            // 
            this.l_work_bar_color.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.l_work_bar_color.AutoSize = true;
            this.l_work_bar_color.Location = new System.Drawing.Point(0, 1);
            this.l_work_bar_color.Margin = new System.Windows.Forms.Padding(0);
            this.l_work_bar_color.Name = "l_work_bar_color";
            this.l_work_bar_color.Size = new System.Drawing.Size(81, 13);
            this.l_work_bar_color.TabIndex = 28;
            this.l_work_bar_color.Text = "Work time color";
            // 
            // b_work_bar_color
            // 
            this.b_work_bar_color.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.b_work_bar_color.FlatAppearance.BorderSize = 0;
            this.b_work_bar_color.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.b_work_bar_color.Location = new System.Drawing.Point(81, 0);
            this.b_work_bar_color.Margin = new System.Windows.Forms.Padding(0);
            this.b_work_bar_color.Name = "b_work_bar_color";
            this.b_work_bar_color.Size = new System.Drawing.Size(16, 16);
            this.b_work_bar_color.TabIndex = 27;
            this.b_work_bar_color.UseVisualStyleBackColor = true;
            // 
            // flp_3
            // 
            this.flp_3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.flp_3.AutoSize = true;
            this.flp_3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.SetColumnSpan(this.flp_3, 2);
            this.flp_3.Controls.Add(this.l_latest_unlocked_bar_color);
            this.flp_3.Controls.Add(this.b_latest_unlocked_bar_color);
            this.flp_3.Location = new System.Drawing.Point(254, 239);
            this.flp_3.Name = "flp_3";
            this.flp_3.Size = new System.Drawing.Size(144, 16);
            this.flp_3.TabIndex = 34;
            // 
            // l_latest_unlocked_bar_color
            // 
            this.l_latest_unlocked_bar_color.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.l_latest_unlocked_bar_color.AutoSize = true;
            this.l_latest_unlocked_bar_color.Location = new System.Drawing.Point(3, 1);
            this.l_latest_unlocked_bar_color.Name = "l_latest_unlocked_bar_color";
            this.l_latest_unlocked_bar_color.Size = new System.Drawing.Size(122, 13);
            this.l_latest_unlocked_bar_color.TabIndex = 29;
            this.l_latest_unlocked_bar_color.Text = "Last unlocked time color";
            // 
            // b_latest_unlocked_bar_color
            // 
            this.b_latest_unlocked_bar_color.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.b_latest_unlocked_bar_color.FlatAppearance.BorderSize = 0;
            this.b_latest_unlocked_bar_color.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.b_latest_unlocked_bar_color.Location = new System.Drawing.Point(128, 0);
            this.b_latest_unlocked_bar_color.Margin = new System.Windows.Forms.Padding(0);
            this.b_latest_unlocked_bar_color.Name = "b_latest_unlocked_bar_color";
            this.b_latest_unlocked_bar_color.Size = new System.Drawing.Size(16, 16);
            this.b_latest_unlocked_bar_color.TabIndex = 28;
            this.b_latest_unlocked_bar_color.UseVisualStyleBackColor = true;
            // 
            // flp_4
            // 
            this.flp_4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.flp_4.AutoSize = true;
            this.flp_4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.SetColumnSpan(this.flp_4, 2);
            this.flp_4.Controls.Add(this.l_last_work_bar_color);
            this.flp_4.Controls.Add(this.b_latest_work_bar_color);
            this.flp_4.Location = new System.Drawing.Point(275, 277);
            this.flp_4.Name = "flp_4";
            this.flp_4.Size = new System.Drawing.Size(123, 16);
            this.flp_4.TabIndex = 35;
            // 
            // l_last_work_bar_color
            // 
            this.l_last_work_bar_color.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.l_last_work_bar_color.AutoSize = true;
            this.l_last_work_bar_color.Location = new System.Drawing.Point(3, 1);
            this.l_last_work_bar_color.Name = "l_last_work_bar_color";
            this.l_last_work_bar_color.Size = new System.Drawing.Size(101, 13);
            this.l_last_work_bar_color.TabIndex = 30;
            this.l_last_work_bar_color.Text = "Last work time color";
            // 
            // b_latest_work_bar_color
            // 
            this.b_latest_work_bar_color.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.b_latest_work_bar_color.FlatAppearance.BorderSize = 0;
            this.b_latest_work_bar_color.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.b_latest_work_bar_color.Location = new System.Drawing.Point(107, 0);
            this.b_latest_work_bar_color.Margin = new System.Windows.Forms.Padding(0);
            this.b_latest_work_bar_color.Name = "b_latest_work_bar_color";
            this.b_latest_work_bar_color.Size = new System.Drawing.Size(16, 16);
            this.b_latest_work_bar_color.TabIndex = 29;
            this.b_latest_work_bar_color.UseVisualStyleBackColor = true;
            // 
            // ConfigurationForm
            // 
            this.AcceptButton = this.b_ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.b_cancel;
            this.ClientSize = new System.Drawing.Size(401, 385);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ConfigurationForm";
            this.Text = "Configuration";
            ((System.ComponentModel.ISupportInitialize)(this.c_work_hours)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c_unlocked_hours)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c_time_start)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c_time_end)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c_zoom_amount)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flp_1.ResumeLayout(false);
            this.flp_1.PerformLayout();
            this.flp_2.ResumeLayout(false);
            this.flp_2.PerformLayout();
            this.flp_3.ResumeLayout(false);
            this.flp_3.PerformLayout();
            this.flp_4.ResumeLayout(false);
            this.flp_4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button b_ok;
        private System.Windows.Forms.Button b_cancel;
        private System.Windows.Forms.NumericUpDown c_work_hours;
        private System.Windows.Forms.NumericUpDown c_unlocked_hours;
        private System.Windows.Forms.NumericUpDown c_time_start;
        private System.Windows.Forms.NumericUpDown c_time_end;
        private System.Windows.Forms.NumericUpDown c_zoom_amount;
        private System.Windows.Forms.CheckBox cb_show_unlocked_hours;
        private System.Windows.Forms.CheckBox cb_show_work_hours;
        private System.Windows.Forms.CheckBox cb_full_background_lines;
        private System.Windows.Forms.DateTimePicker dtp_work_start;
        private System.Windows.Forms.DateTimePicker dtp_work_end;
        private System.Windows.Forms.CheckBox cb_show_detailed_weeks;
        private System.Windows.Forms.Label l_work_hours;
        private System.Windows.Forms.Label l_unlocked_hours;
        private System.Windows.Forms.Label l_work_start;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label l_time_start;
        private System.Windows.Forms.Label l_time_end;
        private System.Windows.Forms.Label l_zoom;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button b_process_eventlog;
        private System.Windows.Forms.CheckBox cb_log_program_start;
        private System.Windows.Forms.CheckBox cb_log_program_shutdown;
        private System.Windows.Forms.FlowLayoutPanel flp_1;
        private System.Windows.Forms.Label l_unlocked_bar_color;
        private System.Windows.Forms.Button b_unlocked_bar_color;
        private System.Windows.Forms.FlowLayoutPanel flp_2;
        private System.Windows.Forms.Label l_work_bar_color;
        private System.Windows.Forms.Button b_work_bar_color;
        private System.Windows.Forms.FlowLayoutPanel flp_3;
        private System.Windows.Forms.Label l_latest_unlocked_bar_color;
        private System.Windows.Forms.Button b_latest_unlocked_bar_color;
        private System.Windows.Forms.FlowLayoutPanel flp_4;
        private System.Windows.Forms.Label l_last_work_bar_color;
        private System.Windows.Forms.Button b_latest_work_bar_color;
    }
}