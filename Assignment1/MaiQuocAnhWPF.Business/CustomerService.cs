using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaiQuocAnhWPF.Data.models;
using MaiQuocAnhWPF.Data.repositories;

namespace MaiQuocAnhWPF.Business
{
    public class CustomerService
    {
        private readonly CustomerRepository _repo = CustomerRepository.Instance;

        public IEnumerable<Customer> GetAllCustomers() => _repo.GetAll();
        public Customer? GetCustomerById(int id) => _repo.GetById(id);
        public void AddCustomer(Customer customer) => _repo.Add(customer);
        public void UpdateCustomer(Customer customer) => _repo.Update(customer);
        public void DeleteCustomer(int id) => _repo.Delete(id);
        public IEnumerable<Customer> SearchCustomers(string keyword) => _repo.Search(keyword);
    }
}
