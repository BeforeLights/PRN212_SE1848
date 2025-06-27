using MaiQuocAnhWPF.Data.models;
using MaiQuocAnhWPF.Business;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MaiQuocAnhWPF
{
    public partial class MainWindow : Window
    {
        private Employee? _currentEmployee;
        private Customer? _currentCustomer;
        private bool _isAdmin;

        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow(Employee? employee, Customer? customer) : this()
        {
            _currentEmployee = employee;
            _currentCustomer = customer;
            _isAdmin = employee != null;

            SetupUI();
        }

        private void SetupUI()
        {
            if (_isAdmin && _currentEmployee != null)
            {
                // Admin UI setup
                WelcomeText.Text = $"Welcome, {_currentEmployee.Name}!";
                RoleText.Text = $"Role: {_currentEmployee.JobTitle}";
                Title = $"Sales Management System - {_currentEmployee.Name}";

                // Show admin features
                ManagementMenu.Visibility = Visibility.Visible;
                ReportsMenu.Visibility = Visibility.Visible;
                CustomerMenu.Visibility = Visibility.Collapsed;
                AdminActionsPanel.Visibility = Visibility.Visible;
                CustomerActionsPanel.Visibility = Visibility.Collapsed;
            }
            else if (_currentCustomer != null)
            {
                // Customer UI setup
                WelcomeText.Text = $"Welcome, {_currentCustomer.ContactName}!";
                RoleText.Text = $"Customer: {_currentCustomer.CompanyName}";
                Title = $"Sales Management System - {_currentCustomer.ContactName}";

                // Hide admin features and show customer features
                ManagementMenu.Visibility = Visibility.Collapsed;
                ReportsMenu.Visibility = Visibility.Collapsed;
                CustomerMenu.Visibility = Visibility.Visible;
                AdminActionsPanel.Visibility = Visibility.Collapsed;
                CustomerActionsPanel.Visibility = Visibility.Visible;
            }
        }

        // Admin menu handlers
        private void OpenCustomers_Click(object sender, RoutedEventArgs e)
        {
            if (!_isAdmin) return;
            OpenTabIfNotExists("Customers", () => new CustomerManagementControl(_isAdmin));
        }

        private void OpenProducts_Click(object sender, RoutedEventArgs e)
        {
            if (!_isAdmin) return;
            OpenTabIfNotExists("Products", () => new ProductManagementControl());
        }

        private void OpenOrders_Click(object sender, RoutedEventArgs e)
        {
            if (!_isAdmin) return;
            OpenTabIfNotExists("Orders", () => new OrderManagementControl());
        }

        private void OpenCategories_Click(object sender, RoutedEventArgs e)
        {
            if (!_isAdmin) return;
            OpenTabIfNotExists("Categories", () => new CategoryManagementControl());
        }

        private void OpenEmployees_Click(object sender, RoutedEventArgs e)
        {
            if (!_isAdmin) return;
            OpenTabIfNotExists("Employees", () => new EmployeeManagementControl());
        }

        private void OpenSalesReport_Click(object sender, RoutedEventArgs e)
        {
            if (!_isAdmin) return;
            OpenTabIfNotExists("Sales Reports", () => new ReportViewControl("Sales Reports"));
        }

        private void OpenCustomerReport_Click(object sender, RoutedEventArgs e)
        {
            if (!_isAdmin) return;
            OpenTabIfNotExists("Customer Reports", () => new ReportViewControl("Customer Reports"));
        }

        // Customer menu handlers
        private void OpenMyOrders_Click(object sender, RoutedEventArgs e)
        {
            if (_currentCustomer == null) return;
            OpenTabIfNotExists("My Orders", () => new CustomerOrdersControl(_currentCustomer.CustomerID));
        }

        private void EditProfile_Click(object sender, RoutedEventArgs e)
        {
            if (_currentCustomer == null) return;

            var dialog = new CustomerDialog(_currentCustomer);
            if (dialog.ShowDialog() == true)
            {
                var customerService = new CustomerService();
                customerService.UpdateCustomer(dialog.Customer);

                // Update current customer info
                _currentCustomer = dialog.Customer;
                WelcomeText.Text = $"Welcome, {_currentCustomer.ContactName}!";
                RoleText.Text = $"Customer: {_currentCustomer.CompanyName}";

                MessageBox.Show("Profile updated successfully!", "Success",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void OpenTabIfNotExists(string tabHeader, System.Func<UserControl> createControl)
        {
            // Check if tab already exists
            foreach (TabItem tab in MainTabControl.Items)
            {
                if (tab.Header.ToString() == tabHeader)
                {
                    tab.IsSelected = true;
                    return;
                }
            }

            // Create new tab
            var newTab = new TabItem
            {
                Header = tabHeader,
                Content = createControl()
            };
            MainTabControl.Items.Add(newTab);
            newTab.IsSelected = true;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}