using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace Lib.TodoListApp.Models
{
    [DataContract]
    public class TodoTask : TodoItem
    {
        public override string ItemSpecificProps => $"Deadline: {Deadline.ToShortDateString()}\nTask completed: {(IsCompleted ? "Yes" : "No")}";
        [DataMember]
        public override string TodoType => "Task";
        [DataMember]
        public DateTime Deadline { get; set; }
        [DataMember]
        public bool IsCompleted { get; set; }
        public TodoTask() : base() { }
        public TodoTask(string id) : base(id) { }
        [JsonConstructor]
        public TodoTask(string id, string name, string description, string priority, DateTime deadline, bool isCompleted = false) : base(id, name, description, priority)
        {
            Deadline = deadline;
            IsCompleted = isCompleted;
        }
        public TodoTask(string name, string description, string priority, DateTime deadline, bool isCompleted = false) : base(name, description, priority)
        {
            Deadline = deadline;
            IsCompleted = isCompleted;
        }
        public void ChangeAllProps(TodoTask t)
        {
            base.ChangeAllProps(t);
            Deadline = t.Deadline;
            IsCompleted = t.IsCompleted;
        }
    }
}