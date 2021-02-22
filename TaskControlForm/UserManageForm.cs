using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TaskControl;
using System.Linq;

namespace TaskControlForm
{
    public partial class UserManageForm : Form
    {
        /// <summary>
        /// Initialize.
        /// </summary>
        public UserManageForm()
        {
            InitializeComponent();
            Form1.UsersList = saveProject.LoadUserList();
            this.listBox1.Items.Clear();
            this.listBox1.Items.AddRange(Form1.UsersList.Select(e => e.Name).ToArray());
        }
        /// <summary>
        /// Create user.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.textBox1.Text.Length == 0 || Form1.UsersList.Select(e => e.Name).Contains(this.textBox1.Text))
                    throw new ArgumentException("Can't create user with this name!");
                Form1.UsersList.Add(new User(this.textBox1.Text));
                this.listBox1.Items.Clear();
                this.listBox1.Items.AddRange(Form1.UsersList.Select(e => e.Name).ToArray());
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Save to the file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserManageForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            saveProject.saveUserList(Form1.UsersList);
        }
        /// <summary>
        /// Delete user.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedItem == null)
                return;
            Form1.UsersList.Remove(Form1.UsersList.Find(e => e.Name == this.listBox1.SelectedItem.ToString()));
            this.listBox1.Items.Clear();
            this.listBox1.Items.AddRange(Form1.UsersList.Select(e => e.Name).ToArray());

        }
    }
}
