using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using Tema_2.Model;
using Tema_2.ViewModel;

namespace Tema_2.View
{
    public partial class AddToDoListWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ToDoList _toDoList;

        private string _name;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
            }
        }

        private string _imagePath;

        public string ImagePath
        {
            get => _imagePath;
            set
            {
                _imagePath = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ImagePath)));
            }
        }

        public AddToDoListWindow(ToDoList toDoList = null)
        {
            InitializeComponent();
            DataContext = this;
			_toDoList = toDoList;
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";
            var result = dialog.ShowDialog();
            if (result == true)
            {
                ImagePath = dialog.FileName;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Name = NameTextBox.Text;
            var categ = Application.Current.MainWindow.DataContext as CategoryViewModel;

            if (!string.IsNullOrEmpty(Name) && categ.ToDoLists.All(tdl => tdl.Name != Name))
            {
                var newTDL = new ToDoList
                {
                    Name = Name, ImagePath = ImagePath, Categories = new ObservableCollection<ToDoList>(),
                    Tasks = new ObservableCollection<Task>()
                };
                if (_toDoList != null)
                {
                    if (_toDoList.Categories == null)
                    {
                        _toDoList.Categories = new ObservableCollection<ToDoList>();
                    }

                    _toDoList.Categories.Add(newTDL);
                }
                else
                {
                    (Application.Current.MainWindow.DataContext as CategoryViewModel)?.ToDoLists.Add(newTDL);
                }

                Close();
            }
            else
            {
                MessageBox.Show("Please enter a name for the TDL.");
            }
        }
    }
}