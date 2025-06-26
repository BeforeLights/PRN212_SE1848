using System;
using System.Collections.Generic;
using System.Linq;
using MaiQuocAnhWPF.Data.models;

namespace MaiQuocAnhWPF.Data.repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private static CustomerRepository? _instance;
        private readonly List<Customer> _customers = new();

        private CustomerRepository() { }

        public static CustomerRepository Instance => _instance ??= new CustomerRepository();

        public IEnumerable<Customer> GetAll() => _customers;
        public Customer? GetById(int id) => _customers.FirstOrDefault(c => c.CustomerID == id);
        public void Add(Customer customer) => _customers.Add(customer);
        public void Update(Customer customer)
        {
            var existing = GetById(customer.CustomerID);
            if (existing is not null)
            {
                existing.CompanyName = customer.CompanyName;
                existing.ContactName = customer.ContactName;
                existing.ContactTitle = customer.ContactTitle;
                existing.Address = customer.Address;
                existing.Phone = customer.Phone;
            }
        }
        public void Delete(int id)
        {
            var customer = GetById(id);
            if (customer is not null) _customers.Remove(customer);
        }
        public IEnumerable<Customer> Search(string keyword) =>
            _customers.Where(c => c.CompanyName.Contains(keyword, StringComparison.OrdinalIgnoreCase));
    }
}
