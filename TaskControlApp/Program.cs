using System;
using TaskControl;
using System.Collections.Generic;
using System.Linq;
using ConsoleTools;

namespace TaskControlApp
{
    class Program
    {
        static List<User> UsersList = new List<User>();
        static List<Project> ProjectList = new List<Project>();
        /// <summary>
        /// Create user.
        /// </summary>
        /// <param name="name"></param>
        static void CreateUser(string name)
        {
            User user = new User(name);
            if (UsersList.Select(e => e.Name).Contains(name))
                throw new ArgumentException("N0 repeat user name!");
            UsersList.Add(user);
            saveProject.saveUserList(UsersList);
        }
        /// <summary>
        /// Delete user.
        /// </summary>
        /// <param name="user"></param>
        static void DeleteUser(User user)
        {
            UsersList.Remove(user);
            saveProject.saveUserList(UsersList);
        }
        /// <summary>
        /// Create prpject.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="mtn"></param>
        static void CreateProject(string name, int mtn)
        {
            if (name == "")
                throw new ArgumentException("Name cant be empty!");
            if (ProjectList.Select(e => e.Name).Contains(name))
                throw new ArgumentException("Project name cant repeat!");
            Project prj = new Project(name, mtn);
            ProjectList.Add(prj);
        }
        /// <summary>
        /// Delete porject.
        /// </summary>
        /// <param name="name"></param>

        static void DeleteProject(string name)
        {
            ProjectList.Remove(ProjectList.Find(e => e.Name == name));
        }
        /// <summary>
        /// Main.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            while (true)
                try
                {
                    ProjectList = saveProject.LoadFileInfo();
                    UsersList = saveProject.LoadUserList();
                    var MainMenu = new ConsoleMenu();
                    RefreshMainMenu(ref MainMenu);
                    MainMenu.Show();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message + "Press any button to try again!");
                    Console.ReadKey();
                }
        }
        /// <summary>
        /// Get main menu refreshed.
        /// </summary>
        /// <param name="m"></param>
        static void RefreshMainMenu(ref ConsoleMenu m)
        {
            var MainMenu = new ConsoleMenu();
            MainMenu.Configure(config =>
            {
                config.WriteHeaderAction = () =>
                {
                    Console.WriteLine("Main Menu: view project list and create new project.");
                };
            });
            // Help.
            MainMenu.Add("Help", () =>
            {
                Console.WriteLine("Press Enter to get in, up and down arrow to choose");
                Console.WriteLine("Type of task: BugTask, TaskTask, EpicTask and StoryTask.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            });
            // User manage.
            MainMenu.Add("User Manage", () =>
            {
                UserManageMenu().Show();
            });
            // Create new project.
            MainMenu.Add("Create new project", () =>
            {
                CreatePorjectMenu();
                RefreshMainMenu(ref MainMenu);
                MainMenu.Show();
            });
            // Get project list.
            for (int i = 0; i < ProjectList.Count; i++)
            {
                Project prj = ProjectList[i];
                RefreshProjectMenu(MainMenu, prj);
            }
            m = MainMenu;
        }
        /// <summary>
        /// Refresh project menu.
        /// </summary>
        /// <param name="MainMenu"></param>
        /// <param name="prj"></param>
        /// <returns></returns>
        static ConsoleMenu RefreshProjectMenu(ConsoleMenu MainMenu, Project prj)
        {
            string prjname = prj.Name;
            var ProjectMenu = new ConsoleMenu();
            // Get project name.
            ProjectMenu.Configure(config =>
            {
                config.WriteHeaderAction = () =>
            {
                Console.WriteLine("Project: " + prj.Name + "  Task number: " + prj.TaskList.Count);
            };
            });
            // All task in default.
            ProjectMenu.Add("Default:", () => { });
            for (int j = 0; j < prj.TaskList.Count; j++)
            {
                Task task = prj.TaskList[j];
                if (task.GetStatus() == Task.Status.Default)
                    ProjectMenu.Add("     \\---> Task: " + prj.TaskList[j].Name, () =>
                    {
                        TaskMenu(ProjectMenu, task, prj, MainMenu, false, new Task()).Show();
                    });
            }
            // All task at work.
            ProjectMenu.Add("AtWork:", () => { });
            for (int j = 0; j < prj.TaskList.Count; j++)
            {
                Task task = prj.TaskList[j];
                if (task.GetStatus() == Task.Status.AtWork)
                    ProjectMenu.Add("     \\---> Task: " + prj.TaskList[j].Name, () =>
                    {
                        TaskMenu(ProjectMenu, task, prj, MainMenu, false, new Task()).Show();
                    });
            }
            // All task end work.
            ProjectMenu.Add("EndWork:", () => { });
            for (int j = 0; j < prj.TaskList.Count; j++)
            {
                Task task = prj.TaskList[j];
                if (task.GetStatus() == Task.Status.EndWork)
                    ProjectMenu.Add("     \\---> Task: " + prj.TaskList[j].Name, () =>
                    {
                        TaskMenu(ProjectMenu, task, prj, MainMenu, false, new Task()).Show();
                    });
            }

            // Create new task.

            ProjectMenu.Add("Create new Task", () =>
            {
                CreateTaskMenu(prj);
                RefreshMainMenu(ref MainMenu);
                RefreshProjectMenu(MainMenu, prj).Show();
            });
            // Rename project.
            ProjectMenu.Add("Rename this project", () =>
            {
                RenameProjectMenu(prj);
                RefreshMainMenu(ref MainMenu);
            });
            // Delete project.
            ProjectMenu.Add("Delete this project", () =>
            {
                DeleteProject(prjname);
                saveProject.SaveProjectList(ProjectList);
                RefreshMainMenu(ref MainMenu);
                MainMenu.Show();
            });
            // Go back.
            ProjectMenu.Add("Back", () =>
            {
                RefreshMainMenu(ref MainMenu);
                MainMenu.Show();
            });
            MainMenu.Add("Project: " + prj.Name, ProjectMenu.Show);
            return ProjectMenu;
        }
        /// <summary>
        /// Create Task menu for project.
        /// </summary>
        /// <param name="prj"></param>
        static void CreateTaskMenu(Project prj)
        {
            while (true)
                if (prj.MaxTaskNumber > prj.TaskList.Count)
                    try
                    {
                        string taskName = "";
                        string type = "";
                        Console.WriteLine("Input task name:");
                        taskName = Console.ReadLine();
                        Console.WriteLine("Input task type:");
                        type = Console.ReadLine();
                        Task task = new Task(taskName);
                        switch (type)
                        {
                            case ("TaskTask"):
                                task = new TaskTask(taskName);
                                break;
                            case ("EpicTask"):
                                task = new EpicTask(taskName);
                                break;
                            case ("StoryTask"):
                                task = new StoryTask(taskName);
                                break;
                            case ("BugTask"):
                                task = new BugTask(taskName);
                                break;
                            default:
                                throw new ArgumentException("Cant find this type!");
                        }
                        prj.AddTask(task);
                        saveProject.SaveProjectList(ProjectList);
                        break;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error! : " + e.Message + " Plz try again...");
                    }
                else
                {
                    Console.WriteLine("No can do, reach max task number...  Pres any button to continue...");
                    Console.ReadKey();
                    break;
                }
        }
        /// <summary>
        /// Create task menu for task.
        /// </summary>
        /// <param name="mtask"></param>
        static void CreateTaskMenu(Task mtask)
        {
            while (true)
                try
                {
                    string taskName = "";
                    string type = "";
                    Console.WriteLine("Input task name:");
                    taskName = Console.ReadLine();
                    Console.WriteLine("Input task type:");
                    type = Console.ReadLine();
                    Task task = new Task(taskName);
                    switch (type)
                    {
                        case ("TaskTask"):
                            task = new TaskTask(taskName);
                            break;
                        case ("EpicTask"):
                            task = new EpicTask(taskName);
                            break;
                        case ("StoryTask"):
                            task = new StoryTask(taskName);
                            break;
                        case ("BugTask"):
                            task = new BugTask(taskName);
                            break;
                        default:
                            throw new ArgumentException("Cant find this type!");
                    }
                    mtask.AddTask(task);
                    saveProject.SaveProjectList(ProjectList);
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error! : " + e.Message + " Plz try again...");
                }
        }
        /// <summary>
        /// Create user.
        /// </summary>
        static void CreateUserMenu()
        {
            while (true)
                try
                {
                    Console.WriteLine("Input user name");
                    string username = Console.ReadLine();
                    CreateUser(username);
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error!: " + e.Message + " Plz try again...");
                }
        }
        /// <summary>
        /// User management.
        /// </summary>
        /// <returns></returns>
        static ConsoleMenu UserManageMenu()
        {
            ConsoleMenu umm = new ConsoleMenu();
            umm.Configure(config =>
            {
                config.WriteHeaderAction = () =>
                {
                    Console.WriteLine("User Manager");
                };
            });
            // Create new user.
            umm.Add("Create new user", () =>
            {
                CreateUserMenu();
                umm.CloseMenu();
                umm = UserManageMenu();
                umm.Show();
            });
            // Show every user.
            for (int i = 0; i < UsersList.Count; i++)
            {
                User user = UsersList[i];
                umm.Add(user.Name, () =>
                {
                    umm.CloseMenu();
                    ConsoleMenu um = new ConsoleMenu();
                    um.Configure(config =>
                    {
                        config.WriteHeaderAction = () =>
                        {
                            Console.WriteLine(user.Name);
                        };
                    });
                    um.Add("Delete this user", () =>
                    {
                        DeleteUser(user);
                        umm = UserManageMenu();
                        um.CloseMenu();
                        umm.Show();
                    });
                    um.Add("Back", () =>
                    {
                        um.CloseMenu();
                    });
                    um.Show();
                });
            }
            // Go back.
            umm.Add("Back", umm.CloseMenu);
            return umm;
        }
        /// <summary>
        /// Rename the certain project.
        /// </summary>
        /// <param name="prj"></param>
        static void RenameProjectMenu(Project prj)
        {
            while (true)
                try
                {
                    Console.WriteLine("Plz input new project name:");
                    string name = Console.ReadLine();
                    if (ProjectList.Select(e => e.Name).Contains(name))
                        throw new ArgumentException("No repeat project name!");
                    prj.Name = name;
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error!: " + e.Message + " Plz try again...");
                }
        }
        /// <summary>
        /// Refresh sub task menu after delete or add new task.
        /// </summary>
        /// <param name="task"></param>
        /// <param name="mm"></param>
        /// <param name="prj"></param>
        /// <param name="chum"></param>
        /// <returns></returns>
        static ConsoleMenu RefreshSubTaskMenu(Task task, ConsoleMenu mm, Project prj, ConsoleMenu chum)
        {
            ConsoleMenu um = new ConsoleMenu();
            um.Configure(config =>
            {
                config.WriteHeaderAction = () =>
                {
                    Console.WriteLine("Subtask manage in " + task.Name);
                };
            });
            um.Add("Create new Task", () =>
            {
                CreateTaskMenu(task);
                RefreshSubTaskMenu(task, mm, prj, chum).Show();
            });
            um.Add("Default:", () => { });
            for (int i = 0; i < task.SubTaskList.Count; i++)
            {
                Task subtask = task.SubTaskList[i];
                if (subtask.GetStatus() == Task.Status.Default)
                    um.Add("     \\--->Task: " + subtask.Name, () =>
                    {
                        TaskMenu(chum, subtask, prj, mm, true, task).Show();
                    });
            }
            um.Add("AtWork:", () => { });
            for (int i = 0; i < task.SubTaskList.Count; i++)
            {
                Task subtask = task.SubTaskList[i];
                if (subtask.GetStatus() == Task.Status.AtWork)
                    um.Add("     \\--->Task: " + subtask.Name, () =>
                    {
                        TaskMenu(chum, subtask, prj, mm, true, task).Show();
                    });
            }
            um.Add("EndWork:", () => { });
            for (int i = 0; i < task.SubTaskList.Count; i++)
            {
                Task subtask = task.SubTaskList[i];
                if (subtask.GetStatus() == Task.Status.EndWork)
                    um.Add("     \\--->Task: " + subtask.Name, () =>
                    {
                        TaskMenu(chum, subtask, prj, mm, true, task).Show();
                    });
            }
            um.Add("Back", () =>
            {
                chum.Show();
            });
            return um;
        }
        /// <summary>
        /// Task menu.
        /// </summary>
        /// <param name="up"></param>
        /// <param name="task"></param>
        /// <param name="prj"></param>
        /// <param name="mm"></param>
        /// <param name="IsSubtask"></param>
        /// <param name="mothertask"></param>
        /// <returns></returns>
        static ConsoleMenu TaskMenu(ConsoleMenu up, Task task, Project prj, ConsoleMenu mm, bool IsSubtask, Task mothertask)
        {
            ConsoleMenu chum = new ConsoleMenu();
            chum.Configure(config =>
            {
                config.WriteHeaderAction = () =>
                {
                    Console.WriteLine(task.Name + ": Type: " + task.GetType() + ", Status: " + task.GetStatus() + ", Create time: " + task.CreateTime);
                };
            });
            if (task.GetType() == typeof(EpicTask))
                chum.Add("Manage subtask", () =>
                {
                    RefreshSubTaskMenu(task, mm, prj, chum).Show();
                });
            // Add user to task.
            chum.Add("Add user to task", () =>
            {
                ConsoleMenu um = new ConsoleMenu();
                um.Configure(config =>
                {
                    config.WriteHeaderAction = () =>
                    {
                        Console.WriteLine("Choose user for " + task.Name + " from all user");
                    };
                });
                for (int i = 0; i < UsersList.Count; i++)
                {
                    User user = UsersList[i];
                    um.Add(user.Name, () =>
                    {
                        try
                        {
                            if (!task.UserList.Contains(user))
                                task.AddUser(user);
                            saveProject.SaveProjectList(ProjectList);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message + " Press any button to continue...");
                            Console.ReadKey();
                        }
                    });
                }
                um.Add("Back", () =>
                {
                    chum.Show();
                });
                um.Show();

            });
            // Delete user from task.
            chum.Add("Delete user from task", () =>
            {
                ConsoleMenu um = new ConsoleMenu();
                um.Configure(config =>
                {
                    config.WriteHeaderAction = () =>
                    {
                        Console.WriteLine("Delete user from " + task.Name);
                    };
                });
                for (int i = 0; i < task.UserList.Count; i++)
                {
                    User user = task.UserList[i];
                    um.Add(user.Name, () =>
                    {
                        task.DeleteUser(user);
                        saveProject.SaveProjectList(ProjectList);
                        chum.Show();
                    });
                }
                um.Add("Back", () =>
                {
                    chum.Show();
                });
                um.Show();
            });
            // Change status.
            chum.Add("Change status", () =>
            {
                ConsoleMenu um = new ConsoleMenu();
                um.Configure(config =>
                {
                    config.WriteHeaderAction = () =>
                    {
                        Console.WriteLine("Change status from " + task.GetStatus().ToString() + " to:");
                    };
                });
                um.Add("Default", () =>
                {
                    task.SetStatus(Task.Status.Default);
                    saveProject.SaveProjectList(ProjectList);
                    if (!IsSubtask)
                        chum = RefreshProjectMenu(mm, prj);
                    chum.Show();
                });
                um.Add("At work", () =>
                {
                    task.SetStatus(Task.Status.AtWork);
                    saveProject.SaveProjectList(ProjectList);
                    if (!IsSubtask)
                        chum = RefreshProjectMenu(mm, prj);
                    chum.Show();
                });
                um.Add("End work", () =>
                {
                    task.SetStatus(Task.Status.EndWork);
                    saveProject.SaveProjectList(ProjectList);
                    if (!IsSubtask)
                        chum = RefreshProjectMenu(mm, prj);
                    chum.Show();
                });
                um.Add("Back", () =>
                {
                    chum.Show();
                });
                um.Show();
            });
            // Delete task.
            chum.Add("Delete this task", () =>
            {
                if (IsSubtask)
                {
                    mothertask.DeleteTask(task);
                    saveProject.SaveProjectList(ProjectList);
                    RefreshProjectMenu(mm, prj).Show();
                }
                else
                {
                    prj.DeleteTask(task);
                    saveProject.SaveProjectList(ProjectList);
                    RefreshProjectMenu(mm, prj).Show();
                }
            });
            // Go back.
            chum.Add("Back", () =>
            {
                up.Show();
            });
            return chum;
        }
        /// <summary>
        /// Create project menu.
        /// </summary>
        static void CreatePorjectMenu()
        {
            while (true)
                try
                {
                    string prjname = "";
                    string maxn = "";
                    Console.WriteLine("Input project name:");
                    prjname = Console.ReadLine();
                    Console.WriteLine("Input max task number:");
                    maxn = Console.ReadLine();
                    CreateProject(prjname, int.Parse(maxn));
                    saveProject.SaveProjectList(ProjectList);
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error!: " + e.Message + " Plz try again...");
                }

        }

    }
}
