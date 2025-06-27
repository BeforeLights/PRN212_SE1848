using MaiQuocAnhWPF.Data.models;
using MaiQuocAnhWPF.Data.repositories;
using System;
using System.Linq;

namespace MaiQuocAnhWPF.Data
{
    public static class SampleDataInitializer
    {
        public static void Initialize()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("Starting data initialization...");
                
                // Check if employees exist
                var existingEmployees = EmployeeRepository.Instance.GetAll().ToList();
                System.Diagnostics.Debug.WriteLine($"Existing employees: {existingEmployees.Count}");
                
                // Check if customers exist  
                var existingCustomers = CustomerRepository.Instance.GetAll().ToList();
                System.Diagnostics.Debug.WriteLine($"Existing customers: {existingCustomers.Count}");
                
                // Initialize employees if they don't exist
                if (!existingEmployees.Any())
                {
                    System.Diagnostics.Debug.WriteLine("No employees found. Initializing employees...");
                    InitializeEmployees();
                }
                
                // Initialize other data if it doesn't exist
                if (!existingCustomers.Any())
                {
                    System.Diagnostics.Debug.WriteLine("No customers found. Initializing all sample data...");
                    InitializeCategories();
                    InitializeCustomers();
                    InitializeProducts();
                    InitializeOrders();
                }
                
                // Verify data was created
                var newEmployees = EmployeeRepository.Instance.GetAll().ToList();
                System.Diagnostics.Debug.WriteLine($"After initialization - Employees: {newEmployees.Count}");
                foreach (var emp in newEmployees)
                {
                    System.Diagnostics.Debug.WriteLine($"  - {emp.UserName}: {emp.Password}");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error during initialization: {ex.Message}");
                throw;
            }
        }

        private static void InitializeCategories()
        {
            System.Diagnostics.Debug.WriteLine("Initializing categories...");
            var repo = CategoryRepository.Instance;
            repo.Add(new Category { CategoryID = 1, CategoryName = "Electronics", Description = "Electronic devices and accessories" });
            repo.Add(new Category { CategoryID = 2, CategoryName = "Clothing", Description = "Apparel and fashion items" });
            repo.Add(new Category { CategoryID = 3, CategoryName = "Books", Description = "Books and publications" });
            repo.Add(new Category { CategoryID = 4, CategoryName = "Home & Garden", Description = "Home improvement and garden supplies" });
        }

        private static void InitializeEmployees()
        {
            System.Diagnostics.Debug.WriteLine("Initializing employees...");
            var repo = EmployeeRepository.Instance;
            repo.Add(new Employee 
            { 
                EmployeeID = 1, 
                Name = "Admin User", 
                UserName = "admin", 
                Password = "admin123", 
                JobTitle = "System Administrator",
                BirthDate = new DateTime(1985, 5, 15),
                HireDate = new DateTime(2020, 1, 1),
                Address = "123 Main St, City"
            });
            repo.Add(new Employee 
            { 
                EmployeeID = 2, 
                Name = "John Manager", 
                UserName = "jmanager", 
                Password = "pass123", 
                JobTitle = "Sales Manager",
                BirthDate = new DateTime(1980, 8, 20),
                HireDate = new DateTime(2019, 3, 15),
                Address = "456 Oak Ave, City"
            });
            System.Diagnostics.Debug.WriteLine("Employees initialized successfully.");
        }

        private static void InitializeCustomers()
        {
            System.Diagnostics.Debug.WriteLine("Initializing customers...");
            var repo = CustomerRepository.Instance;
            repo.Add(new Customer 
            { 
                CustomerID = 1, 
                CompanyName = "Tech Solutions Inc", 
                ContactName = "Alice Johnson", 
                ContactTitle = "CEO", 
                Address = "789 Business Blvd, Tech City", 
                Phone = "555-0101" 
            });
            repo.Add(new Customer 
            { 
                CustomerID = 2, 
                CompanyName = "Fashion Forward LLC", 
                ContactName = "Bob Smith", 
                ContactTitle = "Manager", 
                Address = "321 Fashion St, Style Town", 
                Phone = "555-0102" 
            });
            repo.Add(new Customer 
            { 
                CustomerID = 3, 
                CompanyName = "Book World", 
                ContactName = "Carol Davis", 
                ContactTitle = "Owner", 
                Address = "654 Library Lane, Reading City", 
                Phone = "555-0103" 
            });
        }

        private static void InitializeProducts()
        {
            System.Diagnostics.Debug.WriteLine("Initializing products...");
            var repo = ProductRepository.Instance;
            repo.Add(new Product 
            { 
                ProductID = 1, 
                ProductName = "Laptop Computer", 
                CategoryID = 1, 
                UnitPrice = 999.99m, 
                UnitsInStock = 25, 
                QuantityPerUnit = "1 unit",
                Discontinued = false
            });
            repo.Add(new Product 
            { 
                ProductID = 2, 
                ProductName = "T-Shirt", 
                CategoryID = 2, 
                UnitPrice = 19.99m, 
                UnitsInStock = 100, 
                QuantityPerUnit = "1 piece",
                Discontinued = false
            });
            repo.Add(new Product 
            { 
                ProductID = 3, 
                ProductName = "Programming Book", 
                CategoryID = 3, 
                UnitPrice = 49.99m, 
                UnitsInStock = 50, 
                QuantityPerUnit = "1 book",
                Discontinued = false
            });
        }

        private static void InitializeOrders()
        {
            System.Diagnostics.Debug.WriteLine("Initializing orders...");
            var repo = OrderRepository.Instance;
            
            // Order 1 - Customer 1 (Tech Solutions Inc)
            var order1 = new Order 
            { 
                OrderID = 1, 
                CustomerID = 1, 
                EmployeeID = 1, 
                OrderDate = DateTime.Now.AddDays(-5) 
            };
            order1.OrderDetails.Add(new OrderDetail 
            { 
                OrderID = 1, 
                ProductID = 1, 
                UnitPrice = 999.99m, 
                Quantity = 2, 
                Discount = 0.1f 
            });
            repo.Add(order1);

            // Order 2 - Customer 2 (Fashion Forward LLC)
            var order2 = new Order 
            { 
                OrderID = 2, 
                CustomerID = 2, 
                EmployeeID = 2, 
                OrderDate = DateTime.Now.AddDays(-3) 
            };
            order2.OrderDetails.Add(new OrderDetail 
            { 
                OrderID = 2, 
                ProductID = 2, 
                UnitPrice = 19.99m, 
                Quantity = 5, 
                Discount = 0f 
            });
            repo.Add(order2);

            // Order 3 - Customer 1 (Tech Solutions Inc) - Another order
            var order3 = new Order 
            { 
                OrderID = 3, 
                CustomerID = 1, 
                EmployeeID = 1, 
                OrderDate = DateTime.Now.AddDays(-1) 
            };
            order3.OrderDetails.Add(new OrderDetail 
            { 
                OrderID = 3, 
                ProductID = 3, 
                UnitPrice = 49.99m, 
                Quantity = 3, 
                Discount = 0.05f 
            });
            repo.Add(order3);

            // Order 4 - Customer 3 (Book World)
            var order4 = new Order 
            { 
                OrderID = 4, 
                CustomerID = 3, 
                EmployeeID = 2, 
                OrderDate = DateTime.Now.AddDays(-7) 
            };
            order4.OrderDetails.Add(new OrderDetail 
            { 
                OrderID = 4, 
                ProductID = 1, 
                UnitPrice = 999.99m, 
                Quantity = 1, 
                Discount = 0f 
            });
            order4.OrderDetails.Add(new OrderDetail 
            { 
                OrderID = 4, 
                ProductID = 3, 
                UnitPrice = 49.99m, 
                Quantity = 2, 
                Discount = 0f 
            });
            repo.Add(order4);
        }
    }
}