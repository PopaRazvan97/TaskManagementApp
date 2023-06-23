using System.Windows;
using Microsoft.Win32;
using Tema_2.Model;
using Tema_2.ViewModel;

namespace Tema_2.View
{
    public partial class EditToDoListWindow : Window
    {
        private CategoryViewModel _categoryViewModel;
        private string _imagePath;

        public EditToDoListWindow(CategoryViewModel categoryViewModel)
        {
            _categoryViewModel = categoryViewModel;
            InitializeComponent();
        }

        private void BrowseImage(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
            if (openFileDialog.ShowDialog() == true)
            {
                _imagePath = openFileDialog.FileName;
            }
        }

        private void SaveTDL(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(NameTextBox.Text))
            {
                _categoryViewModel.EditToDoList(NameTextBox.Text, _imagePath);
                Close();
            }
            else
            {
                MessageBox.Show("Please enter a name for the TDL.");
            }
        }
    }
}