namespace AsterixDecoder
{
    partial class Form1
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
            this.loadFiles_Button = new System.Windows.Forms.Button();
            this.doubleClickInfo_label = new System.Windows.Forms.Label();
            this.gridCAT10 = new System.Windows.Forms.DataGridView();
            this.gridCAT21 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridCAT10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridCAT21)).BeginInit();
            this.SuspendLayout();
            // 
            // loadFiles_Button
            // 
            this.loadFiles_Button.Location = new System.Drawing.Point(847, 12);
            this.loadFiles_Button.Name = "loadFiles_Button";
            this.loadFiles_Button.Size = new System.Drawing.Size(166, 43);
            this.loadFiles_Button.TabIndex = 2;
            this.loadFiles_Button.Text = "Load Files";
            this.loadFiles_Button.UseVisualStyleBackColor = true;
            this.loadFiles_Button.Click += new System.EventHandler(this.loadFiles_Button_Click);
            // 
            // doubleClickInfo_label
            // 
            this.doubleClickInfo_label.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.doubleClickInfo_label.Font = new System.Drawing.Font("Segoe UI", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.doubleClickInfo_label.Location = new System.Drawing.Point(847, 73);
            this.doubleClickInfo_label.Name = "doubleClickInfo_label";
            this.doubleClickInfo_label.Size = new System.Drawing.Size(166, 203);
            this.doubleClickInfo_label.TabIndex = 3;
            this.doubleClickInfo_label.Click += new System.EventHandler(this.doubleClickInfo_label_Click);
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
            this.gridCAT10.Location = new System.Drawing.Point(12, 12);
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
            this.gridCAT10.Size = new System.Drawing.Size(240, 150);
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
            this.gridCAT21.Location = new System.Drawing.Point(12, 172);
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
            this.gridCAT21.Size = new System.Drawing.Size(240, 150);
            this.gridCAT21.TabIndex = 4;
            this.gridCAT21.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridCAT21_CellClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1025, 641);
            this.Controls.Add(this.gridCAT21);
            this.Controls.Add(this.gridCAT10);
            this.Controls.Add(this.doubleClickInfo_label);
            this.Controls.Add(this.loadFiles_Button);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridCAT10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridCAT21)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button loadFiles_Button;
        private System.Windows.Forms.Label doubleClickInfo_label;
        private System.Windows.Forms.DataGridView gridCAT10;
        private System.Windows.Forms.DataGridView gridCAT21;
    }
}

