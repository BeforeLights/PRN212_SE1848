using MaiQuocAnhWPF.Data.models;
using MaiQuocAnhWPF.Data.repositories;
using System.Collections.Generic;

namespace MaiQuocAnhWPF.Business
{
    public class EmployeeService
    {
        private readonly EmployeeRepository _repo = EmployeeRepository.Instance;

        public IEnumerable<Employee> GetAllEmployees() => _repo.GetAll();
        public Employee? GetEmployeeById(int id) => _repo.GetById(id);
        public Employee? GetEmployeeByUsername(string username) => _repo.GetByUsername(username);
        public void AddEmployee(Employee employee) => _repo.Add(employee);
        public void UpdateEmployee(Employee employee) => _repo.Update(employee);
        public void DeleteEmployee(int id) => _repo.Delete(id);
        public IEnumerable<Employee> SearchEmployees(string keyword) => _repo.Search(keyword);
    }
}