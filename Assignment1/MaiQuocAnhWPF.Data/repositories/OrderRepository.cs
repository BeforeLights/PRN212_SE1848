using MaiQuocAnhWPF.Data.models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MaiQuocAnhWPF.Data.repositories
{
    public class OrderRepository : IRepository<Order>
    {
        private static OrderRepository? _instance;
        private static readonly object _lock = new object();
        private readonly List<Order> _orders = new();
        private readonly JsonDataPersistence<Order> _persistence = new();
        private const string DATA_FILE = "orders.json";

        private OrderRepository() 
        {
            LoadOrders();
        }

        public static OrderRepository Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        _instance ??= new OrderRepository();
                    }
                }
                return _instance;
            }
        }

        private void LoadOrders()
        {
            try
            {
                var loadedOrders = _persistence.LoadData(DATA_FILE);
                _orders.Clear();
                _orders.AddRange(loadedOrders);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading orders: {ex.Message}");
            }
        }

        private void SaveOrders()
        {
            try
            {
                _persistence.SaveData(_orders, DATA_FILE);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to save orders: {ex.Message}");
            }
        }

        public IEnumerable<Order> GetAll() => _orders.ToList();

        public Order? GetById(int id) => _orders.FirstOrDefault(o => o.OrderID == id);

        public void Add(Order order)
        {
            if (order.OrderID == 0)
                order.OrderID = _orders.Count > 0 ? _orders.Max(o => o.OrderID) + 1 : 1;
            
            _orders.Add(order);
            SaveOrders(); // Save after adding
        }

        public void Update(Order order)
        {
            var existing = GetById(order.OrderID);
            if (existing != null)
            {
                existing.CustomerID = order.CustomerID;
                existing.EmployeeID = order.EmployeeID;
                existing.OrderDate = order.OrderDate;
                existing.OrderDetails = order.OrderDetails;
                
                SaveOrders(); // Save after updating
            }
        }

        public void Delete(int id)
        {
            var order = GetById(id);
            if (order != null)
            {
                _orders.Remove(order);
                SaveOrders(); // Save after deleting
            }
        }

        public IEnumerable<Order> Search(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return GetAll();

            return _orders.Where(o => 
                o.OrderID.ToString().Contains(keyword) ||
                o.CustomerID.ToString().Contains(keyword) ||
                o.EmployeeID.ToString().Contains(keyword));
        }

        public IEnumerable<Order> GetOrdersByCustomer(int customerId)
        {
            return _orders.Where(o => o.CustomerID == customerId);
        }
    }
}