using System;
using System.Collections.Generic;
using System.Linq;
using MaiQuocAnhWPF.Data.models;

namespace MaiQuocAnhWPF.Data.repositories
{
    public class CustomerRepository : IRepository<Customer>
    {
        private static CustomerRepository? _instance;
        private static readonly object _lock = new object();
        private readonly List<Customer> _customers = new();
        private readonly JsonDataPersistence<Customer> _persistence = new();
        private const string DATA_FILE = "customers.json";

        private CustomerRepository()
        {
            LoadCustomers();
        }

        public static CustomerRepository Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        _instance ??= new CustomerRepository();
                    }
                }
                return _instance;
            }
        }

        private void LoadCustomers()
        {
            try
            {
                var loadedCustomers = _persistence.LoadData(DATA_FILE);
                _customers.Clear();
                _customers.AddRange(loadedCustomers);
            }
            catch (Exception ex)
            {
                // Log error but continue with empty list
                System.Diagnostics.Debug.WriteLine($"Error loading customers: {ex.Message}");
            }
        }

        private void SaveCustomers()
        {
            try
            {
                _persistence.SaveData(_customers, DATA_FILE);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to save customers: {ex.Message}");
            }
        }

        public IEnumerable<Customer> GetAll() => _customers.ToList();

        public Customer? GetById(int id) => _customers.FirstOrDefault(c => c.CustomerID == id);

        public void Add(Customer customer)
        {
            if (customer.CustomerID == 0)
                customer.CustomerID = _customers.Count > 0 ? _customers.Max(c => c.CustomerID) + 1 : 1;

            _customers.Add(customer);
            SaveCustomers(); // Save after adding
        }

        public void Update(Customer customer)
        {
            var existing = GetById(customer.CustomerID);
            if (existing != null)
            {
                existing.CompanyName = customer.CompanyName;
                existing.ContactName = customer.ContactName;
                existing.ContactTitle = customer.ContactTitle;
                existing.Address = customer.Address;
                existing.Phone = customer.Phone;

                SaveCustomers(); // Save after updating
            }
        }

        public void Delete(int id)
        {
            var customer = GetById(id);
            if (customer != null)
            {
                _customers.Remove(customer);
                SaveCustomers(); // Save after deleting
            }
        }

        public IEnumerable<Customer> Search(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return GetAll();

            return _customers.Where(c =>
                c.CompanyName.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                c.ContactName.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                c.Phone.Contains(keyword, StringComparison.OrdinalIgnoreCase));
        }
    }
}
