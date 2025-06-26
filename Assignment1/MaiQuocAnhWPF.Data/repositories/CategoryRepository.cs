using MaiQuocAnhWPF.Data.models;
using System.Collections.Generic;
using System.Linq;

namespace MaiQuocAnhWPF.Data.repositories
{
    public class CategoryRepository
    {
        private static CategoryRepository _instance;
        private readonly List<Category> _categories = new();

        private CategoryRepository() { }

        public static CategoryRepository Instance => _instance ??= new CategoryRepository();

        public IEnumerable<Category> GetAll() => _categories;
        public Category? GetById(int id) => _categories.FirstOrDefault(c => c.CategoryID == id);
        public void Add(Category category) => _categories.Add(category);
        public void Update(Category category)
        {
            var existing = GetById(category.CategoryID);
            if (existing != null)
            {
                existing.CategoryName = category.CategoryName;
                existing.Description = category.Description;
                existing.Picture = category.Picture;
            }
        }
        public void Delete(int id)
        {
            var category = GetById(id);
            if (category != null) _categories.Remove(category);
        }
        public IEnumerable<Category> Search(string keyword) =>
            _categories.Where(c => c.CategoryName.Contains(keyword, System.StringComparison.OrdinalIgnoreCase));
    }
}