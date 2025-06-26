using MaiQuocAnhWPF.Data.models;
using MaiQuocAnhWPF.Data.repositories;
using System;
using System.Windows;

namespace MaiQuocAnhWPF
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            EmployeeRepository.Instance.Add(new Employee
            {
                EmployeeID = 1,
                Name = "Admin",
                UserName = "admin",
                Password = "admin123",
                JobTitle = "Administrator"
            });

            CustomerRepository.Instance.Add(new Customer
            {
                CustomerID = 1,
                CompanyName = "Test Company",
                ContactName = "John Doe",
                ContactTitle = "Manager",
                Address = "123 Main St",
                Phone = "1234567890"
            });

            base.OnStartup(e);
        }
    }
}
