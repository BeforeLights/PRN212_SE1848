using MaiQuocAnhWPF.Data.models;
using System.Windows;

namespace MaiQuocAnhWPF
{
    public partial class CategoryDialog : Window
    {
        public Category Category { get; set; }

        public CategoryDialog(Category? category = null)
        {
            InitializeComponent();
            
            if (category != null)
            {
                Category = new Category
                {
                    CategoryID = category.CategoryID,
                    CategoryName = category.CategoryName,
                    Description = category.Description
                };
            }
            else
            {
                Category = new Category();
            }
            
            DataContext = Category;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Category.CategoryName))
            {
                MessageBox.Show("Category name is required.", "Validation Error", 
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