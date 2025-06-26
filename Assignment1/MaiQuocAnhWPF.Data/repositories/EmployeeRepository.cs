using System;
using System.Collections.Generic;
using System.Linq;
using MaiQuocAnhWPF.Data.models;

namespace MaiQuocAnhWPF.Data.repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private static EmployeeRepository? _instance;
        private readonly List<Employee> _employees = new();

        private EmployeeRepository() { }

        public static EmployeeRepository Instance => _instance ??= new EmployeeRepository();

        public IEnumerable<Employee> GetAll() => _employees;
        public Employee? GetById(int id) => _employees.FirstOrDefault(e => e.EmployeeID == id);
        public Employee? GetByUsername(string username) => _employees.FirstOrDefault(e => e.UserName.Equals(username, StringComparison.OrdinalIgnoreCase));
        public void Add(Employee employee) => _employees.Add(employee);
        public void Update(Employee employee)
        {
            var existing = GetById(employee.EmployeeID);
            if (existing != null)
            {
                existing.Name = employee.Name;
                existing.UserName = employee.UserName;
                existing.Password = employee.Password;
                existing.JobTitle = employee.JobTitle;
                existing.BirthDate = employee.BirthDate;
                existing.HireDate = employee.HireDate;
                existing.Address = employee.Address;
            }
        }
        public void Delete(int id)
        {
            var employee = GetById(id);
            if (employee != null) _employees.Remove(employee);
        }
        public IEnumerable<Employee> Search(string keyword) =>
            _employees.Where(e => e.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                                  e.UserName.Contains(keyword, StringComparison.OrdinalIgnoreCase));
    }
}