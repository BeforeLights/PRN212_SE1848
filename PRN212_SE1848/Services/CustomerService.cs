using BusinessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CustomerService : ICustomerService
    {
        ICustomerRepository iCustomerRepository;
        public CustomerService()
        {
            iCustomerRepository = new CustomerRepository();
        }
        public void GenerateSampleDataset()
        {
            iCustomerRepository.GenerateSampleDataset();
        }

        public List<Customer> GetCustomers()
        {
            return iCustomerRepository.GetCustomers();
        }
    }
}
