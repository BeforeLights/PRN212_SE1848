using MaiQuocAnhWPF.Data.models;
using MaiQuocAnhWPF.Data.repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MaiQuocAnhWPF.Business
{
    public class ReportService
    {
        private readonly OrderRepository _orderRepo = OrderRepository.Instance;
        private readonly CustomerRepository _customerRepo = CustomerRepository.Instance;
        private readonly ProductRepository _productRepo = ProductRepository.Instance;
        private readonly EmployeeRepository _employeeRepo = EmployeeRepository.Instance;

        public IEnumerable<OrderStatistic> GetOrderStatisticsByDay(DateTime startDate, DateTime endDate)
        {
            var orders = _orderRepo.GetAll()
                .Where(o => o.OrderDate.Date >= startDate.Date && o.OrderDate.Date <= endDate.Date)
                .GroupBy(o => o.OrderDate.Date)
                .Select(g => new OrderStatistic
                {
                    Period = g.Key.ToString("yyyy-MM-dd"),
                    PeriodDate = g.Key,
                    OrderCount = g.Count(),
                    TotalRevenue = g.Sum(o => o.OrderDetails.Sum(od => od.UnitPrice * od.Quantity * (1 - (decimal)od.Discount))),
                    AverageOrderValue = g.Average(o => o.OrderDetails.Sum(od => od.UnitPrice * od.Quantity * (1 - (decimal)od.Discount))),
                    UniqueCustomers = g.Select(o => o.CustomerID).Distinct().Count(),
                    TopEmployee = GetTopEmployeeForOrders(g),
                    TopProduct = GetTopProductForOrders(g)
                })
                .OrderByDescending(s => s.PeriodDate)
                .ToList();

            return orders;
        }

        public IEnumerable<OrderStatistic> GetOrderStatisticsByWeek(DateTime startDate, DateTime endDate)
        {
            var orders = _orderRepo.GetAll()
                .Where(o => o.OrderDate.Date >= startDate.Date && o.OrderDate.Date <= endDate.Date)
                .GroupBy(o => GetWeekStart(o.OrderDate))
                .Select(g => new OrderStatistic
                {
                    Period = $"Week of {g.Key:yyyy-MM-dd}",
                    PeriodDate = g.Key,
                    OrderCount = g.Count(),
                    TotalRevenue = g.Sum(o => o.OrderDetails.Sum(od => od.UnitPrice * od.Quantity * (1 - (decimal)od.Discount))),
                    AverageOrderValue = g.Average(o => o.OrderDetails.Sum(od => od.UnitPrice * od.Quantity * (1 - (decimal)od.Discount))),
                    UniqueCustomers = g.Select(o => o.CustomerID).Distinct().Count(),
                    TopEmployee = GetTopEmployeeForOrders(g),
                    TopProduct = GetTopProductForOrders(g)
                })
                .OrderByDescending(s => s.PeriodDate)
                .ToList();

            return orders;
        }

        public IEnumerable<OrderStatistic> GetOrderStatisticsByMonth(DateTime startDate, DateTime endDate)
        {
            var orders = _orderRepo.GetAll()
                .Where(o => o.OrderDate.Date >= startDate.Date && o.OrderDate.Date <= endDate.Date)
                .GroupBy(o => new { o.OrderDate.Year, o.OrderDate.Month })
                .Select(g => new OrderStatistic
                {
                    Period = $"{g.Key.Year}-{g.Key.Month:D2}",
                    PeriodDate = new DateTime(g.Key.Year, g.Key.Month, 1),
                    OrderCount = g.Count(),
                    TotalRevenue = g.Sum(o => o.OrderDetails.Sum(od => od.UnitPrice * od.Quantity * (1 - (decimal)od.Discount))),
                    AverageOrderValue = g.Average(o => o.OrderDetails.Sum(od => od.UnitPrice * od.Quantity * (1 - (decimal)od.Discount))),
                    UniqueCustomers = g.Select(o => o.CustomerID).Distinct().Count(),
                    TopEmployee = GetTopEmployeeForOrders(g),
                    TopProduct = GetTopProductForOrders(g)
                })
                .OrderByDescending(s => s.PeriodDate)
                .ToList();

            return orders;
        }

        public IEnumerable<CustomerStatistic> GetCustomerStatistics(DateTime startDate, DateTime endDate)
        {
            var customerStats = _orderRepo.GetAll()
                .Where(o => o.OrderDate.Date >= startDate.Date && o.OrderDate.Date <= endDate.Date)
                .GroupBy(o => o.CustomerID)
                .Select(g => new CustomerStatistic
                {
                    CustomerID = g.Key,
                    CustomerName = _customerRepo.GetById(g.Key)?.CompanyName ?? "Unknown",
                    OrderCount = g.Count(),
                    TotalSpent = g.Sum(o => o.OrderDetails.Sum(od => od.UnitPrice * od.Quantity * (1 - (decimal)od.Discount))),
                    AverageOrderValue = g.Average(o => o.OrderDetails.Sum(od => od.UnitPrice * od.Quantity * (1 - (decimal)od.Discount))),
                    LastOrderDate = g.Max(o => o.OrderDate)
                })
                .OrderByDescending(c => c.TotalSpent)
                .ToList();

            return customerStats;
        }

        public IEnumerable<ProductStatistic> GetProductStatistics(DateTime startDate, DateTime endDate)
        {
            var productStats = _orderRepo.GetAll()
                .Where(o => o.OrderDate.Date >= startDate.Date && o.OrderDate.Date <= endDate.Date)
                .SelectMany(o => o.OrderDetails)
                .GroupBy(od => od.ProductID)
                .Select(g => new ProductStatistic
                {
                    ProductID = g.Key,
                    ProductName = _productRepo.GetById(g.Key)?.ProductName ?? "Unknown",
                    QuantitySold = g.Sum(od => od.Quantity),
                    TotalRevenue = g.Sum(od => od.UnitPrice * od.Quantity * (1 - (decimal)od.Discount)),
                    OrderCount = g.Count()
                })
                .OrderByDescending(p => p.TotalRevenue)
                .ToList();

            return productStats;
        }

        private DateTime GetWeekStart(DateTime date)
        {
            var diff = (7 + (date.DayOfWeek - DayOfWeek.Monday)) % 7;
            return date.AddDays(-1 * diff).Date;
        }

        private string GetTopEmployeeForOrders(IEnumerable<Order> orders)
        {
            var topEmployeeId = orders.GroupBy(o => o.EmployeeID)
                .OrderByDescending(g => g.Count())
                .FirstOrDefault()?.Key ?? 0;

            return _employeeRepo.GetById(topEmployeeId)?.Name ?? "Unknown";
        }

        private string GetTopProductForOrders(IEnumerable<Order> orders)
        {
            var topProductId = orders.SelectMany(o => o.OrderDetails)
                .GroupBy(od => od.ProductID)
                .OrderByDescending(g => g.Sum(od => od.Quantity))
                .FirstOrDefault()?.Key ?? 0;

            return _productRepo.GetById(topProductId)?.ProductName ?? "Unknown";
        }
    }

    // Report Models
    public class OrderStatistic
    {
        public string Period { get; set; } = string.Empty;
        public DateTime PeriodDate { get; set; }
        public int OrderCount { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal AverageOrderValue { get; set; }
        public int UniqueCustomers { get; set; }
        public string TopEmployee { get; set; } = string.Empty;
        public string TopProduct { get; set; } = string.Empty;
    }

    public class CustomerStatistic
    {
        public int CustomerID { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public int OrderCount { get; set; }
        public decimal TotalSpent { get; set; }
        public decimal AverageOrderValue { get; set; }
        public DateTime LastOrderDate { get; set; }
    }

    public class ProductStatistic
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int QuantitySold { get; set; }
        public decimal TotalRevenue { get; set; }
        public int OrderCount { get; set; }
    }
}