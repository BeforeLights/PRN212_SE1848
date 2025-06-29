using BusinessObjects;
using DataAccessLayer;
using Repositories.Interface;

namespace Repositories.Implementation
{
    public class ProductRepository : IProductRepository
    {
        public void DeleteProduct(Product p) => ProductDAO.DeleteProduct(p);
        public Product? GetProductById(int id) => ProductDAO.GetProductById(id);
        public List<Product> GetProducts() => ProductDAO.GetProducts();
        public void SaveProduct(Product p) => ProductDAO.SaveProduct(p);
        public void UpdateProduct(Product p) => ProductDAO.UpdateProduct(p);
    }
}
