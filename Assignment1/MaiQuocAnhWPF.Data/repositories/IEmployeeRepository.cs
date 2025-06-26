using MaiQuocAnhWPF.Data.models;
using System.Collections.Generic;

namespace MaiQuocAnhWPF.Data.repositories
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAll();
        Employee? GetById(int id);
        Employee? GetByUsername(string username);
        void Add(Employee employee);
        void Update(Employee employee);
        void Delete(int id);
        IEnumerable<Employee> Search(string keyword);
    }
}