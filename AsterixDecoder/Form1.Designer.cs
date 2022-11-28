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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.clickInfo_label = new System.Windows.Forms.Label();
            this.gridCAT10 = new System.Windows.Forms.DataGridView();
            this.gridCAT21 = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileStripMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.loadFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveCSVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CAT10ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CAT21ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mAPSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.trajectoriesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.info_label = new System.Windows.Forms.Label();
            this.process_label = new System.Windows.Forms.Label();
            this.msg_label = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gridCAT10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridCAT21)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // clickInfo_label
            // 
            this.clickInfo_label.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.clickInfo_label.Font = new System.Drawing.Font("Segoe UI", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.clickInfo_label.Location = new System.Drawing.Point(1166, 62);
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
            this.gridCAT10.RowTemplate.Height = 25;
            this.gridCAT10.RowTemplate.ReadOnly = true;
            this.gridCAT10.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
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
            this.gridCAT21.RowTemplate.Height = 25;
            this.gridCAT21.RowTemplate.ReadOnly = true;
            this.gridCAT21.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
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
            this.loadFileToolStripMenuItem,
            this.saveCSVToolStripMenuItem});
            this.fileStripMenu.Name = "fileStripMenu";
            this.fileStripMenu.Size = new System.Drawing.Size(37, 20);
            this.fileStripMenu.Text = "File";
            // 
            // loadFileToolStripMenuItem
            // 
            this.loadFileToolStripMenuItem.Name = "loadFileToolStripMenuItem";
            this.loadFileToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.loadFileToolStripMenuItem.Text = "Load File";
            this.loadFileToolStripMenuItem.Click += new System.EventHandler(this.loadFileToolStripMenuItem_Click);
            // 
            // saveCSVToolStripMenuItem
            // 
            this.saveCSVToolStripMenuItem.Name = "saveCSVToolStripMenuItem";
            this.saveCSVToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.saveCSVToolStripMenuItem.Text = "Save CSV";
            // 
            // CAT10ToolStripMenuItem
            // 
            this.CAT10ToolStripMenuItem.Name = "CAT10ToolStripMenuItem";
            this.CAT10ToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.CAT10ToolStripMenuItem.Text = "CAT10";
            this.CAT10ToolStripMenuItem.Click += new System.EventHandler(this.CAT10ToolStripMenuItem_Click);
            // 
            // CAT21ToolStripMenuItem
            // 
            this.CAT21ToolStripMenuItem.Name = "CAT21ToolStripMenuItem";
            this.CAT21ToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.CAT21ToolStripMenuItem.Text = "CAT21";
            this.CAT21ToolStripMenuItem.Click += new System.EventHandler(this.CAT21ToolStripMenuItem_Click);
            // 
            // mAPSToolStripMenuItem
            // 
            this.mAPSToolStripMenuItem.Name = "mAPSToolStripMenuItem";
            this.mAPSToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.mAPSToolStripMenuItem.Text = "Maps";
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
            // AsterixDecoder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1300, 650);
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
            this.Load += new System.EventHandler(this.Form1_Load);
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
        private System.Windows.Forms.ToolStripMenuItem saveCSVToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CAT10ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CAT21ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mAPSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem trajectoriesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.Label info_label;
        private System.Windows.Forms.Label process_label;
        private System.Windows.Forms.Label msg_label;
    }
}

