using MaiQuocAnhWPF.Business;
using MaiQuocAnhWPF.Data.models;
using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace MaiQuocAnhWPF
{
    public partial class CustomerOrdersWindow : Window
    {
        private readonly Customer _customer;
        private readonly OrderService _orderService = new();
        private readonly ProductService _productService = new();

        public CustomerOrdersWindow(Customer customer)
        {
            InitializeComponent();
            _customer = customer;
            LoadCustomerOrders();
        }

        private void LoadCustomerOrders()
        {
            try
            {
                // Set customer info
                CustomerNameText.Text = $"Orders for {_customer.CompanyName}";
                CustomerInfoText.Text = $"Contact: {_customer.ContactName} ({_customer.ContactTitle}) | Phone: {_customer.Phone}";

                // Get customer orders
                var orders = _orderService.GetOrdersByCustomer(_customer.CustomerID).ToList();
                OrdersGrid.ItemsSource = orders;

                // Calculate summary statistics
                if (orders.Any())
                {
                    var totalOrders = orders.Count;
                    var totalSpent = orders.Sum(o => o.OrderDetails.Sum(od => od.UnitPrice * od.Quantity * (1 - (decimal)od.Discount)));
                    var averageOrder = totalSpent / totalOrders;
                    var lastOrder = orders.OrderByDescending(o => o.OrderDate).First().OrderDate;

                    TotalOrdersText.Text = totalOrders.ToString();
                    TotalSpentText.Text = totalSpent.ToString("C");
                    AverageOrderText.Text = averageOrder.ToString("C");
                    LastOrderText.Text = lastOrder.ToString("MM/dd/yyyy");
                }
                else
                {
                    TotalOrdersText.Text = "0";
                    TotalSpentText.Text = "$0.00";
                    AverageOrderText.Text = "$0.00";
                    LastOrderText.Text = "No orders";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading customer orders: {ex.Message}", "Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OrdersGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (OrdersGrid.SelectedItem is Order selectedOrder)
            {
                SelectedOrderText.Text = $"Order #{selectedOrder.OrderID} - {selectedOrder.OrderDate:yyyy-MM-dd}";
                OrderDetailsGrid.ItemsSource = selectedOrder.OrderDetails;
            }
            else
            {
                SelectedOrderText.Text = "Select an order to view details";
                OrderDetailsGrid.ItemsSource = null;
            }
        }

        private void PrintOrders_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var orders = _orderService.GetOrdersByCustomer(_customer.CustomerID).ToList();
                if (!orders.Any())
                {
                    MessageBox.Show("No orders to print.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                var printContent = GeneratePrintContent(orders);
                
                // Simple print dialog
                var printDialog = new System.Windows.Controls.PrintDialog();
                if (printDialog.ShowDialog() == true)
                {
                    var flowDoc = new System.Windows.Documents.FlowDocument();
                    var paragraph = new System.Windows.Documents.Paragraph();
                    paragraph.Inlines.Add(new System.Windows.Documents.Run(printContent));
                    flowDoc.Blocks.Add(paragraph);
                    
                    printDialog.PrintDocument(((System.Windows.Documents.IDocumentPaginatorSource)flowDoc).DocumentPaginator, 
                        $"Orders for {_customer.CompanyName}");
                    
                    MessageBox.Show("Orders sent to printer successfully!", "Print Complete", 
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error printing orders: {ex.Message}", "Print Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ExportOrders_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var orders = _orderService.GetOrdersByCustomer(_customer.CustomerID).ToList();
                if (!orders.Any())
                {
                    MessageBox.Show("No orders to export.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                var saveDialog = new SaveFileDialog
                {
                    Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*",
                    DefaultExt = "csv",
                    FileName = $"Orders_{_customer.CompanyName.Replace(" ", "_")}_{DateTime.Now:yyyyMMdd}.csv"
                };

                if (saveDialog.ShowDialog() == true)
                {
                    var csv = GenerateCSVContent(orders);
                    File.WriteAllText(saveDialog.FileName, csv);
                    MessageBox.Show($"Orders exported successfully to {saveDialog.FileName}", "Export Complete", 
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting orders: {ex.Message}", "Export Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private string GeneratePrintContent(System.Collections.Generic.List<Order> orders)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"CUSTOMER ORDERS REPORT");
            sb.AppendLine($"Customer: {_customer.CompanyName}");
            sb.AppendLine($"Contact: {_customer.ContactName} ({_customer.ContactTitle})");
            sb.AppendLine($"Phone: {_customer.Phone}");
            sb.AppendLine($"Generated: {DateTime.Now:yyyy-MM-dd HH:mm}");
            sb.AppendLine();
            sb.AppendLine(new string('=', 80));
            sb.AppendLine();

            foreach (var order in orders.OrderByDescending(o => o.OrderDate))
            {
                sb.AppendLine($"Order #{order.OrderID} - {order.OrderDate:yyyy-MM-dd}");
                sb.AppendLine(new string('-', 40));
                
                foreach (var detail in order.OrderDetails)
                {
                    var product = _productService.GetProductById(detail.ProductID);
                    var lineTotal = detail.UnitPrice * detail.Quantity * (1 - (decimal)detail.Discount);
                    sb.AppendLine($"  {product?.ProductName ?? "Unknown Product"} x{detail.Quantity} @ {detail.UnitPrice:C} = {lineTotal:C}");
                }
                
                var orderTotal = order.OrderDetails.Sum(od => od.UnitPrice * od.Quantity * (1 - (decimal)od.Discount));
                sb.AppendLine($"  Order Total: {orderTotal:C}");
                sb.AppendLine();
            }

            return sb.ToString();
        }

        private string GenerateCSVContent(System.Collections.Generic.List<Order> orders)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Order ID,Order Date,Product Name,Unit Price,Quantity,Discount,Line Total");

            foreach (var order in orders.OrderByDescending(o => o.OrderDate))
            {
                foreach (var detail in order.OrderDetails)
                {
                    var product = _productService.GetProductById(detail.ProductID);
                    var lineTotal = detail.UnitPrice * detail.Quantity * (1 - (decimal)detail.Discount);
                    sb.AppendLine($"{order.OrderID},{order.OrderDate:yyyy-MM-dd},\"{product?.ProductName ?? "Unknown"}\",{detail.UnitPrice},{detail.Quantity},{detail.Discount},{lineTotal}");
                }
            }

            return sb.ToString();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}