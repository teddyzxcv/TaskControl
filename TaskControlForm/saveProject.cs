using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Linq;
using System.IO;
using System.Xml;
using TaskControl;
namespace TaskControlForm
{
    /// <summary>
    /// Class for check tree view.<see langword="abstract"/>
    /// </summary>
    public static class TreeViewExtensions
    {
        /// <summary>
        /// Save all tree expansion states.
        /// </summary>
        /// <param name="nodes"></param>
        /// <returns></returns>
        public static List<string> GetExpansionState(this TreeNodeCollection nodes)
        {
            return nodes.Descendants()
                        .Where(n => n.IsExpanded)
                        .Select(n => n.FullPath)
                        .ToList();
        }
        /// <summary>
        /// Set tree expansion state.
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="savedExpansionState"></param>
        public static void SetExpansionState(this TreeNodeCollection nodes, List<string> savedExpansionState)
        {
            foreach (var node in nodes.Descendants()
                                      .Where(n => savedExpansionState.Contains(n.FullPath)))
            {
                node.Expand();
            }
        }

        public static IEnumerable<TreeNode> Descendants(this TreeNodeCollection c)
        {
            foreach (var node in c.OfType<TreeNode>())
            {
                yield return node;

                foreach (var child in node.Nodes.Descendants())
                {
                    yield return child;
                }
            }
        }
    }
    public class saveProject
    {
        /// <summary>
        /// Path to save file xml.
        /// </summary>
        static string PathToSaving = "ProjectSave.xml";
        /// <summary>
        /// Load user list.
        /// </summary>
        /// <returns></returns>
        public static List<User> LoadUserList()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(PathToSaving);
            List<User> userList = new List<User>();
            List<string> userstrlist = doc.SelectSingleNode("TaskControl").SelectSingleNode("UserManageList").SelectNodes("User").Cast<XmlNode>().Select(e => e.InnerText).ToList();
            if (userstrlist.Count != 0)
            {
                foreach (var item in userstrlist)
                {
                    User user = new User(item);
                    userList.Add(user);
                }
            }
            return userList;
        }
        /// <summary>
        /// Save user list.
        /// </summary>
        /// <param name="UserList"></param>
        public static void saveUserList(List<User> UserList)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(PathToSaving);
            XmlNode root = doc.SelectSingleNode("TaskControl");
            root.SelectSingleNode("UserManageList").RemoveAll();
            XmlNode usernodelist = root.SelectSingleNode("UserManageList");
            for (int i = 0; i < UserList.Count; i++)
            {
                XmlElement newUser = doc.CreateElement("User");
                newUser.InnerText = UserList.Select(e => e.Name).ToList()[i];
                usernodelist.AppendChild(newUser);
            }
            doc.Save(PathToSaving);
        }
        /// <summary>
        /// Load task list.
        /// </summary>
        /// <param name="nodes"></param>
        /// <returns></returns>
        public static List<Task> LoadTaskList(List<XmlNode> nodes)
        {
            List<Task> Output = new List<Task>();
            for (int i = 0; i < nodes.Count; i++)
            {
                Task task = new Task(nodes[i].Attributes["Name"].Value);
                switch (nodes[i].Attributes["Type"].Value)
                {
                    case ("TaskTask"):
                        task = new TaskTask(nodes[i].Attributes["Name"].Value);
                        break;
                    case ("EpicTask"):
                        task = new EpicTask(nodes[i].Attributes["Name"].Value);
                        break;
                    case ("StoryTask"):
                        task = new StoryTask(nodes[i].Attributes["Name"].Value);
                        break;
                    case ("BugTask"):
                        task = new BugTask(nodes[i].Attributes["Name"].Value);
                        break;
                }
                task.SetStatus((Task.Status)Enum.Parse(typeof(Task.Status), nodes[i].Attributes["CurrentStatus"].Value));
                task.CreateTime = DateTime.Parse(nodes[i].Attributes["CreateTime"].Value);
                List<string> usernames = nodes[i].SelectSingleNode("UserList").SelectNodes("User").Cast<XmlNode>().Select(e => e.Attributes["Name"].Value).ToList();
                List<User> UserList = new List<User>();
                for (int j = 0; j < usernames.Count; j++)
                {
                    UserList.Add(new User(usernames[j]));
                }
                task.UserList = UserList;
                List<Task> SubTasks = LoadTaskList(nodes[i].SelectSingleNode("SubTaskList").SelectNodes("Task").Cast<XmlNode>().ToList());
                task.SubTaskList = SubTasks;
                Output.Add(task);
            }
            return Output;
        }
        /// <summary>
        /// Save project list.
        /// </summary>
        /// <param name="prjList"></param>

        public static void SaveProjectList(List<Project> prjList)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(PathToSaving);
            XmlNode root = doc.SelectSingleNode("TaskControl");
            root.SelectSingleNode("ProjectList").RemoveAll();
            XmlNode prjnodelist = root.SelectSingleNode("ProjectList");
            for (int i = 0; i < prjList.Count; i++)
            {
                XmlElement newProject = doc.CreateElement("Project");
                XmlAttribute ProjectName = doc.CreateAttribute("Name");
                XmlAttribute ProjectMaxTaskNumber = doc.CreateAttribute("MaxTaskNumber");
                ProjectName.Value = prjList[i].Name;
                ProjectMaxTaskNumber.Value = prjList[i].MaxTaskNumber.ToString();
                newProject.SetAttributeNode(ProjectName);
                newProject.SetAttributeNode(ProjectMaxTaskNumber);
                XmlElement newTaskList = doc.CreateElement("TaskList");
                SaveTask(ref newTaskList, prjList[i].TaskList, doc);
                newProject.AppendChild(newTaskList);
                prjnodelist.AppendChild(newProject);
            }
            doc.Save(PathToSaving);
        }
        /// <summary>
        /// Save task.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="tasklist"></param>
        /// <param name="doc"></param>
        private static void SaveTask(ref XmlElement node, List<Task> tasklist, XmlDocument doc)
        {
            for (int i = 0; i < tasklist.Count; i++)
            {
                XmlElement newTask = doc.CreateElement("Task");
                XmlAttribute taskName = doc.CreateAttribute("Name");
                XmlAttribute taskCreateTime = doc.CreateAttribute("CreateTime");
                XmlAttribute taskType = doc.CreateAttribute("Type");
                XmlAttribute taskStatus = doc.CreateAttribute("CurrentStatus");
                taskCreateTime.Value = tasklist[i].CreateTime.ToString();
                taskName.Value = tasklist[i].Name;
                taskType.Value = tasklist[i].GetType().Name;
                taskStatus.Value = tasklist[i].GetStatus().ToString();
                newTask.SetAttributeNode(taskName);
                newTask.SetAttributeNode(taskCreateTime);
                newTask.SetAttributeNode(taskType);
                newTask.SetAttributeNode(taskStatus);
                XmlElement newUserList = doc.CreateElement("UserList");
                for (int j = 0; j < tasklist[i].UserList.Count; j++)
                {
                    XmlElement user = doc.CreateElement("User");
                    XmlAttribute username = doc.CreateAttribute("Name");
                    username.Value = tasklist[i].UserList[j].Name;
                    user.SetAttributeNode(username);
                    newUserList.AppendChild(user);
                }
                XmlElement newSubList = doc.CreateElement("SubTaskList");
                SaveTask(ref newSubList, tasklist[i].SubTaskList, doc);
                newTask.AppendChild(newUserList);
                newTask.AppendChild(newSubList);
                node.AppendChild(newTask);

            }
        }
        /// <summary>
        /// Load file info.
        /// </summary>
        /// <returns></returns>

        public static List<Project> LoadFileInfo()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(PathToSaving);
            XmlNodeList ProjectList = doc.SelectNodes("//ProjectList/Project");
            List<Project> prjList = new List<Project>();
            if (ProjectList != null)
            {
                foreach (XmlNode item in ProjectList)
                {
                    Project prj = new Project(item.Attributes["Name"].Value, int.Parse(item.Attributes["MaxTaskNumber"].Value));
                    prj.TaskList = LoadTaskList(item.SelectSingleNode("TaskList").SelectNodes("Task").Cast<XmlNode>().ToList());
                    prjList.Add(prj);
                }
            }
            return prjList;
        }

    }
}