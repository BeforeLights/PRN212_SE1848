using MaiQuocAnhWPF.Business;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MaiQuocAnhWPF
{
    public partial class CustomerOrdersControl : UserControl
    {
        private readonly int _customerId;
        private readonly OrderService _orderService = new();

        public CustomerOrdersControl(int customerId)
        {
            InitializeComponent();
            _customerId = customerId;
            LoadCustomerOrders();
        }

        private void LoadCustomerOrders()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"Loading orders for customer ID: {_customerId}");
                
                var customerOrders = _orderService.GetOrdersByCustomer(_customerId);
                var ordersList = customerOrders.ToList();
                
                System.Diagnostics.Debug.WriteLine($"Found {ordersList.Count} orders for customer {_customerId}");
                
                OrdersGrid.ItemsSource = ordersList;
                
                // If no orders found, show a message
                if (!ordersList.Any())
                {
                    MessageBox.Show($"No orders found for customer ID {_customerId}.", "No Orders", 
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading customer orders: {ex.Message}\n\nStack Trace:\n{ex.StackTrace}", 
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}