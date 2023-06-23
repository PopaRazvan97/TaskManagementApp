using System.ComponentModel;
using System.Runtime.CompilerServices;
using Tema_2.Model;

namespace Tema_2.ViewModel
{
    public class TaskViewModel : INotifyPropertyChanged
    {
        public ToDoList Parent
        {
            get => _parent;
            set
            {
                if (Equals(value, _parent)) return;
                _parent = value;
                OnPropertyChanged();
            }
        }

        private Task _selectedTask;
        private ToDoList _parent;

        public Task SelectedTask
        {
            get => _selectedTask;
            set
            {
                _selectedTask = value;
                OnPropertyChanged();
                OnPropertyChanged("SelectedTask");
            }
        }

        public void MoveTaskUp()
        {
            int index = Parent.Tasks.IndexOf(SelectedTask);
            if (index > 0)
            {
                Parent.Tasks.Move(index, index - 1);
            }
        }

        public void MoveTaskDown()
        {
            int index = Parent.Tasks.IndexOf(SelectedTask);
            if (index < Parent.Tasks.Count - 1)
            {
                Parent.Tasks.Move(index, index + 1);
            }
        }
        
        public void SetDone()
        {
            SelectedTask.Status = true;
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}