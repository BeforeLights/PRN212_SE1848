using MaiQuocAnhWPF.Data.models;
using MaiQuocAnhWPF.Data.repositories;
using System.Collections.Generic;

namespace MaiQuocAnhWPF.Business
{
    public class CategoryService
    {
        private readonly CategoryRepository _repo = CategoryRepository.Instance;

        public IEnumerable<Category> GetAllCategories() => _repo.GetAll();
        public Category? GetCategoryById(int id) => _repo.GetById(id);
        public void AddCategory(Category category) => _repo.Add(category);
        public void UpdateCategory(Category category) => _repo.Update(category);
        public void DeleteCategory(int id) => _repo.Delete(id);
        public IEnumerable<Category> SearchCategories(string keyword) => _repo.Search(keyword);
    }
}