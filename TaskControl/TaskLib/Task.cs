using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace TaskControl
{
    public class Task : IAssignable
    {
        public string Name { get; set; }
        public DateTime CreateTime { get; set; }

        public List<Task> SubTaskList { get; set; }
        public List<User> UserList { get; set; }

        Status CurrentStatus = Status.Default;

        public Task(string name)
        {
            Name = name;
            CreateTime = DateTime.Now;
            SubTaskList = new List<Task>();
            UserList = new List<User>();
        }
        public Task()
        {

        }
        public Status GetStatus()
        {
            return CurrentStatus;
        }
        public void SetStatus(Status status)
        {
            CurrentStatus = status;
        }
        public enum Status
        {
            Default,
            AtWork,
            EndWork
        }
        public void AddTask(Task task)
        {
            if (task.GetType() == typeof(BugTask))
                throw new ArgumentException("Bug only can describe the project.");
            if (this.GetType() == typeof(StoryTask))
                throw new ArgumentException("Can't add task to story task.");
            if (this.GetType() == typeof(TaskTask))
                throw new ArgumentException("Can't add task to task task.");
            if (this.GetType() == typeof(EpicTask) && (task.GetType() != typeof(StoryTask) && task.GetType() != typeof(TaskTask)))
                throw new ArgumentException("Can't add any task beside story and task to epic task.");
            if (SubTaskList.Select(e => e.Name).Contains(task.Name))
                throw new ArgumentException("No! No repeat!");
            SubTaskList.Add(task);
        }
        public void DeleteTask(Task task)
        {
            SubTaskList.Remove(task);
        }
        public void AddUser(User user)
        {
            if (this.GetType() == typeof(TaskTask) || this.GetType() == typeof(BugTask))
            {
                if (this.UserList.Count == 0)
                    UserList.Add(user);
                else
                    throw new ArgumentException("Can't be more than one user for Bug and Task.");
            }
            else
            {
                UserList.Add(user);
            }
        }
        public void DeleteUser(User user)
        {
            UserList.Remove(user);
        }
        public override string ToString()
        {
            return $"Task:{this.Name}, create time: {this.CreateTime.ToString()}, users:{String.Join(',', this.UserList)}, status: {this.CurrentStatus}";
        }
        public Dictionary<Task.Status, List<Task>> GroupByStatus()
        {
            return this.SubTaskList.GroupBy(e => e.GetStatus()).ToDictionary(group => group.Key, group => group.ToList());
        }


    }
}
