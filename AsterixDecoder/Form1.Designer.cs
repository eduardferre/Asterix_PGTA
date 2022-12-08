﻿using GMap.NET.WindowsForms;

namespace AsterixDecoder
{
    partial class AsterixDecoder
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.clickInfo_label = new System.Windows.Forms.Label();
            this.gridCAT10 = new System.Windows.Forms.DataGridView();
            this.gridCAT21 = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileStripMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.loadFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CAT10ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportCSV_CAT10_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CAT21ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportCSV_CAT21_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mAPSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportKMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.trajectoriesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.info_label = new System.Windows.Forms.Label();
            this.process_label = new System.Windows.Forms.Label();
            this.msg_label = new System.Windows.Forms.Label();
            this.gMapControl1 = new GMap.NET.WindowsForms.GMapControl();
            this.startButton = new System.Windows.Forms.Button();
            this.resetButton = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timeLabel = new System.Windows.Forms.Label();
            this.speedButton = new System.Windows.Forms.Button();
            this.smrCheck = new System.Windows.Forms.CheckBox();
            this.mlatCheck = new System.Windows.Forms.CheckBox();
            this.adsbCheck = new System.Windows.Forms.CheckBox();
            this.timeButton = new System.Windows.Forms.Button();
            this.labelFilter = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.trajButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridCAT10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridCAT21)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // clickInfo_label
            // 
            this.clickInfo_label.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.clickInfo_label.Font = new System.Drawing.Font("Segoe UI", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.clickInfo_label.Location = new System.Drawing.Point(1152, 62);
            this.clickInfo_label.Name = "clickInfo_label";
            this.clickInfo_label.Size = new System.Drawing.Size(122, 172);
            this.clickInfo_label.TabIndex = 3;
            // 
            // gridCAT10
            // 
            this.gridCAT10.AllowUserToAddRows = false;
            this.gridCAT10.AllowUserToDeleteRows = false;
            this.gridCAT10.AllowUserToResizeColumns = false;
            this.gridCAT10.AllowUserToResizeRows = false;
            this.gridCAT10.CausesValidation = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridCAT10.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridCAT10.ColumnHeadersHeight = 22;
            this.gridCAT10.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridCAT10.DefaultCellStyle = dataGridViewCellStyle2;
            this.gridCAT10.Location = new System.Drawing.Point(12, 317);
            this.gridCAT10.Margin = new System.Windows.Forms.Padding(0);
            this.gridCAT10.MultiSelect = false;
            this.gridCAT10.Name = "gridCAT10";
            this.gridCAT10.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridCAT10.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.gridCAT10.RowHeadersWidth = 6;
            this.gridCAT10.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.gridCAT10.Size = new System.Drawing.Size(10, 11);
            this.gridCAT10.TabIndex = 4;
            this.gridCAT10.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridCAT10_CellClick);
            // 
            // gridCAT21
            // 
            this.gridCAT21.AllowUserToAddRows = false;
            this.gridCAT21.AllowUserToDeleteRows = false;
            this.gridCAT21.AllowUserToResizeColumns = false;
            this.gridCAT21.AllowUserToResizeRows = false;
            this.gridCAT21.CausesValidation = false;
            this.gridCAT21.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridCAT21.ColumnHeadersHeight = 22;
            this.gridCAT21.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.gridCAT21.DefaultCellStyle = dataGridViewCellStyle2;
            this.gridCAT21.Location = new System.Drawing.Point(12, 358);
            this.gridCAT21.Margin = new System.Windows.Forms.Padding(0);
            this.gridCAT21.MultiSelect = false;
            this.gridCAT21.Name = "gridCAT21";
            this.gridCAT21.ReadOnly = true;
            this.gridCAT21.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.gridCAT21.RowHeadersWidth = 6;
            this.gridCAT21.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.gridCAT21.Size = new System.Drawing.Size(10, 10);
            this.gridCAT21.TabIndex = 4;
            this.gridCAT21.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridCAT21_CellClick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileStripMenu,
            this.CAT10ToolStripMenuItem,
            this.CAT21ToolStripMenuItem,
            this.mAPSToolStripMenuItem,
            this.trajectoriesToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1300, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileStripMenu
            // 
            this.fileStripMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadFileToolStripMenuItem});
            this.fileStripMenu.Name = "fileStripMenu";
            this.fileStripMenu.Size = new System.Drawing.Size(37, 20);
            this.fileStripMenu.Text = "File";
            // 
            // loadFileToolStripMenuItem
            // 
            this.loadFileToolStripMenuItem.Name = "loadFileToolStripMenuItem";
            this.loadFileToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.loadFileToolStripMenuItem.Text = "Load File";
            this.loadFileToolStripMenuItem.Click += new System.EventHandler(this.loadFileToolStripMenuItem_Click);
            // 
            // CAT10ToolStripMenuItem
            // 
            this.CAT10ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportCSV_CAT10_ToolStripMenuItem});
            this.CAT10ToolStripMenuItem.Name = "CAT10ToolStripMenuItem";
            this.CAT10ToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.CAT10ToolStripMenuItem.Text = "CAT10";
            this.CAT10ToolStripMenuItem.Click += new System.EventHandler(this.tableCAT10ToolStripMenuItem_Click);
            // 
            // exportCSV_CAT10_ToolStripMenuItem
            // 
            this.exportCSV_CAT10_ToolStripMenuItem.Name = "exportCSV_CAT10_ToolStripMenuItem";
            this.exportCSV_CAT10_ToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.exportCSV_CAT10_ToolStripMenuItem.Text = "Export CSV";
            this.exportCSV_CAT10_ToolStripMenuItem.Click += new System.EventHandler(this.exportCSV_CAT10_ToolStripMenuItem_Click);
            // 
            // CAT21ToolStripMenuItem
            // 
            this.CAT21ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportCSV_CAT21_ToolStripMenuItem});
            this.CAT21ToolStripMenuItem.Name = "CAT21ToolStripMenuItem";
            this.CAT21ToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.CAT21ToolStripMenuItem.Text = "CAT21";
            this.CAT21ToolStripMenuItem.Click += new System.EventHandler(this.tableCAT21ToolStripMenuItem_Click);
            // 
            // exportCSV_CAT21_ToolStripMenuItem
            // 
            this.exportCSV_CAT21_ToolStripMenuItem.Name = "exportCSV_CAT21_ToolStripMenuItem";
            this.exportCSV_CAT21_ToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.exportCSV_CAT21_ToolStripMenuItem.Text = "Export CSV";
            this.exportCSV_CAT21_ToolStripMenuItem.Click += new System.EventHandler(this.exportCSV_CAT21_ToolStripMenuItem_Click);
            // 
            // mAPSToolStripMenuItem
            // 
            this.mAPSToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportKMLToolStripMenuItem});
            this.mAPSToolStripMenuItem.Name = "mAPSToolStripMenuItem";
            this.mAPSToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.mAPSToolStripMenuItem.Text = "Maps";
            this.mAPSToolStripMenuItem.Click += new System.EventHandler(this.mapsToolStripMenuItem_Click);
            // 
            // exportKMLToolStripMenuItem
            // 
            this.exportKMLToolStripMenuItem.Name = "exportKMLToolStripMenuItem";
            this.exportKMLToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.exportKMLToolStripMenuItem.Text = "Export KML";
            this.exportKMLToolStripMenuItem.Click += new System.EventHandler(this.exportKMLToolStripMenuItem_Click);
            // 
            // trajectoriesToolStripMenuItem
            // 
            this.trajectoriesToolStripMenuItem.Name = "trajectoriesToolStripMenuItem";
            this.trajectoriesToolStripMenuItem.Size = new System.Drawing.Size(78, 20);
            this.trajectoriesToolStripMenuItem.Text = "Trajectories";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // info_label
            // 
            this.info_label.AutoSize = true;
            this.info_label.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.info_label.Location = new System.Drawing.Point(1127, 37);
            this.info_label.Name = "info_label";
            this.info_label.Size = new System.Drawing.Size(161, 25);
            this.info_label.TabIndex = 6;
            this.info_label.Text = "DATA ITEM INFO";
            this.info_label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // process_label
            // 
            this.process_label.AutoSize = true;
            this.process_label.Location = new System.Drawing.Point(494, 82);
            this.process_label.Name = "process_label";
            this.process_label.Size = new System.Drawing.Size(0, 15);
            this.process_label.TabIndex = 7;
            // 
            // msg_label
            // 
            this.msg_label.AutoSize = true;
            this.msg_label.Location = new System.Drawing.Point(494, 131);
            this.msg_label.Name = "msg_label";
            this.msg_label.Size = new System.Drawing.Size(0, 15);
            this.msg_label.TabIndex = 8;
            // 
            // gMapControl1
            // 
            this.gMapControl1.Bearing = 0F;
            this.gMapControl1.CanDragMap = true;
            this.gMapControl1.EmptyTileColor = System.Drawing.Color.Navy;
            this.gMapControl1.GrayScaleMode = false;
            this.gMapControl1.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gMapControl1.LevelsKeepInMemory = 5;
            this.gMapControl1.Location = new System.Drawing.Point(12, 37);
            this.gMapControl1.MarkersEnabled = true;
            this.gMapControl1.MaxZoom = 2;
            this.gMapControl1.MinZoom = 2;
            this.gMapControl1.MouseWheelZoomEnabled = true;
            this.gMapControl1.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.gMapControl1.Name = "gMapControl1";
            this.gMapControl1.NegativeMode = false;
            this.gMapControl1.PolygonsEnabled = true;
            this.gMapControl1.RetryLoadTile = 0;
            this.gMapControl1.RoutesEnabled = true;
            this.gMapControl1.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.gMapControl1.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.gMapControl1.ShowTileGridLines = false;
            this.gMapControl1.Size = new System.Drawing.Size(1091, 601);
            this.gMapControl1.TabIndex = 9;
            this.gMapControl1.Zoom = 0D;
            this.gMapControl1.OnMarkerClick += new GMap.NET.WindowsForms.MarkerClick(this.gMapControl1_OnMarkerClick);
            this.gMapControl1.Load += new System.EventHandler(this.gMapControl1_Load);
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(1127, 65);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(149, 32);
            this.startButton.TabIndex = 10;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // resetButton
            // 
            this.resetButton.Location = new System.Drawing.Point(1127, 103);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(149, 32);
            this.resetButton.TabIndex = 11;
            this.resetButton.Text = "Reset";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timeLabel
            // 
            this.timeLabel.AutoSize = true;
            this.timeLabel.Location = new System.Drawing.Point(1182, 45);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(49, 15);
            this.timeLabel.TabIndex = 12;
            this.timeLabel.Text = "00:00:00";
            // 
            // speedButton
            // 
            this.speedButton.Location = new System.Drawing.Point(1127, 141);
            this.speedButton.Name = "speedButton";
            this.speedButton.Size = new System.Drawing.Size(149, 32);
            this.speedButton.TabIndex = 13;
            this.speedButton.Text = "Speed (x1)";
            this.speedButton.UseVisualStyleBackColor = true;
            this.speedButton.Click += new System.EventHandler(this.speedButton_Click);
            // 
            // smrCheck
            // 
            this.smrCheck.AutoSize = true;
            this.smrCheck.Location = new System.Drawing.Point(1127, 179);
            this.smrCheck.Name = "smrCheck";
            this.smrCheck.Size = new System.Drawing.Size(85, 19);
            this.smrCheck.TabIndex = 14;
            this.smrCheck.Text = "SMR Traffic";
            this.smrCheck.UseVisualStyleBackColor = true;
            // 
            // mlatCheck
            // 
            this.mlatCheck.AutoSize = true;
            this.mlatCheck.Location = new System.Drawing.Point(1126, 204);
            this.mlatCheck.Name = "mlatCheck";
            this.mlatCheck.Size = new System.Drawing.Size(91, 19);
            this.mlatCheck.TabIndex = 15;
            this.mlatCheck.Text = "MLAT Traffic";
            this.mlatCheck.UseVisualStyleBackColor = true;
            // 
            // adsbCheck
            // 
            this.adsbCheck.AutoSize = true;
            this.adsbCheck.Location = new System.Drawing.Point(1126, 229);
            this.adsbCheck.Name = "adsbCheck";
            this.adsbCheck.Size = new System.Drawing.Size(90, 19);
            this.adsbCheck.TabIndex = 16;
            this.adsbCheck.Text = "ADSB Traffic";
            this.adsbCheck.UseVisualStyleBackColor = true;
            // 
            // timeButton
            // 
            this.timeButton.Location = new System.Drawing.Point(1126, 254);
            this.timeButton.Name = "timeButton";
            this.timeButton.Size = new System.Drawing.Size(149, 32);
            this.timeButton.TabIndex = 17;
            this.timeButton.Text = "Select Time";
            this.timeButton.UseVisualStyleBackColor = true;
            this.timeButton.Click += new System.EventHandler(this.timeButton_Click);
            // 
            // labelFilter
            // 
            this.labelFilter.AutoSize = true;
            this.labelFilter.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelFilter.Location = new System.Drawing.Point(1152, 255);
            this.labelFilter.Name = "labelFilter";
            this.labelFilter.Size = new System.Drawing.Size(124, 25);
            this.labelFilter.TabIndex = 6;
            this.labelFilter.Text = "FILTER DATA";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1238, 501);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 15);
            this.label1.TabIndex = 18;
            this.label1.Text = "label1";
            // 
            // trajButton
            // 
            this.trajButton.Location = new System.Drawing.Point(1126, 292);
            this.trajButton.Name = "trajButton";
            this.trajButton.Size = new System.Drawing.Size(149, 32);
            this.trajButton.TabIndex = 19;
            this.trajButton.Text = "Show Trajectory";
            this.trajButton.UseVisualStyleBackColor = true;
            this.trajButton.Click += new System.EventHandler(this.trajButton_Click);
            // 
            // AsterixDecoder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1300, 650);
            this.Controls.Add(this.labelFilter);
            this.Controls.Add(this.trajButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.timeButton);
            this.Controls.Add(this.adsbCheck);
            this.Controls.Add(this.mlatCheck);
            this.Controls.Add(this.smrCheck);
            this.Controls.Add(this.speedButton);
            this.Controls.Add(this.timeLabel);
            this.Controls.Add(this.resetButton);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.gMapControl1);
            this.Controls.Add(this.msg_label);
            this.Controls.Add(this.process_label);
            this.Controls.Add(this.info_label);
            this.Controls.Add(this.gridCAT21);
            this.Controls.Add(this.gridCAT10);
            this.Controls.Add(this.clickInfo_label);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.Name = "AsterixDecoder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ASTERIX Decoder";
            this.Load += new System.EventHandler(this.AsterixDecoder_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridCAT10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridCAT21)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label clickInfo_label;
        private System.Windows.Forms.DataGridView gridCAT10;
        private System.Windows.Forms.DataGridView gridCAT21;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileStripMenu;
        private System.Windows.Forms.ToolStripMenuItem loadFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CAT10ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CAT21ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mAPSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem trajectoriesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.Label info_label;
        private System.Windows.Forms.Label process_label;
        private System.Windows.Forms.Label msg_label;
        private GMap.NET.WindowsForms.GMapControl gMapControl1;
        private System.Windows.Forms.ToolStripMenuItem exportCSV_CAT10_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportCSV_CAT21_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportKMLToolStripMenuItem;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label timeLabel;
        private System.Windows.Forms.Button speedButton;
        private System.Windows.Forms.CheckBox smrCheck;
        private System.Windows.Forms.CheckBox mlatCheck;
        private System.Windows.Forms.CheckBox adsbCheck;
        private System.Windows.Forms.Button timeButton;
        private System.Windows.Forms.Label labelFilter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button trajButton;
    }
}

