using System;
using System.Collections.Generic;

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
            this.TaskList.Add(task);
        }
        public override string ToString()
        {
            return $"{this.Name} - task number : {this.MaxTaskNumber}";
        }

    }
}
