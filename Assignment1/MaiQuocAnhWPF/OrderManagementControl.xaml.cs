using MaiQuocAnhWPF.Data.models;
using MaiQuocAnhWPF.Business;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MaiQuocAnhWPF
{
    public partial class OrderManagementControl : UserControl
    {
        private readonly OrderService _service = new();
        public ObservableCollection<Order> Orders { get; set; }

        public OrderManagementControl()
        {
            InitializeComponent();
            LoadOrders();
        }

        private void LoadOrders()
        {
            Orders = new ObservableCollection<Order>(_service.GetAllOrders());
            OrderGrid.ItemsSource = Orders;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OrderDialog();
            if (dialog.ShowDialog() == true)
            {
                _service.AddOrder(dialog.Order);
                LoadOrders(); // Refresh the grid
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (OrderGrid.SelectedItem is not Order selected) return;
            var dialog = new OrderDialog(selected);
            if (dialog.ShowDialog() == true)
            {
                _service.UpdateOrder(dialog.Order);
                LoadOrders(); // Refresh the grid
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (OrderGrid.SelectedItem is not Order selected) return;
            var result = MessageBox.Show($"Are you sure you want to delete order #{selected.OrderID}?", 
                                       "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                _service.DeleteOrder(selected.OrderID);
                LoadOrders(); // Refresh the grid
            }
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            var keyword = SearchTextBox.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                LoadOrders();
            }
            else
            {
                var filteredOrders = _service.SearchOrders(keyword);
                Orders = new ObservableCollection<Order>(filteredOrders);
                OrderGrid.ItemsSource = Orders;
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Text = "";
            LoadOrders();
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            LoadOrders();
        }
    }
}