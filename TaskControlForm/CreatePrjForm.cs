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
            if (this.textBox1.Text.Length == 0)
            {
                this.DialogResult = DialogResult.Cancel;
                MessageBox.Show("Give him a name!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            MaxTaskNumber = (int)this.numericUpDown1.Value;
            PrjName = this.textBox1.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void CreatePrjForm_Load(object sender, EventArgs e)
        {

        }
    }
}
