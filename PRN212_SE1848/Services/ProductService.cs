using BusinessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductService : IProductService
    {
        IProductRepository iRepository;
        public ProductService()
        {
            iRepository = new ProductRepository();
        }

        public bool DeleteProduct(int id)
        {
            return iRepository.DeleteProduct(id);
        }

        public bool DeleteProduct(Product product)
        {
            return iRepository.DeleteProduct(product);
        }

        public void GenerateSampleDataset()
        {
            iRepository.GenerateSampleDataset();
        }

        public Product GetProduct(int id)
        {
            return iRepository.GetProduct(id);
        }

        public List<Product> GetProducts()
        {
            return iRepository.GetProducts();
        }

        public bool SaveProduct(Product product)
        {
            return iRepository.SaveProduct(product);
        }

        public bool UpdateProduct(Product product)
        {
            return iRepository.UpdateProduct(product);
        }
    }
}
