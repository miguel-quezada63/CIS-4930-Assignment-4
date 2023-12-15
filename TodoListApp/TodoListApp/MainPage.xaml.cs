using System;
using TodoListApp.Dialogs;
using TodoListApp.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace TodoListApp
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            DataContext = new MainViewModel();
        }
        private async void AddNew_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new TodoItemDialog((DataContext as MainViewModel)?.TodoItems);
            await dialog.ShowAsync();
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as MainViewModel)?.Remove();
        }
        private async void Edit_Click(object sender, RoutedEventArgs e)
        {
            await (DataContext as MainViewModel)?.EditTicket();
        }
        private void Sort_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as MainViewModel)?.SortList();
        }
        private void Search(object sender, TextChangedEventArgs e)
        {
            (DataContext as MainViewModel)?.Search();
            // (DataContext as MainViewModel)?.RefreshList()
        }
    }
}
