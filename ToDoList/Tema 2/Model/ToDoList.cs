using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Tema_2.Model
{
    public class ToDoList : IBaseEntity, INotifyPropertyChanged
    {
        private string _imagePath;
        private ObservableCollection<ToDoList> _categories;
        private ObservableCollection<Task> _tasks;
        private string _name;

        public string ImagePath
        {
            get => _imagePath;
            set
            {
                if (value == _imagePath) return;
                _imagePath = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ToDoList> Categories
        {
            get => _categories;
            set
            {
                if (Equals(value, _categories)) return;
                _categories = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Task> Tasks
        {
            get => _tasks;
            set
            {
                if (Equals(value, _tasks)) return;
                _tasks = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                if (value == _name) return;
                _name = value;
                OnPropertyChanged();
            }
        }

        public void Add(ToDoList category)
        {
            if (Categories == null)
            {
                Categories = new ObservableCollection<ToDoList>();
            }
            Categories.Add(category);
        }
        public void Add(Task task)
        {
            if (Tasks == null)
            {
                Tasks = new ObservableCollection<Task>();
            }
            Tasks.Add(task);
        }
        public void Remove(ToDoList category)
        {
            Categories.Remove(category);
        }
        public void Remove(Task task)
        {
            Tasks.Remove(task);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}