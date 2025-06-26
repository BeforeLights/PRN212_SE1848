using BusinessObjects;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class ProductRepository : IProductRepository
    {
        ProductDAO productDAO=new ProductDAO();

        public bool DeleteProduct(int id)
        {
            return productDAO.DeleteProduct(id);
        }

        public bool DeleteProduct(Product product)
        {
            return productDAO.DeleteProduct(product);
        }

        public void GenerateSampleDataset()
        {
            productDAO.GenerateSampleDataset();
        }

        public Product GetProduct(int id)
        {
            return productDAO.GetProduct(id);
        }

        public List<Product> GetProducts()
        {
           return productDAO.GetProducts();
        }

        public bool SaveProduct(Product product)
        {
            return productDAO.SaveProduct(product);
        }

        public bool UpdateProduct(Product product)
        {
            return productDAO.UpdateProduct(product);
        }
    }
}
