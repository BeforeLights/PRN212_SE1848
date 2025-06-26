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
                UserLabel.Text = "Username:";
                PasswordLabel.Visibility = Visibility.Visible;
                PasswordBox.Visibility = Visibility.Visible;
            }
            else // Customer
            {
                UserLabel.Text = "Phone:";
                PasswordLabel.Visibility = Visibility.Collapsed;
                PasswordBox.Visibility = Visibility.Collapsed;
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (RoleComboBox.SelectedIndex == 0) // Admin
            {
                var username = UserTextBox.Text.Trim();
                var password = PasswordBox.Password;
                var employee = EmployeeRepository.Instance.GetByUsername(username);
                if (employee != null && employee.Password == password)
                {
                    MessageBox.Show("Admin login successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    OpenMainWindow(employee, null);
                }
                else
                {
                    MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else // Customer
            {
                var phone = UserTextBox.Text.Trim();
                var customer = CustomerRepository.Instance.GetAll().FirstOrDefault(c => c.Phone == phone);
                if (customer != null)
                {
                    MessageBox.Show("Customer login successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    OpenMainWindow(null, customer);
                }
                else
                {
                    MessageBox.Show("Invalid phone number.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
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