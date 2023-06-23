using System.Globalization;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Tema_2.Model;
using Tema_2.ViewModel;
using Tema_2.Utils;
using Tema_2.View;

namespace Tema_2
{
	public class BooleanToStringConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			bool status = (bool)value;
			return status ? "Finished" : "Not Finished";
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
	
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
    {
        private readonly CategoryViewModel _categoryViewModel;
        private readonly TaskViewModel _taskViewModel;
        private readonly StatisticsViewModel _statisticsViewModel;

        public MainWindow()
        {
            _categoryViewModel = new CategoryViewModel();
            _taskViewModel = new TaskViewModel();
            _statisticsViewModel = new StatisticsViewModel(_categoryViewModel.ToDoLists);
            InitializeComponent();
            DataContext = _categoryViewModel;
            TaskPanel.DataContext = _taskViewModel;
            StatisticsPanel.DataContext = _statisticsViewModel;
        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var selectedToDoList = e.NewValue as ToDoList;
            _categoryViewModel.SelectedItem = selectedToDoList;
            AddSubTDL.IsEnabled = true;
            EditTDL.IsEnabled = true;
            DeleteTDL.IsEnabled = true;
            _taskViewModel.Parent = selectedToDoList;
            if (selectedToDoList?.Tasks != null && selectedToDoList.Tasks.Any())
            {
                _taskViewModel.SelectedTask = selectedToDoList.Tasks.First();
            }
            else
            {
                _taskViewModel.SelectedTask = null;
            }
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listView = sender as ListView;
            var selectedTask = (listView.SelectedItem as Model.Task);
            _taskViewModel.SelectedTask = selectedTask;
            MoveUpButton.IsEnabled = true;
            MoveDownButton.IsEnabled = true;
            SetDoneButton.IsEnabled = true;
        }

        private void ArchiveFile(object sender, RoutedEventArgs e)
        {
			string fileName = string.Format("ToDoList_{0:yyyy-MM-dd_HH-mm-ss}.xml", DateTime.Now);
			FileUtils.SerializeToFile(fileName, _categoryViewModel.ToDoLists);
        }

        private void OpenFile(object sender, RoutedEventArgs e)
        {
	        var openFileDialog = new Microsoft.Win32.OpenFileDialog();
	        openFileDialog.Filter = "XML Files (*.xml)|*.xml";

	        // Show the dialog and get the result
	        var result = openFileDialog.ShowDialog();

	        if (result == true)
	        {
		        var fileName = openFileDialog.FileName;

		        _categoryViewModel.ToDoLists = FileUtils.DeserializeFromFile<ObservableCollection<ToDoList>>(fileName);
		        _statisticsViewModel.CalculateStatistics(_categoryViewModel.ToDoLists);
	        }
        }

        private void NewList(object sender, RoutedEventArgs e)
        {
	        try
	        {
		        _categoryViewModel.ToDoLists.Clear();
	        }
	        catch (Exception exception)
	        {
		        _categoryViewModel.ToDoLists = new ObservableCollection<ToDoList>();
	        }
	        _statisticsViewModel.CalculateStatistics(_categoryViewModel.ToDoLists);
	        _taskViewModel.SelectedTask = null;
        }

        private void AddTDLRoot(object sender, RoutedEventArgs e)
        {
	        var tdlWindow = new AddToDoListWindow();
	        tdlWindow.ShowDialog();
        }

        private void AddSubTDLOnClick(object sender, RoutedEventArgs e)
        {
	        var tdlWinow = new AddToDoListWindow(_categoryViewModel.SelectedItem);
	        tdlWinow.ShowDialog();
	        _statisticsViewModel.CalculateStatistics(_categoryViewModel.ToDoLists);
        }

        private void EditTDLOnClick(object sender, RoutedEventArgs e)
        {
	        var editWindow = new EditToDoListWindow(_categoryViewModel);
	        editWindow.ShowDialog();
	        InvalidateVisual();
        }

        private void DeleteTDLOnClick(object sender, RoutedEventArgs e)
        {
	        _categoryViewModel.RemoveToDoListRecursive(_categoryViewModel.SelectedItem, _categoryViewModel.ToDoLists);
        }

        private void MoveUpOnClick(object sender, RoutedEventArgs e)
        {
	        _categoryViewModel.MoveToDoListUp(_categoryViewModel.SelectedItem, _categoryViewModel.ToDoLists);
        }

        private void MoveDownOnClick(object sender, RoutedEventArgs e)
        {
	        _categoryViewModel.MoveToDoListDown(_categoryViewModel.SelectedItem, _categoryViewModel.ToDoLists);
        }

        private void SetDoneOnClick(object sender, RoutedEventArgs e)
        {
	        if (_taskViewModel.SelectedTask != null)
	        {
		        _taskViewModel.SetDone();
		        _statisticsViewModel.CalculateStatistics(_categoryViewModel.ToDoLists);
	        }
	        else
	        {
		        MessageBox.Show("Please select a task before pressing the button");
	        }
        }

        private void MoveUpOnClickForTask(object sender, RoutedEventArgs e)
        {
	        _taskViewModel.MoveTaskUp();
        }

        private void MoveDownOnClickForTask(object sender, RoutedEventArgs e)
        {
	        _taskViewModel.MoveTaskDown();
        }
	}
}