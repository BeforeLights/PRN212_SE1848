using MaiQuocAnhWPF.Data.models;
using MaiQuocAnhWPF.Data.repositories;
using System.Collections.Generic;

namespace MaiQuocAnhWPF.Business
{
    public class ProductService
    {
        private readonly ProductRepository _repo = ProductRepository.Instance;

        public IEnumerable<Product> GetAllProducts() => _repo.GetAll();
        public Product? GetProductById(int id) => _repo.GetById(id);
        public void AddProduct(Product product) => _repo.Add(product);
        public void UpdateProduct(Product product) => _repo.Update(product);
        public void DeleteProduct(int id) => _repo.Delete(id);
        public IEnumerable<Product> SearchProducts(string keyword) => _repo.Search(keyword);
    }
}