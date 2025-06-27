using MaiQuocAnhWPF.Data.models;
using System.Windows;

namespace MaiQuocAnhWPF
{
    public partial class EmployeeDialog : Window
    {
        public Employee Employee { get; set; }

        public EmployeeDialog(Employee? employee = null)
        {
            InitializeComponent();
            
            if (employee != null)
            {
                Employee = new Employee
                {
                    EmployeeID = employee.EmployeeID,
                    Name = employee.Name,
                    UserName = employee.UserName,
                    Password = employee.Password,
                    JobTitle = employee.JobTitle,
                    BirthDate = employee.BirthDate,
                    HireDate = employee.HireDate,
                    Address = employee.Address
                };
                PasswordBox.Password = employee.Password;
            }
            else
            {
                Employee = new Employee();
            }
            
            DataContext = Employee;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Employee.Name))
            {
                MessageBox.Show("Employee name is required.", "Validation Error", 
                              MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            
            if (string.IsNullOrWhiteSpace(Employee.UserName))
            {
                MessageBox.Show("Username is required.", "Validation Error", 
                              MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            
            // Update password from PasswordBox
            Employee.Password = PasswordBox.Password;
            
            DialogResult = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}