using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using MaiQuocAnhWPF.Data.models;
using MaiQuocAnhWPF.Business;

namespace MaiQuocAnhWPF
{
    public partial class OrderDialog : Window
    {
        public Order Order { get; set; }
        private readonly CustomerService _customerService = new();
        private readonly EmployeeService _employeeService = new();
        private readonly ProductService _productService = new();

        public OrderDialog(Order? order = null)
        {
            InitializeComponent();
            
            // Initialize Order
            if (order != null)
            {
                Order = new Order
                {
                    OrderID = order.OrderID,
                    CustomerID = order.CustomerID,
                    EmployeeID = order.EmployeeID,
                    OrderDate = order.OrderDate,
                    OrderDetails = new ObservableCollection<OrderDetail>(order.OrderDetails)
                };
            }
            else
            {
                Order = new Order
                {
                    OrderDate = DateTime.Now,
                    OrderDetails = new ObservableCollection<OrderDetail>()
                };
            }
            
            LoadComboBoxData();
            DataContext = Order;
            
            // Subscribe to collection changes to update total
            Order.OrderDetails.CollectionChanged += (s, e) => UpdateOrderTotal();
            UpdateOrderTotal();
        }

        private void LoadComboBoxData()
        {
            CustomerComboBox.ItemsSource = _customerService.GetAllCustomers().ToList();
            EmployeeComboBox.ItemsSource = _employeeService.GetAllEmployees().ToList();
            
            // Set up product column for DataGrid
            var products = _productService.GetAllProducts().ToList();
            ProductColumn.ItemsSource = products;
        }

        private void AddOrderDetail_Click(object sender, RoutedEventArgs e)
        {
            var newDetail = new OrderDetail
            {
                OrderID = Order.OrderID,
                Quantity = 1,
                Discount = 0f
            };
            
            Order.OrderDetails.Add(newDetail);
        }

        private void RemoveOrderDetail_Click(object sender, RoutedEventArgs e)
        {
            if (OrderDetailsGrid.SelectedItem is OrderDetail selectedDetail)
            {
                Order.OrderDetails.Remove(selectedDetail);
            }
        }

        private void UpdateOrderTotal()
        {
            decimal total = 0;
            foreach (var detail in Order.OrderDetails)
            {
                total += detail.UnitPrice * detail.Quantity * (1 - (decimal)detail.Discount);
            }
            OrderTotalTextBlock.Text = total.ToString("C");
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            // Validate required fields
            if (Order.CustomerID == 0)
            {
                MessageBox.Show("Please select a customer.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            
            if (Order.EmployeeID == 0)
            {
                MessageBox.Show("Please select an employee.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Update unit prices from selected products
            var products = _productService.GetAllProducts().ToList();
            foreach (var detail in Order.OrderDetails)
            {
                var product = products.FirstOrDefault(p => p.ProductID == detail.ProductID);
                if (product != null)
                {
                    detail.UnitPrice = product.UnitPrice;
                }
            }

            DialogResult = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }

    // Converter for calculating order detail totals
    public class OrderDetailTotalConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is OrderDetail detail)
            {
                return detail.UnitPrice * detail.Quantity * (1 - (decimal)detail.Discount);
            }
            return 0m;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    // Converter for calculating order totals
    public class OrderTotalConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ObservableCollection<OrderDetail> details)
            {
                decimal total = 0;
                foreach (var detail in details)
                {
                    total += detail.UnitPrice * detail.Quantity * (1 - (decimal)detail.Discount);
                }
                return total;
            }
            return 0m;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}