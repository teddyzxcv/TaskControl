using System;
using System.Collections.Generic;
using System.Linq;

namespace TaskControl
{
    public class Project
    {
        public string Name { get; set; }
        public List<Task> TaskList { get; set; }
        public int MaxTaskNumber { get; set; }
        public Project(string name, int mtn)
        {
            MaxTaskNumber = mtn;
            Name = name;
            TaskList = new List<Task>();
        }

        public void DeleteTask(Task task)
        {
            this.TaskList.Remove(task);
        }
        public void AddTask(Task task)
        {
            if (TaskList.Count >= MaxTaskNumber)
                throw new ArgumentException("Max Task number have been reached");
            if (TaskList.Select(e => e.Name).Contains(task.Name))
                throw new ArgumentException("No! No repeat!");
            this.TaskList.Add(task);
        }
        public override string ToString()
        {
            return $"{this.Name} - task number : {this.MaxTaskNumber}";
        }
        public Dictionary<Task.Status, List<Task>> GroupByStatus()
        {
            return this.TaskList.GroupBy(e => e.GetStatus()).ToDictionary(group => group.Key, group => group.ToList());
        }

    }
}
