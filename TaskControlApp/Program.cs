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

        static void CreateUser(string name)
        {
            User user = new User(name);
            if (UsersList.Select(e => e.Name).Contains(name))
                throw new ArgumentException("N0 repeat user name!");
            UsersList.Add(user);
            saveProject.saveUserList(UsersList);
        }
        static void DeleteUser(User user)
        {
            UsersList.Remove(user);
            saveProject.saveUserList(UsersList);

        }
        static void CreateProject(string name, int mtn)
        {
            if (name == "")
                throw new ArgumentException("Name cant be empty!");
            if (ProjectList.Select(e => e.Name).Contains(name))
                throw new ArgumentException("Project name cant repeat!");
            Project prj = new Project(name, mtn);
            ProjectList.Add(prj);
        }

        static void DeleteProject(string name)
        {
            ProjectList.Remove(ProjectList.Find(e => e.Name == name));
        }

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
            MainMenu.Add("Help", () =>
            {
                Console.WriteLine("Press Enter to get in, up and down arrow to choose");
                Console.WriteLine("Type of task: BugTask, TaskTask, EpicTask and StoryTask.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            });
            MainMenu.Add("User Manage", () =>
            {
                UserManageMenu().Show();
            });
            MainMenu.Add("Create new project", () =>
            {
                CreatePorjectMenu();
                RefreshMainMenu(ref MainMenu);
                MainMenu.Show();
            });
            for (int i = 0; i < ProjectList.Count; i++)
            {
                Project prj = ProjectList[i];
                RefreshProjectMenu(MainMenu, prj);
            }
            m = MainMenu;
        }
        static ConsoleMenu RefreshProjectMenu(ConsoleMenu MainMenu, Project prj)
        {
            string prjname = prj.Name;
            var ProjectMenu = new ConsoleMenu();
            ProjectMenu.Configure(config =>
            {
                config.WriteHeaderAction = () =>
            {
                Console.WriteLine("Project: " + prj.Name + "  Task number: " + prj.TaskList.Count);
            };
            });
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



            ProjectMenu.Add("Create new Task", () =>
            {
                CreateTaskMenu(prj);
                RefreshMainMenu(ref MainMenu);
                RefreshProjectMenu(MainMenu, prj).Show();
            });
            ProjectMenu.Add("Rename this project", () =>
            {
                RenameProjectMenu(prj);
                RefreshMainMenu(ref MainMenu);
            });
            ProjectMenu.Add("Delete this project", () =>
            {
                DeleteProject(prjname);
                saveProject.SaveProjectList(ProjectList);
                RefreshMainMenu(ref MainMenu);
                MainMenu.Show();
            });
            ProjectMenu.Add("Back", () =>
            {
                RefreshMainMenu(ref MainMenu);
                MainMenu.Show();
            });
            MainMenu.Add("Project: " + prj.Name, ProjectMenu.Show);
            return ProjectMenu;
        }
        static void CreateTaskMenu(Project prj)
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
                    prj.AddTask(task);
                    saveProject.SaveProjectList(ProjectList);
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error! : " + e.Message + " Plz try again...");
                }
        }
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
            umm.Add("Create new user", () =>
            {
                CreateUserMenu();
                umm.CloseMenu();
                umm = UserManageMenu();
                umm.Show();
            });
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
            umm.Add("Back", umm.CloseMenu);
            return umm;
        }

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
            chum.Add("Back", () =>
            {
                up.Show();
            });
            return chum;
        }
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
