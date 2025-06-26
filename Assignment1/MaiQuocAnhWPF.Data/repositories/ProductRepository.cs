using MaiQuocAnhWPF.Data.models;
using System.Collections.Generic;
using System.Linq;

namespace MaiQuocAnhWPF.Data.repositories
{
    public class ProductRepository
    {
        private static ProductRepository _instance;
        private readonly List<Product> _products = new();

        private ProductRepository() { }

        public static ProductRepository Instance => _instance ??= new ProductRepository();

        public IEnumerable<Product> GetAll() => _products;
        public Product? GetById(int id) => _products.FirstOrDefault(p => p.ProductID == id);
        public void Add(Product product) => _products.Add(product);
        public void Update(Product product)
        {
            var existing = GetById(product.ProductID);
            if (existing != null)
            {
                existing.ProductName = product.ProductName;
                existing.SupplierID = product.SupplierID;
                existing.CategoryID = product.CategoryID;
                existing.QuantityPerUnit = product.QuantityPerUnit;
                existing.UnitPrice = product.UnitPrice;
                existing.UnitsInStock = product.UnitsInStock;
                existing.UnitsOnOrder = product.UnitsOnOrder;
                existing.ReorderLevel = product.ReorderLevel;
                existing.Discontinued = product.Discontinued;
            }
        }
        public void Delete(int id)
        {
            var product = GetById(id);
            if (product != null) _products.Remove(product);
        }
        public IEnumerable<Product> Search(string keyword) =>
            _products.Where(p => p.ProductName.Contains(keyword, System.StringComparison.OrdinalIgnoreCase));
    }
}