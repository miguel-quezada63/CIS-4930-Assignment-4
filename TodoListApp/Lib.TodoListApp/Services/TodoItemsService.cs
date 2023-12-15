using Lib.TodoListApp.Models;
using Lib.TodoListApp.Persistence;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Lib.TodoListApp.Services
{
    public class TodoItemsService
    {
        private static string API_URL = "http://localhost:16048/api/";

        public static async Task<List<TodoItem>> GetAllTodoItems()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var res = await client.GetAsync(API_URL + "TodoItems/all").ConfigureAwait(false);
                    var json = await res.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<TodoItem>>(json, new TodoItemJSONConverter());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return new List<TodoItem>();
        }

        public static async Task<List<TodoItem>> CreateTodoItem(TodoItem newItem)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var serialized =
                        new StringContent(JsonConvert.SerializeObject(newItem, new TodoItemJSONConverter()),
                            Encoding.UTF8, "application/json");
                    var res = await client.PostAsync(API_URL + $"TodoItems/create", serialized).ConfigureAwait(false);
                    var json = await res.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<TodoItem>>(json, new TodoItemJSONConverter());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return new List<TodoItem>();
        }

        public static async Task<List<TodoItem>> DeleteTodoItem(string id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var res = await client.DeleteAsync(API_URL + $"TodoItems/delete/{id}").ConfigureAwait(false);
                    var json = await res.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<TodoItem>>(json, new TodoItemJSONConverter());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return new List<TodoItem>();
        }

        public static async Task<List<TodoItem>> EditTodoItem(string id, TodoItem newItem)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var serialized =
                        new StringContent(JsonConvert.SerializeObject(newItem, new TodoItemJSONConverter()),
                            Encoding.UTF8, "application/json");
                    var res = await client.PutAsync(API_URL + $"TodoItems/edit/{id}", serialized).ConfigureAwait(false);
                    var json = await res.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<TodoItem>>(json, new TodoItemJSONConverter());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return new List<TodoItem>();
        }

        public static async Task<List<TodoItem>> SaveTodoItems(List<TodoItem> newItems)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var serialized = new StringContent(JsonConvert.SerializeObject(newItems, new TodoItemJSONConverter()), Encoding.UTF8, "application/json");
                    var res = await client.PostAsync(API_URL + "TodoItems/save", serialized).ConfigureAwait(false);
                    var json = await res.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<TodoItem>>(json, new TodoItemJSONConverter());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return new List<TodoItem>();
        }

        public static async Task<List<TodoItem>> SearchTodoItems(string query)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var res = await client.GetAsync(API_URL + $"TodoItems/search?query={query}").ConfigureAwait(false);
                    var json = await res.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<TodoItem>>(json, new TodoItemJSONConverter());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return new List<TodoItem>();
        }
    }
}
