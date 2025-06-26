using MaiQuocAnhWPF.Data.models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace MaiQuocAnhWPF.Data.repositories
{
    public class OrderRepository
    {
        private static OrderRepository _instance;
        private readonly List<Order> _orders = new();

        private OrderRepository() { }

        public static OrderRepository Instance => _instance ??= new OrderRepository();

        public IEnumerable<Order> GetAll() => _orders;
        public Order? GetById(int id) => _orders.FirstOrDefault(o => o.OrderID == id);
        public void Add(Order order) => _orders.Add(order);
        public void Update(Order order)
        {
            var existing = GetById(order.OrderID);
            if (existing != null)
            {
                existing.CustomerID = order.CustomerID;
                existing.EmployeeID = order.EmployeeID;
                existing.OrderDate = order.OrderDate;
                existing.OrderDetails = order.OrderDetails;
            }
        }
        public void Delete(int id)
        {
            var order = GetById(id);
            if (order != null) _orders.Remove(order);
        }
        public IEnumerable<Order> SearchByCustomer(int customerId) =>
            _orders.Where(o => o.CustomerID == customerId);
        public IEnumerable<Order> SearchByDateRange(DateTime from, DateTime to) =>
            _orders.Where(o => o.OrderDate >= from && o.OrderDate <= to)
                   .OrderByDescending(o => o.OrderDate);
    }
}