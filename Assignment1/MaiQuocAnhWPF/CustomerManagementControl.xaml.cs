using MaiQuocAnhWPF.Business;
using MaiQuocAnhWPF.Data.models;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MaiQuocAnhWPF
{
    public partial class CustomerManagementControl : UserControl
    {
        private readonly CustomerService _service = new();
        private readonly bool _isAdmin;

        public CustomerManagementControl(bool isAdmin = true)
        {
            InitializeComponent();
            _isAdmin = isAdmin;
            
            if (!_isAdmin)
            {
                AddButton.Visibility = Visibility.Collapsed;
                EditButton.Visibility = Visibility.Collapsed;
                DeleteButton.Visibility = Visibility.Collapsed;
            }
            
            LoadCustomers();
        }

        private void LoadCustomers()
        {
            CustomerGrid.ItemsSource = _service.GetAllCustomers().ToList();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            var keyword = SearchTextBox.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                LoadCustomers();
            }
            else
            {
                CustomerGrid.ItemsSource = _service.SearchCustomers(keyword).ToList();
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Text = "";
            LoadCustomers();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (!_isAdmin) return;
            
            var dialog = new CustomerDialog();
            if (dialog.ShowDialog() == true)
            {
                _service.AddCustomer(dialog.Customer);
                LoadCustomers();
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (!_isAdmin || CustomerGrid.SelectedItem is not Customer selected) return;
            
            var dialog = new CustomerDialog(selected);
            if (dialog.ShowDialog() == true)
            {
                _service.UpdateCustomer(dialog.Customer);
                LoadCustomers();
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (!_isAdmin || CustomerGrid.SelectedItem is not Customer selected) return;
            
            var result = MessageBox.Show($"Are you sure you want to delete customer '{selected.CompanyName}'?", 
                                       "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                _service.DeleteCustomer(selected.CustomerID);
                LoadCustomers();
            }
        }

        private void ViewOrders_Click(object sender, RoutedEventArgs e)
        {
            if (CustomerGrid.SelectedItem is not Customer selected)
            {
                MessageBox.Show("Please select a customer to view their orders.", "No Selection", 
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            
            // Open the new CustomerOrdersWindow
            var ordersWindow = new CustomerOrdersWindow(selected);
            ordersWindow.ShowDialog();
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            LoadCustomers();
        }
    }
}