using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Lib.TodoListApp.Models
{
    [DataContract]
    public class TodoAppointment : TodoItem
    {
        public override string ItemSpecificProps => $"Start: {Start.ToShortDateString()}\nStop: {Stop.ToShortDateString()}\nAttendees: {String.Join(", ", Attendees)}";
        [DataMember]
        public override string TodoType => "Appointment";
        [DataMember]
        public DateTime Start { get; set; }
        [DataMember]
        public DateTime Stop { get; set; }
        [DataMember]
        public List<string> Attendees { get; set; }
        public TodoAppointment() : base() { }
        public TodoAppointment(string id) : base(id) { }
        [JsonConstructor]
        public TodoAppointment(string id, string name, string description, string priority, DateTime start, DateTime stop, List<string> attendees) : base(id, name, description, priority)
        {
            Name = name;
            Description = description;
            Start = start;
            Stop = stop;
            Attendees = attendees;
        }
        public TodoAppointment(string name, string description, string priority, DateTime start, DateTime stop, List<string> attendees) : base(name, description, priority)
        {
            Name = name;
            Description = description;
            Start = start;
            Stop = stop;
            Attendees = attendees;
        }
        public void ChangeAllProps(TodoAppointment t)
        {
            base.ChangeAllProps(t);
            Start = t.Start;
            Stop = t.Stop;
            Attendees = t.Attendees;
        }
        public override bool Contains(string query) => base.Contains(query) || Attendees.Contains(query);
    }
}