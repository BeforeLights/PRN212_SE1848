using MaiQuocAnhWPF.Data.repositories;
using MaiQuocAnhWPF.Data.models;
using System.Linq;
using System.Windows;

namespace MaiQuocAnhWPF
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            RoleComboBox.SelectionChanged += RoleComboBox_SelectionChanged;

            // Ensure Administrator is selected by default
            RoleComboBox.SelectedIndex = 0;
            UpdateLabels();
        }

        private void RoleComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            UpdateLabels();
        }

        private void UpdateLabels()
        {
            if (RoleComboBox.SelectedIndex == 0) // Admin
            {
                UserLabel.Content = "Username:";
                PasswordLabel.Visibility = Visibility.Visible;
                PasswordBox.Visibility = Visibility.Visible;

                // Add placeholder text for better UX
                if (string.IsNullOrEmpty(UserTextBox.Text) || UserTextBox.Text == "555-0101")
                {
                    UserTextBox.Text = "admin";
                }
            }
            else // Customer
            {
                UserLabel.Content = "Phone:";
                PasswordLabel.Visibility = Visibility.Collapsed;
                PasswordBox.Visibility = Visibility.Collapsed;

                // Add placeholder text for better UX
                if (UserTextBox.Text == "admin" || string.IsNullOrEmpty(UserTextBox.Text))
                {
                    UserTextBox.Text = "555-0101";
                }
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (RoleComboBox.SelectedIndex == 0) // Admin
            {
                var username = UserTextBox.Text.Trim();
                var password = PasswordBox.Password;

                if (string.IsNullOrEmpty(username))
                {
                    MessageBox.Show("Please enter a username.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Please enter a password.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Debug: Check if any employees exist
                var allEmployees = EmployeeRepository.Instance.GetAll().ToList();
                System.Diagnostics.Debug.WriteLine($"Total employees in repository: {allEmployees.Count}");
                
                foreach (var emp in allEmployees)
                {
                    System.Diagnostics.Debug.WriteLine($"Employee: {emp.UserName} / {emp.Password}");
                }

                var employee = EmployeeRepository.Instance.GetByUsername(username);
                
                if (employee == null)
                {
                    MessageBox.Show($"No employee found with username '{username}'.\n\nDebug Info:\nTotal employees: {allEmployees.Count}\nAvailable usernames: {string.Join(", ", allEmployees.Select(e => e.UserName))}", 
                        "Login Debug", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                System.Diagnostics.Debug.WriteLine($"Found employee: {employee.UserName}, Password: '{employee.Password}', Entered: '{password}'");

                if (employee.Password == password)
                {
                    MessageBox.Show($"Login successful for {employee.Name}!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    OpenMainWindow(employee, null);
                }
                else
                {
                    MessageBox.Show($"Password mismatch!\n\nUsername: {username}\nStored Password: '{employee.Password}'\nEntered Password: '{password}'\n\nTry:\nUsername: admin\nPassword: admin123",
                        "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else // Customer
            {
                var phone = UserTextBox.Text.Trim();

                if (string.IsNullOrEmpty(phone))
                {
                    MessageBox.Show("Please enter your phone number.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var customer = CustomerRepository.Instance.GetAll().FirstOrDefault(c => c.Phone == phone);
                if (customer != null)
                {
                    MessageBox.Show($"Customer login successful for {customer.ContactName}!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    OpenMainWindow(null, customer);
                }
                else
                {
                    MessageBox.Show("Invalid phone number.\n\nTry: 555-0101, 555-0102, or 555-0103",
                        "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void OpenMainWindow(Employee? employee, Customer? customer)
        {
            var mainWindow = new MainWindow(employee, customer);
            mainWindow.Show();
            this.Close();
        }
    }
}