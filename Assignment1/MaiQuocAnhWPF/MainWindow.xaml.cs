using MaiQuocAnhWPF.Data.models;
using MaiQuocAnhWPF.Business;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
                WelcomeText.Text = $"Welcome, {_currentEmployee.Name}!";
                RoleText.Text = $"Role: {_currentEmployee.JobTitle}";
                Title = $"Sales Management System - {_currentEmployee.Name}";
            }
            else if (_currentCustomer != null)
            {
                WelcomeText.Text = $"Welcome, {_currentCustomer.ContactName}!";
                RoleText.Text = $"Customer: {_currentCustomer.CompanyName}";
                Title = $"Sales Management System - {_currentCustomer.ContactName}";

                // Hide admin-only features for customers
                EmployeesMenuItem.Visibility = Visibility.Collapsed;
                ReportsMenu.Visibility = Visibility.Collapsed;
                ReportsButton.Visibility = Visibility.Collapsed;
            }
        }

        private void OpenCustomers_Click(object sender, RoutedEventArgs e)
        {
            OpenTabIfNotExists("Customers", () => new CustomerManagementControl(_isAdmin));
        }

        private void OpenProducts_Click(object sender, RoutedEventArgs e)
        {
            OpenTabIfNotExists("Products", () => new ProductManagementControl());
        }

        private void OpenOrders_Click(object sender, RoutedEventArgs e)
        {
            OpenTabIfNotExists("Orders", () => new OrderManagementControl());
        }

        private void OpenCategories_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Category Management - Feature coming soon!", "Info");
        }

        private void OpenEmployees_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Employee Management - Feature coming soon!", "Info");
        }

        private void OpenSalesReport_Click(object sender, RoutedEventArgs e)
        {
            OpenTabIfNotExists("Sales Report", () => new ReportViewControl("Sales Report"));
        }

        private void OpenCustomerReport_Click(object sender, RoutedEventArgs e)
        {
            OpenTabIfNotExists("Customer Report", () => new ReportViewControl("Customer Report"));
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