using MaiQuocAnhWPF.Business;
using MaiQuocAnhWPF.Data.models;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MaiQuocAnhWPF
{
    public partial class EmployeeManagementControl : UserControl
    {
        private readonly EmployeeService _service = new();

        public EmployeeManagementControl()
        {
            InitializeComponent();
            LoadEmployees();
        }

        private void LoadEmployees()
        {
            EmployeeGrid.ItemsSource = _service.GetAllEmployees().ToList();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            var keyword = SearchTextBox.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                LoadEmployees();
            }
            else
            {
                EmployeeGrid.ItemsSource = _service.SearchEmployees(keyword).ToList();
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Text = "";
            LoadEmployees();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new EmployeeDialog();
            if (dialog.ShowDialog() == true)
            {
                _service.AddEmployee(dialog.Employee);
                LoadEmployees();
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeeGrid.SelectedItem is not Employee selected) return;
            
            var dialog = new EmployeeDialog(selected);
            if (dialog.ShowDialog() == true)
            {
                _service.UpdateEmployee(dialog.Employee);
                LoadEmployees();
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeeGrid.SelectedItem is not Employee selected) return;
            
            var result = MessageBox.Show($"Are you sure you want to delete employee '{selected.Name}'?", 
                                       "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                _service.DeleteEmployee(selected.EmployeeID);
                LoadEmployees();
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            LoadEmployees();
        }
    }
}