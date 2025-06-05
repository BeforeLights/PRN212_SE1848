using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP6_Dictionary
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Dictionary<int, Product> Products { get; set; }
        public override string ToString()
        {
            return $"Id: {Id}\tName: {Name}\tProducts Count: {Products.Count}";
        }

        public Category()
        {
            Products = new Dictionary<int, Product>();
        }
        /* Khi quản lý mọi đối tượng ta đều phải đáp ứng
         * đầy đủ tính năng CRUD (Create, Read, Update, Delete)
         */

        public void AddProduct(Product product)
        {
            if (product == null)
            {
                return; // Không thêm sản phẩm null
            }
            if (Products.ContainsKey(product.Id))
            {
                return; // Không thêm sản phẩm đã tồn tại
            }

            // Thêm sản phẩm vào danh sách
            Products.Add(product.Id, product);
        }

        // Xuất danh sách sản phẩm
        public void PrintAllProducts()
        {
            foreach (KeyValuePair<int, Product> kvp in Products)
            {
                Product product = kvp.Value;
                Console.WriteLine(kvp.Value);
            }
        }

        // Lọc các sản phẩm có giá từ minPrice đến maxPrice
        public Dictionary<int, Product> FilterProductsByPrice(double minPrice, double maxPrice)
        {
            return Products.Where(kvp => kvp.Value.Price >= minPrice && kvp.Value.Price <= maxPrice)
                           .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }

        //Sắp xếp sản phẩm theo giá tăng dần
        public Dictionary<int, Product> SortProductsByPriceAscending()
        {
            return Products.OrderBy(kvp => kvp.Value.Price)
                           .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }

        // Sắp xếp sản phẩm theo giá tăng dần, nếu trùng nhau thì sắp xếp theo giảm dần số lượng
        public Dictionary<int, Product> SortProductsByPriceAndQuantity()
        {
            return Products.OrderBy(kvp => kvp.Value.Price)
                          .ThenByDescending(kvp => kvp.Value.Quantity)
                          .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }

        // Sửa thông tin sản phẩm
        public bool UpdateProduct(Product product)
        {
            if (product == null || !Products.ContainsKey(product.Id))
            {
                return false; // Không sửa sản phẩm null hoặc không tồn tại
            }
            // Cập nhật thông tin sản phẩm
            Products[product.Id] = product;
            return true;
        }

        // Xóa sản phẩm
        public bool DeleteProduct(int productId)
        {
            if (!Products.ContainsKey(productId))
            {
                return false; // Không xóa sản phẩm không tồn tại
            }
            // Xóa sản phẩm khỏi danh sách
            Products.Remove(productId);
            return true;
        }

        // Viết hàm xóa các sản phẩm có mức giá từ A - B
        public void DeleteProductsByPriceRange(double priceA, double priceB)
        {
            var productsToRemove = Products.Where(kvp => kvp.Value.Price >= priceA && kvp.Value.Price <= priceB)
                                           .Select(kvp => kvp.Key)
                                           .ToList();
            foreach (var id in productsToRemove)
            {
                Products.Remove(id);
            }
        }
    }
}
