using MaiQuocAnhWPF.Data.models;
using System.Collections.Generic;
using System.Linq;

namespace MaiQuocAnhWPF.Data.repositories
{
    public class ProductRepository : IRepository<Product>
    {
        private static ProductRepository? _instance;
        private static readonly object _lock = new object();
        private readonly List<Product> _products = new();

        private ProductRepository() { }

        public static ProductRepository Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        _instance ??= new ProductRepository();
                    }
                }
                return _instance;
            }
        }

        public IEnumerable<Product> GetAll() => _products.ToList();

        public Product? GetById(int id) => _products.FirstOrDefault(p => p.ProductID == id);

        public void Add(Product product)
        {
            if (product.ProductID == 0)
                product.ProductID = _products.Count > 0 ? _products.Max(p => p.ProductID) + 1 : 1;
            _products.Add(product);
        }

        public void Update(Product product)
        {
            var existing = GetById(product.ProductID);
            if (existing != null)
            {
                existing.ProductName = product.ProductName;
                existing.CategoryID = product.CategoryID;
                existing.UnitPrice = product.UnitPrice;
                existing.UnitsInStock = product.UnitsInStock;
                existing.UnitsOnOrder = product.UnitsOnOrder;
                existing.ReorderLevel = product.ReorderLevel;
                existing.QuantityPerUnit = product.QuantityPerUnit;
                existing.Discontinued = product.Discontinued;
            }
        }

        public void Delete(int id)
        {
            var product = GetById(id);
            if (product != null)
                _products.Remove(product);
        }

        public IEnumerable<Product> Search(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return GetAll();

            return _products.Where(p => 
                p.ProductName.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                p.QuantityPerUnit.Contains(keyword, StringComparison.OrdinalIgnoreCase));
        }
    }
}