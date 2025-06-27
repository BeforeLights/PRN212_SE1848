using System.Windows;
using MaiQuocAnhWPF.Data.models;

namespace MaiQuocAnhWPF
{
    public partial class ProductDialog : Window
    {
        public Product Product { get; set; }

        public ProductDialog(Product? product = null)
        {
            InitializeComponent();
            
            if (product != null)
            {
                Product = new Product
                {
                    ProductID = product.ProductID,
                    ProductName = product.ProductName,
                    SupplierID = product.SupplierID,
                    CategoryID = product.CategoryID,
                    QuantityPerUnit = product.QuantityPerUnit,
                    UnitPrice = product.UnitPrice,
                    UnitsInStock = product.UnitsInStock,
                    UnitsOnOrder = product.UnitsOnOrder,
                    ReorderLevel = product.ReorderLevel,
                    Discontinued = product.Discontinued
                };
            }
            else
            {
                Product = new Product
                {
                    ProductName = "",
                    QuantityPerUnit = "",
                    UnitPrice = 0,
                    UnitsInStock = 0,
                    UnitsOnOrder = 0,
                    ReorderLevel = 0,
                    Discontinued = false
                };
            }
            
            DataContext = Product;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            // Validation
            if (string.IsNullOrWhiteSpace(Product.ProductName))
            {
                MessageBox.Show("Product name is required.", "Validation Error", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            
            if (Product.UnitPrice < 0)
            {
                MessageBox.Show("Unit price cannot be negative.", "Validation Error", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            
            if (Product.UnitsInStock < 0)
            {
                MessageBox.Show("Units in stock cannot be negative.", "Validation Error", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
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
}