using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AsterixDecoder
{
    public partial class Form2 : Form
    {
        int time;
        public Form2()
        {
            InitializeComponent();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            time = Convert.ToInt32(hhBox.Text) * 60 * 60 + Convert.ToInt32(mmBox.Text) * 60 + Convert.ToInt32(ssBox.Text);
            this.Close();
        }
        public int getTime()
        {
            return this.time;
        }
    }
}
