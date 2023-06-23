using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Tema_2.Model;

namespace Tema_2.ViewModel
{
    public class StatisticsViewModel : INotifyPropertyChanged
    {
        private int _tasksDueToday;
        private int _tasksDueTomorrow;
        private int _tasksDone;
        private int _tasksToDo;

        public int TasksDueToday
        {
            get => _tasksDueToday;
            set
            {
                _tasksDueToday = value;
                OnPropertyChanged();
            }
        }

        public int TasksDueTomorrow
        {
            get => _tasksDueTomorrow;
            set
            {
                _tasksDueTomorrow = value;
                OnPropertyChanged();
            }
        }

        public int TasksDone
        {
            get => _tasksDone;
            set
            {
                _tasksDone = value;
                OnPropertyChanged();
            }
        }

        public int TasksToDo
        {
            get => _tasksToDo;
            set
            {
                _tasksToDo = value;
                OnPropertyChanged();
            }
        }

        public StatisticsViewModel(ObservableCollection<ToDoList> todoLists)
        {
            CalculateStatistics(todoLists);
        }

        public void CalculateStatistics(ObservableCollection<ToDoList> todoLists)
        {
            var today = DateTime.Today;
            var tomorrow = today.AddDays(1);

            TasksDueToday = 0;
            TasksDueTomorrow = 0;
            TasksDone = 0;
            TasksToDo = 0;

            foreach (var todo in todoLists)
            {
                TasksDueToday += todo.Tasks.Count(task => task.DueDate >= today && task.DueDate < tomorrow && !task.Status);
                TasksDueTomorrow += todo.Tasks.Count(task => task.DueDate >= tomorrow && task.DueDate < tomorrow.AddDays(1) && !task.Status);
                TasksDone += todo.Tasks.Count(task => task.Status);
                TasksToDo += todo.Tasks.Count(task => !task.Status);

                CalculateSubListStatistics(todo.Categories);
            }

            OnPropertyChanged(nameof(TasksDueToday));
            OnPropertyChanged(nameof(TasksDueTomorrow));
            OnPropertyChanged(nameof(TasksDone));
            OnPropertyChanged(nameof(TasksToDo));
        }

        private void CalculateSubListStatistics(ObservableCollection<ToDoList> subLists)
        {
            var today = DateTime.Today;
            var tomorrow = today.AddDays(1);

            foreach (var subList in subLists)
            {
                TasksDueToday += subList.Tasks.Count(task => task.DueDate >= today && task.DueDate < tomorrow && !task.Status);
                TasksDueTomorrow += subList.Tasks.Count(task => task.DueDate >= tomorrow && task.DueDate < tomorrow.AddDays(1) && !task.Status);
                TasksDone += subList.Tasks.Count(task => task.Status);
                TasksToDo += subList.Tasks.Count(task => !task.Status);

                CalculateSubListStatistics(subList.Categories);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}