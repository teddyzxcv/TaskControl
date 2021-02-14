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

        private void button1_Click(object sender, EventArgs e)
        {
            TaskName = this.textBox1.Text;
            TypeName = this.comboBox1.Text;
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
