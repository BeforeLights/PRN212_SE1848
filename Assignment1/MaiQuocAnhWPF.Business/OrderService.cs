using MaiQuocAnhWPF.Data.models;
using MaiQuocAnhWPF.Data.repositories;
using System;
using System.Collections.Generic;

namespace MaiQuocAnhWPF.Business
{
    public class OrderService
    {
        private readonly OrderRepository _repo = OrderRepository.Instance;

        public IEnumerable<Order> GetAllOrders() => _repo.GetAll();
        public Order? GetOrderById(int id) => _repo.GetById(id);
        public void AddOrder(Order order) => _repo.Add(order);
        public void UpdateOrder(Order order) => _repo.Update(order);
        public void DeleteOrder(int id) => _repo.Delete(id);
        public IEnumerable<Order> SearchOrders(string keyword) => _repo.Search(keyword);
        public IEnumerable<Order> GetOrdersByCustomer(int customerId) => _repo.GetOrdersByCustomer(customerId);
    }
}
