using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using TaskControl;
namespace TaskControlForm
{
    public partial class TaskManageForm : Form
    {
        public Task SelectedTask = new Task();
        /// <summary>
        /// Manage task.
        /// </summary>
        /// <param name="selectedtask"></param>
        public TaskManageForm(Task selectedtask)
        {
            InitializeComponent();
            SelectedTask = selectedtask;
            string[] arr = Form1.UsersList.Select(e => e.Name).ToArray();
            this.checkedListBox1.Items.AddRange(arr);
            for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
            {
                if (SelectedTask.UserList.Select(e => e.Name).Contains(arr[i]))
                    this.checkedListBox1.SetItemChecked(i, true);
            }
            switch (SelectedTask.GetStatus())
            {
                case (Task.Status.Default):
                    this.radioButton1.Checked = true;
                    break;
                case (Task.Status.AtWork):
                    this.radioButton2.Checked = true;
                    break;
                case (Task.Status.EndWork):
                    this.radioButton3.Checked = true;
                    break;
            }

        }
        /// <summary>
        /// Save the change.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (this.radioButton1.Checked)
                SelectedTask.SetStatus(Task.Status.Default);
            if (this.radioButton2.Checked)
                SelectedTask.SetStatus(Task.Status.AtWork);
            if (this.radioButton3.Checked)
                SelectedTask.SetStatus(Task.Status.EndWork);
            List<User> userlist = new List<User>();
            SelectedTask.UserList.Clear();
            foreach (var item in this.checkedListBox1.CheckedItems)
            {
                SelectedTask.AddUser(Form1.UsersList.Find(e => e.Name == item.ToString()));
            }


            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
