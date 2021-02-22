using System;
using System.Collections.Generic;
using System.Linq;

namespace TaskControl
{
    public class Project
    {
        // Name of project.
        public string Name { get; set; }
        // Save task list.
        public List<Task> TaskList { get; set; }
        // Max task number.
        public int MaxTaskNumber { get; set; }
        /// <summary>
        /// Initial setting.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="mtn"></param>
        public Project(string name, int mtn)
        {
            MaxTaskNumber = mtn;
            Name = name;
            TaskList = new List<Task>();
        }
        /// <summary>
        /// Delete task.
        /// </summary>
        /// <param name="task"></param>
        public void DeleteTask(Task task)
        {
            this.TaskList.Remove(task);
        }
        /// <summary>
        /// Add task in certain coditions.
        /// </summary>
        /// <param name="task"></param>
        public void AddTask(Task task)
        {
            if (TaskList.Count >= MaxTaskNumber)
                throw new ArgumentException("Max Task number have been reached");
            if (TaskList.Select(e => e.Name).Contains(task.Name))
                throw new ArgumentException("No! No repeat!");
            task.CreateTime = DateTime.Now;
            this.TaskList.Add(task);
        }
        /// <summary>
        /// String.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{this.Name} - task number : {this.MaxTaskNumber}";
        }
        /// <summary>
        /// Group by status.
        /// </summary>
        /// <returns></returns>
        public Dictionary<Task.Status, List<Task>> GroupByStatus()
        {
            return this.TaskList.GroupBy(e => e.GetStatus()).ToDictionary(group => group.Key, group => group.ToList());
        }

    }
}
