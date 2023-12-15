using Lib.TodoListApp.Models;
using Lib.TodoListApp.Services;
using System.Collections.ObjectModel;
using TodoListApp.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
namespace TodoListApp.Dialogs
{
    public sealed partial class TodoItemDialog : ContentDialog
    {
        private readonly ObservableCollection<TodoItem> _todoItems;
        public TodoItemDialog(ObservableCollection<TodoItem> todoItems)
        {
            this.InitializeComponent();
            DataContext = new DialogViewModel();
            this._todoItems = todoItems;
        }
        public TodoItemDialog(ObservableCollection<TodoItem> todoItems, TodoItem item)
        {
            InitializeComponent();
            DataContext = new DialogViewModel(item);
            this._todoItems = todoItems;
        }
        private async void Dialog_OkayBtnClick(object sender, RoutedEventArgs args)
        {
            var dc = DataContext as DialogViewModel;
            var itemToEdit = dc?.NewItem;
            var i = _todoItems.IndexOf(itemToEdit);
            if (i >= 0)
            {
                var items =
                    new ObservableCollection<TodoItem>(
                        await TodoItemsService.EditTodoItem(_todoItems[i].Id, itemToEdit));
                _todoItems.Clear();
                foreach (var item in items) _todoItems.Add(item);
            }
            else
            {
                var items = new ObservableCollection<TodoItem>(await TodoItemsService.CreateTodoItem(itemToEdit));
                _todoItems.Clear();
                foreach (var item in items) _todoItems.Add(item);
            }
            this.Hide();
        }
        private void Dialog_CancelBtnClick(object sender, RoutedEventArgs args) => this.Hide();
    }
}
