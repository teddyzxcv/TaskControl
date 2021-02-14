using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TaskControlForm
{
    public partial class CreatePrjForm : Form
    {

        public int MaxTaskNumber = 1;
        public string PrjName = "";
        public CreatePrjForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MaxTaskNumber = (int)this.numericUpDown1.Value;
            PrjName = this.textBox1.Text;
            this.Close();
        }

        private void CreatePrjForm_Load(object sender, EventArgs e)
        {

        }
    }
}
