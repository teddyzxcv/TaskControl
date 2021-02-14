using System;
using TaskControl;
using System.Collections.Generic;
using System.Linq;

namespace TaskControlApp
{
    class Program
    {
        static List<User> UsersList = new List<User>();
        static List<Project> ProjectList = new List<Project>();

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


        static void Main(string[] args)
        {

        }

    }
}
