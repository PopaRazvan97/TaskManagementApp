using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Tema_2.Model;

namespace Tema_2.ViewModel
{
    public class CategoryViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<ToDoList> _toDoLists;
        public ObservableCollection<ToDoList> ToDoLists
        {
            get => _toDoLists;
            set
            {
                _toDoLists = value;
                OnPropertyChanged(nameof(ToDoLists));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private ToDoList _selectedItem;
        public ToDoList SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        public void RemoveToDoListRecursive(ToDoList item, ObservableCollection<ToDoList> categories)
        {
            if (categories.Contains(item))
            {
                categories.Remove(item);
                return;
            }

            foreach (var category in categories)
            {
                RemoveToDoListRecursive(item, category.Categories);
            }
        }
        
        public void MoveToDoListUp(ToDoList item, ObservableCollection<ToDoList> categories)
        {
            for (int i = 0; i < categories.Count; i++)
            {
                if (categories[i] == item && i > 0)
                {
                    categories.Move(i, i - 1);
                    break;
                }

                MoveToDoListUp(item, categories[i].Categories);
            }
        }

        public void MoveToDoListDown(ToDoList item, ObservableCollection<ToDoList> categories)
        {
            for (int i = 0; i < categories.Count; i++)
            {
                if (categories[i] == item && i < categories.Count - 1)
                {
                    categories.Move(i, i + 1);
                    break;
                }

                MoveToDoListDown(item, categories[i].Categories);
            }
        }
        
        public void EditToDoList(string name, string imagePath)
        {
            SelectedItem.Name = name;
            SelectedItem.ImagePath = imagePath;
        }
        
        public CategoryViewModel()
        {
            ToDoLists = new ObservableCollection<ToDoList>();
        }
        
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        public event EventHandler AddRootToDoListRequested;
        public void OnAddRootToDoListRequested()
        {
            AddRootToDoListRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}