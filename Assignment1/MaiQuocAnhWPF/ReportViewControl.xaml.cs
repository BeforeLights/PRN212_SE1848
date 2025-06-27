using MaiQuocAnhWPF.Business;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using Microsoft.Win32;

namespace MaiQuocAnhWPF
{
    public partial class ReportViewControl : UserControl
    {
        private readonly ReportService _reportService = new();

        public ReportViewControl(string reportType)
        {
            InitializeComponent();
            ReportTitle.Text = reportType;
            
            // Set default dates
            EndDatePicker.SelectedDate = DateTime.Today;
            StartDatePicker.SelectedDate = DateTime.Today.AddDays(-30); // Last 30 days
            
            // Generate initial report
            GenerateReport_Click(null, null);
        }

        private void GenerateReport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var startDate = StartDatePicker.SelectedDate ?? DateTime.Today.AddDays(-30);
                var endDate = EndDatePicker.SelectedDate ?? DateTime.Today;
                var reportType = ((ComboBoxItem)ReportTypeComboBox.SelectedItem).Content.ToString();
                var period = ((ComboBoxItem)PeriodComboBox.SelectedItem).Content.ToString();

                switch (reportType)
                {
                    case "Order Statistics":
                        GenerateOrderStatistics(startDate, endDate, period);
                        break;
                    case "Customer Analysis":
                        GenerateCustomerAnalysis(startDate, endDate);
                        break;
                    case "Product Performance":
                        GenerateProductPerformance(startDate, endDate);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating report: {ex.Message}", "Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void GenerateOrderStatistics(DateTime startDate, DateTime endDate, string period)
        {
            var statistics = period switch
            {
                "Daily" => _reportService.GetOrderStatisticsByDay(startDate, endDate),
                "Weekly" => _reportService.GetOrderStatisticsByWeek(startDate, endDate),
                "Monthly" => _reportService.GetOrderStatisticsByMonth(startDate, endDate),
                _ => _reportService.GetOrderStatisticsByDay(startDate, endDate)
            };

            // Update DataGrid
            ReportDataGrid.ItemsSource = statistics.ToList();

            // Update Summary
            GenerateOrderSummary(statistics);
        }

        private void GenerateCustomerAnalysis(DateTime startDate, DateTime endDate)
        {
            var customerStats = _reportService.GetCustomerStatistics(startDate, endDate);
            
            // Update DataGrid
            ReportDataGrid.ItemsSource = customerStats.ToList();
            
            // Update Summary
            GenerateCustomerSummary(customerStats);
        }

        private void GenerateProductPerformance(DateTime startDate, DateTime endDate)
        {
            var productStats = _reportService.GetProductStatistics(startDate, endDate);
            
            // Update DataGrid
            ReportDataGrid.ItemsSource = productStats.ToList();
            
            // Update Summary
            GenerateProductSummary(productStats);
        }

        private void GenerateOrderSummary(System.Collections.Generic.IEnumerable<OrderStatistic> statistics)
        {
            ClearSummaryPanels();

            var statsList = statistics.ToList();
            if (!statsList.Any())
            {
                DetailsPanel.Children.Add(new TextBlock 
                { 
                    Text = "No data available for the selected period.", 
                    FontSize = 16, 
                    Margin = new Thickness(0, 10, 0, 10) 
                });
                return;
            }

            // Overall totals
            var totalOrders = statsList.Sum(s => s.OrderCount);
            var totalRevenue = statsList.Sum(s => s.TotalRevenue);
            var avgOrderValue = statsList.Average(s => s.AverageOrderValue);
            var totalCustomers = statsList.Sum(s => s.UniqueCustomers);

            // Summary cards in horizontal line
            AddSummaryCard("Total Orders", totalOrders.ToString("N0"), "#FF4CAF50");
            AddSummaryCard("Total Revenue", totalRevenue.ToString("C"), "#FF2196F3");
            AddSummaryCard("Average Order Value", avgOrderValue.ToString("C"), "#FFFF9800");
            AddSummaryCard("Total Customers", totalCustomers.ToString("N0"), "#FF9C27B0");

            // Top performers in details section
            var topPeriod = statsList.OrderByDescending(s => s.TotalRevenue).FirstOrDefault();
            if (topPeriod != null)
            {
                AddDetailsSection("Top Performing Period", 
                    $"Period: {topPeriod.Period}\n" +
                    $"Revenue: {topPeriod.TotalRevenue:C}\n" +
                    $"Orders: {topPeriod.OrderCount}\n" +
                    $"Top Employee: {topPeriod.TopEmployee}");
            }
        }

        private void GenerateCustomerSummary(System.Collections.Generic.IEnumerable<CustomerStatistic> statistics)
        {
            ClearSummaryPanels();

            var statsList = statistics.ToList();
            if (!statsList.Any())
            {
                DetailsPanel.Children.Add(new TextBlock 
                { 
                    Text = "No customer data available for the selected period.", 
                    FontSize = 16, 
                    Margin = new Thickness(0, 10, 0, 10) 
                });
                return;
            }

            var totalCustomers = statsList.Count;
            var totalRevenue = statsList.Sum(s => s.TotalSpent);
            var avgCustomerValue = statsList.Average(s => s.TotalSpent);
            var topCustomer = statsList.FirstOrDefault();

            // Summary cards in horizontal line
            AddSummaryCard("Total Customers", totalCustomers.ToString("N0"), "#FF4CAF50");
            AddSummaryCard("Total Customer Revenue", totalRevenue.ToString("C"), "#FF2196F3");
            AddSummaryCard("Average Customer Value", avgCustomerValue.ToString("C"), "#FFFF9800");
            AddSummaryCard("Top Customer Spent", topCustomer?.TotalSpent.ToString("C") ?? "$0", "#FF9C27B0");

            if (topCustomer != null)
            {
                AddDetailsSection("Top Customer Details", 
                    $"Customer: {topCustomer.CustomerName}\n" +
                    $"Total Spent: {topCustomer.TotalSpent:C}\n" +
                    $"Orders: {topCustomer.OrderCount}\n" +
                    $"Avg Order: {topCustomer.AverageOrderValue:C}");
            }
        }

        private void GenerateProductSummary(System.Collections.Generic.IEnumerable<ProductStatistic> statistics)
        {
            ClearSummaryPanels();

            var statsList = statistics.ToList();
            if (!statsList.Any())
            {
                DetailsPanel.Children.Add(new TextBlock 
                { 
                    Text = "No product data available for the selected period.", 
                    FontSize = 16, 
                    Margin = new Thickness(0, 10, 0, 10) 
                });
                return;
            }

            var totalProducts = statsList.Count;
            var totalQuantitySold = statsList.Sum(s => s.QuantitySold);
            var totalRevenue = statsList.Sum(s => s.TotalRevenue);
            var topProduct = statsList.FirstOrDefault();

            // Summary cards in horizontal line
            AddSummaryCard("Products Sold", totalProducts.ToString("N0"), "#FF4CAF50");
            AddSummaryCard("Total Quantity", totalQuantitySold.ToString("N0"), "#FF2196F3");
            AddSummaryCard("Total Revenue", totalRevenue.ToString("C"), "#FFFF9800");
            AddSummaryCard("Top Product Revenue", topProduct?.TotalRevenue.ToString("C") ?? "$0", "#FF9C27B0");

            if (topProduct != null)
            {
                AddDetailsSection("Top Product Details", 
                    $"Product: {topProduct.ProductName}\n" +
                    $"Revenue: {topProduct.TotalRevenue:C}\n" +
                    $"Quantity Sold: {topProduct.QuantitySold}\n" +
                    $"Orders: {topProduct.OrderCount}");
            }
        }

        private void ClearSummaryPanels()
        {
            SummaryCardsPanel.Children.Clear();
            DetailsPanel.Children.Clear();
        }

        private void AddSummaryCard(string title, string value, string color)
        {
            var border = new Border
            {
                Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(color)),
                CornerRadius = new CornerRadius(8),
                Padding = new Thickness(20),
                Margin = new Thickness(5, 5, 5, 5),
                Width = 180,
                Height = 80,
                HorizontalAlignment = HorizontalAlignment.Left
            };

            var stackPanel = new StackPanel
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            
            var titleBlock = new TextBlock
            {
                Text = title,
                Foreground = Brushes.White,
                FontWeight = FontWeights.SemiBold,
                FontSize = 11,
                TextAlignment = TextAlignment.Center,
                TextWrapping = TextWrapping.Wrap
            };

            var valueBlock = new TextBlock
            {
                Text = value,
                Foreground = Brushes.White,
                FontWeight = FontWeights.Bold,
                FontSize = 16,
                TextAlignment = TextAlignment.Center,
                Margin = new Thickness(0, 3, 0, 0)
            };

            stackPanel.Children.Add(titleBlock);
            stackPanel.Children.Add(valueBlock);
            border.Child = stackPanel;

            // Add to horizontal cards panel
            SummaryCardsPanel.Children.Add(border);
        }

        private void AddDetailsSection(string title, string content)
        {
            var titleBlock = new TextBlock
            {
                Text = title,
                FontSize = 16,
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(0, 15, 0, 5)
            };

            var contentBlock = new TextBlock
            {
                Text = content,
                FontSize = 14,
                Margin = new Thickness(10, 0, 0, 10),
                TextWrapping = TextWrapping.Wrap
            };

            DetailsPanel.Children.Add(titleBlock);
            DetailsPanel.Children.Add(contentBlock);
        }

        private void ExportReport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var saveDialog = new SaveFileDialog
                {
                    Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*",
                    DefaultExt = "csv",
                    FileName = $"SalesReport_{DateTime.Now:yyyyMMdd_HHmmss}.csv"
                };

                if (saveDialog.ShowDialog() == true)
                {
                    ExportToCsv(saveDialog.FileName);
                    MessageBox.Show($"Report exported successfully to {saveDialog.FileName}", "Export Complete", 
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting report: {ex.Message}", "Export Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ExportToCsv(string fileName)
        {
            var csv = new StringBuilder();
            
            // Add headers
            if (ReportDataGrid.ItemsSource != null)
            {
                var items = ReportDataGrid.ItemsSource.Cast<object>().ToList();
                if (items.Any())
                {
                    var properties = items.First().GetType().GetProperties();
                    csv.AppendLine(string.Join(",", properties.Select(p => p.Name)));

                    // Add data rows
                    foreach (var item in items)
                    {
                        var values = properties.Select(p => p.GetValue(item)?.ToString() ?? "").ToArray();
                        csv.AppendLine(string.Join(",", values.Select(v => $"\"{v}\"")));
                    }
                }
            }

            File.WriteAllText(fileName, csv.ToString());
        }

        private void PrintReport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var printDialog = new PrintDialog();
                if (printDialog.ShowDialog() == true)
                {
                    // Create a simple print document
                    var flowDoc = new FlowDocument();
                    var paragraph = new Paragraph();
                    paragraph.Inlines.Add(new Run($"Sales Report - Generated on {DateTime.Now}\n\n"));
                    
                    // Add summary information
                    foreach (UIElement element in DetailsPanel.Children)
                    {
                        if (element is TextBlock textBlock)
                        {
                            paragraph.Inlines.Add(new Run(textBlock.Text + "\n"));
                        }
                    }
                    
                    flowDoc.Blocks.Add(paragraph);
                    
                    printDialog.PrintDocument(((IDocumentPaginatorSource)flowDoc).DocumentPaginator, "Sales Report");
                    MessageBox.Show("Report sent to printer successfully!", "Print Complete", 
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error printing report: {ex.Message}", "Print Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}