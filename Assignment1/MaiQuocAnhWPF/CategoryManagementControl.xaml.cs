using MaiQuocAnhWPF.Business;
using MaiQuocAnhWPF.Data.models;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MaiQuocAnhWPF
{
    public partial class CategoryManagementControl : UserControl
    {
        private readonly CategoryService _service = new();

        public CategoryManagementControl()
        {
            InitializeComponent();
            LoadCategories();
        }

        private void LoadCategories()
        {
            CategoryGrid.ItemsSource = _service.GetAllCategories().ToList();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            var keyword = SearchTextBox.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                LoadCategories();
            }
            else
            {
                CategoryGrid.ItemsSource = _service.SearchCategories(keyword).ToList();
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Text = "";
            LoadCategories();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new CategoryDialog();
            if (dialog.ShowDialog() == true)
            {
                _service.AddCategory(dialog.Category);
                LoadCategories();
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (CategoryGrid.SelectedItem is not Category selected) return;
            
            var dialog = new CategoryDialog(selected);
            if (dialog.ShowDialog() == true)
            {
                _service.UpdateCategory(dialog.Category);
                LoadCategories();
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (CategoryGrid.SelectedItem is not Category selected) return;
            
            var result = MessageBox.Show($"Are you sure you want to delete category '{selected.CategoryName}'?", 
                                       "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                _service.DeleteCategory(selected.CategoryID);
                LoadCategories();
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            LoadCategories();
        }
    }
}