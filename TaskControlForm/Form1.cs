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
        static List<User> UsersList = new List<User>();
        static List<Project> ProjectList = saveProject.LoadFileInfo();

        static void CreateTask(Task mothertask, Task task)
        {
            mothertask.AddTask(task);
        }
        static void CreateTask(Project project, Task task)
        {
            project.AddTask(task);
        }
        static void CreateUser(string name)
        {
            User user = new User(name);
            UsersList.Add(user);
        }
        static void DeleteUser(User user)
        {
            UsersList.Remove(user);
        }
        static string ViewUsers()
        {
            return String.Join('\n', UsersList);
        }
        static void CreateProject(string name, int mtn)
        {
            Project prj = new Project(name, mtn);
            ProjectList.Add(prj);
        }
        static void ChangeProjectName(Project prj, string name)
        {
            prj.Name = name;
        }
        static string ViewProjects()
        {
            List<string> stringprjlist = ProjectList.Select(e => e.ToString()).ToList();
            return String.Join('\n', stringprjlist);
        }
        static void DeleteProject(string name)
        {
            ProjectList.Remove(ProjectList.Find(e => e.Name == name));
        }

        static string ViewTaskList(string name)
        {
            Project prj = ProjectList.Find(e => e.Name == name);
            return String.Join('\n', prj.TaskList.Select(e => e.ToString()));
        }


        public Form1()
        {
            InitializeComponent();
            RefreshTree();
        }
        private void RefreshTree()
        {
            try
            {
                var savestate = this.treeView1.Nodes.GetExpansionState();
                ProjectList = saveProject.LoadFileInfo();
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
            catch
            {
                MessageBox.Show("Something go wrong!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
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


        private void newPrjStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                CreatePrjForm prjform = new CreatePrjForm();
                prjform.ShowDialog();
                CreateProject(prjform.PrjName, prjform.MaxTaskNumber);
                saveProject.SaveProjectList(ProjectList);
                RefreshTree();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch
            {
                MessageBox.Show("Something go wrong!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }





        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                this.listBox1.Items.Clear();
                List<string> path = this.treeView1.SelectedNode.FullPath.Split('\\').ToList();
                Project targetProject = ProjectList.Find(e => e.Name == path[0]);
                List<string> info = new List<string>();
                if (path.Count == 1)
                {
                    info.Add("Project Name: " + targetProject.Name);
                    info.Add("Project Max Task Number: " + targetProject.MaxTaskNumber);
                }
                List<Task> targetTaskList = targetProject.TaskList;
                for (int i = 1; i < path.Count; i++)
                {
                    Task targetTask = targetTaskList.Find(e => e.Name == path[i]);
                    info.Clear();
                    info.Add("Task Name: " + targetTask.Name);
                    info.Add("Type: " + targetTask.GetType());
                    info.Add("Task Create Time: " + targetTask.CreateTime.ToString());
                    info.Add("Subtask count: " + targetTask.SubTaskList.Count);
                    info.Add("Status: " + targetTask.GetStatus().ToString());
                    info.Add("Assigned users: ");
                    info.AddRange(targetTask.UserList.Select(e => e.Name));
                    targetTaskList = targetTask.SubTaskList;
                }
                this.listBox1.Text = info[0];
                this.listBox1.Items.AddRange(info.ToArray());
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch
            {
                MessageBox.Show("Something go wrong!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void newTaskStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> path = this.treeView1.SelectedNode.FullPath.Split('\\').ToList();
                var node = this.treeView1.SelectedNode;
                CreateTaskForm taskform = new CreateTaskForm();
                taskform.ShowDialog();
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
            catch
            {
                MessageBox.Show("Something go wrong!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
