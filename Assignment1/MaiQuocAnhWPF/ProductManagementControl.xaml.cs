using MaiQuocAnhWPF.Data.models;
using MaiQuocAnhWPF.Business;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MaiQuocAnhWPF
{
    public partial class ProductManagementControl : UserControl
    {
        private readonly ProductService _service = new();
        public ObservableCollection<Product> Products { get; set; }

        public ProductManagementControl()
        {
            InitializeComponent();
            LoadProducts();
        }

        private void LoadProducts()
        {
            Products = new ObservableCollection<Product>(_service.GetAllProducts());
            ProductGrid.ItemsSource = Products;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new ProductDialog();
            if (dialog.ShowDialog() == true)
            {
                _service.AddProduct(dialog.Product);
                LoadProducts(); // Refresh the grid
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
                if (ProductGrid.SelectedItem is not Product selected) 
            {
                MessageBox.Show("Please select a product to edit.", "No Selection", 
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            
            var dialog = new ProductDialog(selected);
            if (dialog.ShowDialog() == true)
            {
                _service.UpdateProduct(dialog.Product);
                LoadProducts(); // Refresh the grid
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (ProductGrid.SelectedItem is not Product selected)
            {
                MessageBox.Show("Please select a product to delete.", "No Selection", 
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            
            var result = MessageBox.Show(
                $"Are you sure you want to delete the product '{selected.ProductName}'?", 
                "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);
                
            if (result == MessageBoxResult.Yes)
            {
                _service.DeleteProduct(selected.ProductID);
                LoadProducts(); // Refresh the grid
            }
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            var keyword = SearchTextBox.Text.Trim();
            if (string.IsNullOrEmpty(keyword) || keyword == "Search products...")
            {
                LoadProducts();
            }
            else
            {
                var filteredProducts = _service.SearchProducts(keyword);
                Products = new ObservableCollection<Product>(filteredProducts);
                ProductGrid.ItemsSource = Products;
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Text = "Search products...";
            LoadProducts();
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            LoadProducts();
        }
    }
}