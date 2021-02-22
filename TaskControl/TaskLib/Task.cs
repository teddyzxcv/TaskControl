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
        /// <summary>
        /// Initialize.
        /// </summary>
        /// <param name="name"></param>
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
        /// <summary>
        /// Get status of task.
        /// </summary>
        /// <returns></returns>
        public Status GetStatus()
        {
            return CurrentStatus;
        }
        /// <summary>
        /// Set status.
        /// </summary>
        /// <param name="status"></param>
        public void SetStatus(Status status)
        {
            CurrentStatus = status;
        }
        /// <summary>
        /// Status enum.
        /// </summary>
        public enum Status
        {
            Default,
            AtWork,
            EndWork
        }
        /// <summary>
        /// Add task under certain condition.
        /// </summary>
        /// <param name="task"></param>
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
        /// <summary>
        /// Delete sub task.
        /// </summary>
        /// <param name="task"></param>
        public void DeleteTask(Task task)
        {
            SubTaskList.Remove(task);
        }
        /// <summary>
        /// Add user unde ceratin conditions.
        /// </summary>
        /// <param name="user"></param>
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
        /// <summary>
        /// Delete user.
        /// </summary>
        /// <param name="user"></param>
        public void DeleteUser(User user)
        {
            UserList.Remove(user);
        }
        public override string ToString()
        {
            return $"Task:{this.Name}, create time: {this.CreateTime.ToString()}, users:{String.Join(',', this.UserList)}, status: {this.CurrentStatus}";
        }
        /// <summary>
        /// Group by status.
        /// </summary>
        /// <returns></returns>
        public Dictionary<Task.Status, List<Task>> GroupByStatus()
        {
            return this.SubTaskList.GroupBy(e => e.GetStatus()).ToDictionary(group => group.Key, group => group.ToList());
        }


    }
}
