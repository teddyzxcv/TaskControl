using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TaskControl;

namespace TaskControlForm
{
    public partial class Form1 : Form
    {
        public static List<User> UsersList = new List<User>();
        static List<Project> ProjectList = saveProject.LoadFileInfo();

        /// <summary>
        /// Create project.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="mtn"></param>
        static void CreateProject(string name, int mtn)
        {
            Project prj = new Project(name, mtn);
            ProjectList.Add(prj);
        }

        public Form1()
        {
            InitializeComponent();
            RefreshTree();
            this.treeView1.ContextMenuStrip = this.contextMenuStrip1;
        }
        /// <summary>
        /// Refresh the tree view.
        /// </summary>
        private void RefreshTree()
        {
            try
            {
                var savestate = this.treeView1.Nodes.GetExpansionState();
                ProjectList = saveProject.LoadFileInfo();
                UsersList = saveProject.LoadUserList();
                this.treeView1.Nodes.Clear();
                List<TreeNode> prjTree = new List<TreeNode>();
                for (int i = 0; i < ProjectList.Count; i++)
                {
                    TreeNode prjnode = new TreeNode(ProjectList[i].Name);
                    RefreshTask(prjnode.Nodes, ProjectList[i].TaskList);
                    prjTree.Add(prjnode);
                }
                this.treeView1.Nodes.AddRange(prjTree.ToArray());
                treeView1.Nodes.SetExpansionState(savestate);
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        /// <summary>
        /// Refresh task info.
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="tasklist"></param>
        private void RefreshTask(TreeNodeCollection nodes, List<Task> tasklist)
        {
            for (int i = 0; i < tasklist.Count; i++)
            {
                TreeNode node = new TreeNode(tasklist[i].Name);
                if (tasklist[i].SubTaskList.Count != 0)
                    RefreshTask(node.Nodes, tasklist[i].SubTaskList);
                nodes.Add(node);
            }
        }

        /// <summary>
        /// Create new project.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newPrjStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                CreatePrjForm prjform = new CreatePrjForm();
                if (prjform.ShowDialog() != DialogResult.OK)
                    return;
                if (ProjectList.Select(e => e.Name).Contains(prjform.PrjName))
                    throw new ArgumentException("No! No repeat!");

                CreateProject(prjform.PrjName, prjform.MaxTaskNumber);
                saveProject.SaveProjectList(ProjectList);
                RefreshTree();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        /// <summary>
        /// Get listbox refreshed after change seleted in tree view.
        /// </summary>

        private void listboxrefresh()
        {
            try
            {
                this.listBox1.Items.Clear();
                List<string> path = this.treeView1.SelectedNode.FullPath.Split('\\').ToList();
                Project targetProject = ProjectList.Find(e => e.Name == path[0]);
                List<string> info = new List<string>();
                if (path.Count == 1)
                {
                    info.Add(">>> Project Name: " + targetProject.Name);
                    info.Add(">>> Project Max Task Number: " + targetProject.MaxTaskNumber);
                    info.Add(">>> Project task group by status:");
                    info.Add("    >> Default:");
                    if (targetProject.GroupByStatus().ContainsKey(Task.Status.Default))
                        targetProject.GroupByStatus()[Task.Status.Default].Select(e => @"      \_>" + e.Name).ToList().ForEach(info.Add);
                    info.Add("    >> At Work:");
                    if (targetProject.GroupByStatus().ContainsKey(Task.Status.AtWork))
                        targetProject.GroupByStatus()[Task.Status.AtWork].Select(e => @"      \_>" + e.Name).ToList().ForEach(info.Add);
                    info.Add("    >> End Work:");
                    if (targetProject.GroupByStatus().ContainsKey(Task.Status.EndWork))
                        targetProject.GroupByStatus()[Task.Status.EndWork].Select(e => @"      \_>" + e.Name).ToList().ForEach(info.Add);
                }
                List<Task> targetTaskList = targetProject.TaskList;
                for (int i = 1; i < path.Count; i++)
                {
                    Task targetTask = targetTaskList.Find(e => e.Name == path[i]);
                    info.Clear();
                    info.Add(">>> Task Name: " + targetTask.Name);
                    info.Add(">>> Type: " + targetTask.GetType());
                    info.Add(">>> Task Create Time: " + targetTask.CreateTime.ToString());
                    info.Add(">>> Subtask count: " + targetTask.SubTaskList.Count);
                    info.Add(">>> Status: " + targetTask.GetStatus().ToString());
                    info.Add(">>> Assigned users: ");
                    info.AddRange(targetTask.UserList.Select(e => "   <+> " + e.Name));
                    info.Add(">>> SubTask Group By Status");
                    info.Add("    >> Default:");
                    if (targetTask.GroupByStatus().ContainsKey(Task.Status.Default))
                        targetTask.GroupByStatus()[Task.Status.Default].Select(e => @"      \_>" + e.Name).ToList().ForEach(info.Add);
                    info.Add("    >> At Work:");
                    if (targetTask.GroupByStatus().ContainsKey(Task.Status.AtWork))
                        targetTask.GroupByStatus()[Task.Status.AtWork].Select(e => @"      \_>" + e.Name).ToList().ForEach(info.Add);
                    info.Add("    >> End Work:");
                    if (targetTask.GroupByStatus().ContainsKey(Task.Status.EndWork))
                        targetTask.GroupByStatus()[Task.Status.EndWork].Select(e => @"      \_>" + e.Name).ToList().ForEach(info.Add);
                    targetTaskList = targetTask.SubTaskList;
                }
                this.listBox1.Text = info[0];
                this.listBox1.Items.AddRange(info.ToArray());
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Tree view refresh.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            listboxrefresh();
        }
        /// <summary>
        /// Create new task.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newTaskStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.treeView1.SelectedNode == null)
                    throw new ArgumentException("Plz choose a task or project!");
                List<string> path = this.treeView1.SelectedNode.FullPath.Split('\\').ToList();
                var node = this.treeView1.SelectedNode;
                CreateTaskForm taskform = new CreateTaskForm();
                if (taskform.ShowDialog() != DialogResult.OK)
                    return;
                var task = new Task(taskform.TaskName);
                switch (taskform.TypeName)
                {
                    case ("TaskTask"):
                        task = new TaskTask(taskform.TaskName);
                        break;
                    case ("EpicTask"):
                        task = new EpicTask(taskform.TaskName);
                        break;
                    case ("StoryTask"):
                        task = new StoryTask(taskform.TaskName);
                        break;
                    case ("BugTask"):
                        task = new BugTask(taskform.TaskName);
                        break;
                }
                task.CreateTime = DateTime.Now;
                if (path.Count == 1)
                    ProjectList.Find(e => e.Name == treeView1.SelectedNode.Text).AddTask(task);
                else
                {
                    Project targetProject = ProjectList.Find(e => e.Name == path[0]);
                    List<Task> targetTaskList = targetProject.TaskList;
                    Task targetTask = targetTaskList.Find(e => e.Name == path[1]);
                    for (int i = 1; i < path.Count; i++)
                    {
                        targetTask = targetTaskList.Find(e => e.Name == path[i]);
                        targetTaskList = targetTask.SubTaskList;
                    }
                    targetTask.AddTask(task);
                }
                saveProject.SaveProjectList(ProjectList);
                RefreshTree();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        /// <summary>
        /// User strip.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void userStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                UserManageForm umf = new UserManageForm();
                umf.Show();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Delete button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> path = this.treeView1.SelectedNode.FullPath.Split('\\').ToList();
                if (path.Count == 1)
                    ProjectList.Remove(ProjectList.Find(e => e.Name == this.treeView1.SelectedNode.Text));
                else
                {
                    Project targetProject = ProjectList.Find(e => e.Name == path[0]);
                    List<Task> targetTaskList = targetProject.TaskList;
                    List<Task> mothertargetTaskList = targetProject.TaskList;
                    Task targetTask = targetTaskList.Find(e => e.Name == path[1]);
                    for (int i = 1; i < path.Count; i++)
                    {

                        mothertargetTaskList = targetTaskList;
                        targetTask = targetTaskList.Find(e => e.Name == path[i]);
                        targetTaskList = targetTask.SubTaskList;
                    }
                    mothertargetTaskList.Remove(targetTask);
                }
                saveProject.SaveProjectList(ProjectList);
                RefreshTree();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
        /// <summary>
        /// Change project/task name.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void changeNameStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> path = this.treeView1.SelectedNode.FullPath.Split('\\').ToList();
                if (path.Count == 1)
                {
                    RenameForm rnf = new RenameForm();
                    rnf.ShowDialog();
                    if (!ProjectList.Select(e => e.Name).Contains(rnf.RenameName))
                        ProjectList.Find(e => e.Name == this.treeView1.SelectedNode.Text).Name = rnf.RenameName;
                    else
                        throw new ArgumentException("Cant change task name!");
                }
                else
                {
                    throw new ArgumentException("Cant change task name!");
                }
                saveProject.SaveProjectList(ProjectList);
                RefreshTree();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Task management.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void taskmanageStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> path = this.treeView1.SelectedNode.FullPath.Split('\\').ToList();
                if (path.Count == 1)
                    throw new ArgumentException("Can't change project!");
                Project targetProject = ProjectList.Find(e => e.Name == path[0]);
                List<Task> targetTaskList = targetProject.TaskList;
                Task targetTask = targetTaskList.Find(e => e.Name == path[1]);
                for (int i = 1; i < path.Count; i++)
                {

                    targetTask = targetTaskList.Find(e => e.Name == path[i]);
                    targetTaskList = targetTask.SubTaskList;
                }
                TaskManageForm tmf = new TaskManageForm(targetTask);
                if (tmf.ShowDialog() == DialogResult.Cancel)
                    return;
                saveProject.SaveProjectList(ProjectList);
                listboxrefresh();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
