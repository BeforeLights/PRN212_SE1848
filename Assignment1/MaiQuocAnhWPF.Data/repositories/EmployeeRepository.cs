using System;
using System.Collections.Generic;
using System.Linq;
using MaiQuocAnhWPF.Data.models;

namespace MaiQuocAnhWPF.Data.repositories
{
    public class EmployeeRepository : IRepository<Employee>
    {
        private static EmployeeRepository? _instance;
        private static readonly object _lock = new object();
        private readonly List<Employee> _employees = new();

        private EmployeeRepository() { }

        public static EmployeeRepository Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        _instance ??= new EmployeeRepository();
                    }
                }
                return _instance;
            }
        }

        public IEnumerable<Employee> GetAll() => _employees.ToList();

        public Employee? GetById(int id) => _employees.FirstOrDefault(e => e.EmployeeID == id);

        public Employee? GetByUsername(string username) => _employees.FirstOrDefault(e => e.UserName == username);

        public void Add(Employee employee)
        {
            if (employee.EmployeeID == 0)
                employee.EmployeeID = _employees.Count > 0 ? _employees.Max(e => e.EmployeeID) + 1 : 1;
            _employees.Add(employee);
        }

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
            if (employee != null)
                _employees.Remove(employee);
        }

        public IEnumerable<Employee> Search(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return GetAll();

            return _employees.Where(e => 
                e.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                e.UserName.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                e.JobTitle.Contains(keyword, StringComparison.OrdinalIgnoreCase));
        }
    }
}