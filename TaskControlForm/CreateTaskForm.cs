using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TaskControlForm
{
    public partial class CreateTaskForm : Form
    {
        public string TaskName = "";
        public string TypeName = "";
        public CreateTaskForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Save new task.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (this.comboBox1.Text == null || this.textBox1.Text.Length == 0)
            {
                this.DialogResult = DialogResult.Cancel;
                MessageBox.Show("Give him a name!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            TaskName = this.textBox1.Text;
            TypeName = this.comboBox1.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void CreateTaskForm_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
    }
}
