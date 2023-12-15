using Lib.TodoListApp.Models;
using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml;

namespace TodoListApp.ViewModels
{
    public class DialogViewModel : INotifyPropertyChanged
    {
        private bool _boundIsCompleted;
        private DateTimeOffset _boundStart;
        private DateTimeOffset _boundStop;
        private DateTimeOffset _boundDeadline;
        private string _attendeesString;
        private readonly bool _isAppointment;
        private readonly bool _showCheckBox;
        public bool BoundIsCompleted
        {
            get
            {
                if (NewItem is TodoTask newItem)
                    return newItem.IsCompleted;
                return _boundIsCompleted;
            }
            set
            {
                _boundIsCompleted = value;
                if (NewItem is TodoTask newItem)
                    newItem.IsCompleted = _boundIsCompleted;
            }
        }
        public DateTimeOffset BoundStart
        {
            get
            {
                if (NewItem is TodoAppointment newItem)
                    return newItem.Start;
                return _boundStart;
            }
            set
            {
                _boundStart = value == DateTimeOffset.MinValue ? _boundStart : value;
                if (NewItem is TodoAppointment newItem)
                    newItem.Start = _boundStart.DateTime;
            }
        }
        public DateTimeOffset BoundStop
        {
            get
            {
                if (NewItem is TodoAppointment newItem)
                    return newItem.Stop;
                return _boundStop;
            }
            set
            {
                _boundStop = value == DateTimeOffset.MinValue ? _boundStop : value;
                if (NewItem is TodoAppointment newItem)
                    newItem.Stop = _boundStop.DateTime;
            }
        }
        public DateTimeOffset BoundDeadline
        {
            get
            {
                if (NewItem is TodoTask newItem)
                    return newItem.Deadline;
                return _boundDeadline;
            }
            set
            {
                _boundDeadline = value == DateTimeOffset.MinValue ? _boundDeadline : value;
                if (NewItem is TodoTask newItem)
                    newItem.Deadline = _boundDeadline.DateTime;
            }
        }
        public string AttendeesString
        {
            get
            {
                if (NewItem is TodoAppointment newItem)
                    return String.Join(",", newItem.Attendees);
                return _attendeesString;
            }
            set
            {
                if (NewItem is TodoAppointment newItem)
                    newItem.Attendees = value.Split(",").ToList();
                _attendeesString = value;
            }
        }
        public Visibility ShowTask => NewItem is TodoTask ? Visibility.Visible : Visibility.Collapsed;
        public Visibility ShowAppointment => NewItem is TodoAppointment ? Visibility.Visible : Visibility.Collapsed;
        public Visibility ShowCheckBox => _showCheckBox ? Visibility.Visible : Visibility.Collapsed;
        public TodoItem NewItem { get; set; }
        public bool IsAppointment
        {
            get => _isAppointment;
            set
            {
                if (value)
                    NewItem = new TodoAppointment() { Name = NewItem.Name, Description = NewItem.Description, Priority = NewItem.Priority, Start = BoundStart.DateTime, Stop = BoundStop.DateTime, Attendees = AttendeesString.Split(",").ToList() };
                else
                    NewItem = new TodoTask() { Name = NewItem.Name, Description = NewItem.Description, Priority = NewItem.Priority, Deadline = BoundDeadline.DateTime, IsCompleted = BoundIsCompleted };
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(NewItem));
                NotifyPropertyChanged(nameof(ShowAppointment));
                NotifyPropertyChanged(nameof(ShowTask));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public DialogViewModel()
        {
            NewItem = new TodoTask();
            _showCheckBox = true;
            BoundDeadline = DateTimeOffset.Now;
            BoundStart = DateTimeOffset.Now;
            BoundStop = DateTimeOffset.Now;
            AttendeesString = "";
        }

        public DialogViewModel(TodoItem item)
        {
            NewItem = item;
            _showCheckBox = false;
            _isAppointment = !(NewItem is TodoTask);
        }
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
