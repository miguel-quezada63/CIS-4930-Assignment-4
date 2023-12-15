using Lib.TodoListApp.Models;
using Newtonsoft.Json.Linq;
using System;

namespace Lib.TodoListApp.Persistence
{
    public class TodoItemJSONConverter : JsonCreationConverter<TodoItem>
    {
        protected override TodoItem Create(Type objectType, JObject jObject)
        {
            if (jObject == null) throw new ArgumentNullException("jObject");
            if ((jObject["Attendees"] != null || jObject["attendees"] != null) && jObject.Type != JTokenType.Null)
            {
                return jObject["Id"] != null
                    ? new TodoAppointment(jObject["Id"].ToString())
                    : new TodoAppointment(jObject["id"].ToString());
            }
            if ((jObject["IsCompleted"] != null || jObject["isCompleted"] != null) && jObject.Type != JTokenType.Null)
            {
                return jObject["Id"] != null
                    ? new TodoTask(jObject["Id"].ToString())
                    : new TodoTask(jObject["id"].ToString());
            }
            return new TodoItem(jObject["id"].ToString());
        }
    }
}
