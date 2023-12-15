using Lib.TodoListApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web.Http;

namespace Api.TodoListApp.Controllers
{
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("[controller]")]
    public class TodoItemsController : ControllerBase
    {
        private static readonly string DataPath = $"{Directory.GetCurrentDirectory()}\\data.json";
        private static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings
        { TypeNameHandling = TypeNameHandling.All };
        private static List<TodoItem> Items = new List<TodoItem>();
        public async void InitializeController() => await LoadFromJsonFile();
        public TodoItemsController() => InitializeController();

        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("all")]
        public IList<TodoItem> GetAll() => Items;

        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("completed")]
        public List<TodoItem> GetCompleted() => Items.FindAll(item => item is TodoTask { IsCompleted: true }); // this may be broke

        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("outstanding")]
        public List<TodoItem> GetOutstanding() => Items.FindAll(item => item is TodoTask { IsCompleted: false }); // this may be broke

        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("appointments")]
        public List<TodoItem> GetAppointments() => Items.FindAll(item => item is TodoAppointment a);

        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("tasks")]
        public List<TodoItem> GetTasks() => Items.FindAll(item => item is TodoTask t);

        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("search")]
        public List<TodoItem> Search([FromUri] string query) => Items.FindAll(item => item.Contains(query));

        [Microsoft.AspNetCore.Mvc.HttpDelete]
        [Microsoft.AspNetCore.Mvc.Route("delete/{id}")]
        public List<TodoItem> DeleteTodo([FromUri] string id)
        {
            var itemToRemove = Items.Find(item => item.Id == id);
            Items.Remove(itemToRemove);
            SaveToJsonFile();
            return Items;
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.Route("create")]
        public List<TodoItem> CreateTask([Microsoft.AspNetCore.Mvc.FromBody] TodoItem t)
        {
            Items.Add(t);
            SaveToJsonFile();
            return Items;
        }

        [Microsoft.AspNetCore.Mvc.HttpPut]
        [Microsoft.AspNetCore.Mvc.Route("update/all")]
        public List<TodoItem> UpdateAllTodoItems([Microsoft.AspNetCore.Mvc.FromBody] List<TodoItem> newItems)
        {
            Items.Clear();
            foreach (var item in newItems) Items.Add(item);
            SaveToJsonFile();
            return Items;
        }

        [Microsoft.AspNetCore.Mvc.HttpPut]
        [Microsoft.AspNetCore.Mvc.Route("edit/{id}")]
        public List<TodoItem> EditTodoItem([FromUri] string id, [System.Web.Http.FromBody] TodoItem newItem)
        {
            var curItem = Items.Find(item => item.Id == id);
            var curItemIndex = Items.FindIndex(item => item.Id == id);
            switch (newItem)
            {
                case TodoTask newTask:
                    switch (curItem)
                    {
                        case TodoTask curTask:
                            curTask.ChangeAllProps(newTask);
                            break;
                        case TodoAppointment:
                            {
                                var tempTask = new TodoTask();
                                tempTask.ChangeAllProps(newTask);
                                Items[curItemIndex] = tempTask;
                                break;
                            }
                    }
                    break;
                case TodoAppointment newApp:
                    switch (curItem)
                    {
                        case TodoTask:
                            {
                                var tempApp = new TodoAppointment();
                                tempApp.ChangeAllProps(newApp);
                                Items[curItemIndex] = tempApp;
                                break;
                            }
                        case TodoAppointment curApp:
                            curApp.ChangeAllProps(newApp);
                            break;
                    }
                    break;
            }
            SaveToJsonFile();
            return Items;
        }

        private async void SaveToJsonFile() => await System.IO.File.WriteAllTextAsync(DataPath, JsonConvert.SerializeObject(Items, SerializerSettings));

        private async Task LoadFromJsonFile()
        {
            if (System.IO.File.Exists(DataPath))
                Items = JsonConvert.DeserializeObject<List<TodoItem>>(await System.IO.File.ReadAllTextAsync(DataPath), SerializerSettings);
        }
    }
}
