using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace Lib.TodoListApp.Models
{
    [DataContract]
    public class TodoItem
    {
        public class PriorityType
        {
            public const string LOW = "Low";
            public const string MEDIUM = "Medium";
            public const string HIGH = "High";
        }
        public string PriorityColor
        {
            get
            {
                switch (Priority)
                {
                    case PriorityType.HIGH:
                        return "#dc3545";
                    case PriorityType.MEDIUM:
                        return "#ffc107";
                    default:
                        return "#007bff";
                }
            }
        }
        public virtual string ItemSpecificProps { get; set; }
        [DataMember]
        public virtual string TodoType => "Item";
        public TodoItem() => Id = Guid.NewGuid().ToString();
        public TodoItem(string id) => Id = id;
        [JsonConstructor]
        public TodoItem(string id, string name, string description, string priority = PriorityType.LOW)
        {
            Id = id;
            Name = name;
            Description = description;
            Priority = priority;
        }
        public TodoItem(string name, string description, string priority = PriorityType.LOW)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            Description = description;
            Priority = priority;
        }
        [DataMember]
        public string Id { get; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string Priority { get; set; }

        public void ChangeAllProps(TodoItem i)
        {
            Name = i.Name;
            Description = i.Description;
            Priority = i.Priority;
        }
        public virtual bool Contains(string query) => Name.ToLower().Contains(query.ToLower()) || Description.ToLower().Contains(query.ToLower());
    }
}