namespace AsterixDecoder
{
    partial class Form2
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
            this.hhBox = new System.Windows.Forms.TextBox();
            this.mmBox = new System.Windows.Forms.TextBox();
            this.ssBox = new System.Windows.Forms.TextBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // hhBox
            // 
            this.hhBox.Location = new System.Drawing.Point(63, 36);
            this.hhBox.Name = "hhBox";
            this.hhBox.PlaceholderText = "hh";
            this.hhBox.Size = new System.Drawing.Size(32, 23);
            this.hhBox.TabIndex = 0;
            // 
            // mmBox
            // 
            this.mmBox.Location = new System.Drawing.Point(101, 36);
            this.mmBox.Name = "mmBox";
            this.mmBox.PlaceholderText = "mm";
            this.mmBox.Size = new System.Drawing.Size(32, 23);
            this.mmBox.TabIndex = 1;
            // 
            // ssBox
            // 
            this.ssBox.Location = new System.Drawing.Point(139, 36);
            this.ssBox.Name = "ssBox";
            this.ssBox.PlaceholderText = "ss";
            this.ssBox.Size = new System.Drawing.Size(32, 23);
            this.ssBox.TabIndex = 2;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(77, 82);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 3;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(229, 126);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.ssBox);
            this.Controls.Add(this.mmBox);
            this.Controls.Add(this.hhBox);
            this.Name = "Form2";
            this.Text = "Select Time";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox hhBox;
        private System.Windows.Forms.TextBox ssBox;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.TextBox mmBox;
    }
}