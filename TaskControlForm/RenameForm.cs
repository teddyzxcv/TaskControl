using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TaskControlForm
{
    public partial class RenameForm : Form
    {
        public RenameForm()
        {
            InitializeComponent();
        }
        public string RenameName = "";

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text.Length == 0)
            {
                MessageBox.Show("Empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            RenameName = this.textBox1.Text;
            
            this.Close();
        }
    }
}
