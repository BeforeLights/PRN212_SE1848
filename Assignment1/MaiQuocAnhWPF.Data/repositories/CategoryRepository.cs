using MaiQuocAnhWPF.Data.models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MaiQuocAnhWPF.Data.repositories
{
    public class CategoryRepository : IRepository<Category>
    {
        private static CategoryRepository? _instance;
        private static readonly object _lock = new object();
        private readonly List<Category> _categories = new();

        private CategoryRepository() { }

        public static CategoryRepository Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        _instance ??= new CategoryRepository();
                    }
                }
                return _instance;
            }
        }

        public IEnumerable<Category> GetAll() => _categories.ToList();

        public Category? GetById(int id) => _categories.FirstOrDefault(c => c.CategoryID == id);

        public void Add(Category category)
        {
            if (category.CategoryID == 0)
                category.CategoryID = _categories.Count > 0 ? _categories.Max(c => c.CategoryID) + 1 : 1;
            _categories.Add(category);
        }

        public void Update(Category category)
        {
            var existing = GetById(category.CategoryID);
            if (existing != null)
            {
                existing.CategoryName = category.CategoryName;
                existing.Description = category.Description;
            }
        }

        public void Delete(int id)
        {
            var category = GetById(id);
            if (category != null)
                _categories.Remove(category);
        }

        public IEnumerable<Category> Search(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return GetAll();

            return _categories.Where(c => 
                c.CategoryName.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                c.Description.Contains(keyword, StringComparison.OrdinalIgnoreCase));
        }
    }
}