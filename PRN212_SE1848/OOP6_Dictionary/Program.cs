using System.Text;
using OOP6_Dictionary;

Console.OutputEncoding = Encoding.UTF8;

Category category1 = new Category
{
    Id = 1,
    Name = "Điện thoại",
    Products = new Dictionary<int, Product>()
};

Product product1 = new Product
{
    Id = 1,
    Name = "iPhone 14 Pro Max",
    Quantity = 10,
    Price = 30000000
};

category1.AddProduct(product1);

Product product2 = new Product
{
    Id = 2,
    Name = "Samsung Galaxy S23 Ultra",
    Quantity = 5,
    Price = 35000000
};

category1.AddProduct(product2);

Product product3 = new Product
{
    Id = 3,
    Name = "Google Pixel 7 Pro",
    Quantity = 8,
    Price = 25000000
};


Product product4 = new Product
{
    Id = 4,
    Name = "OnePlus 11",
    Quantity = 12,
    Price = 20000000
};

Product product5 = new Product
{
    Id = 5,
    Name = "Xiaomi 13 Pro",
    Quantity = 15,
    Price = 25000000
};

category1.AddProduct(product3);
category1.AddProduct(product4);
category1.AddProduct(product5);
category1.PrintAllProducts();

double minPrice = 20000000;
double maxPrice = 30000000;
Dictionary<int, Product> filteredProducts = category1.FilterProductsByPrice(minPrice, maxPrice);
Console.WriteLine($"\nCác sản phẩm có giá từ {minPrice:C} đến {maxPrice:C}:");
foreach (var kvp in filteredProducts)
{
    Console.WriteLine(kvp.Value);
}

Dictionary<int, Product> sortedProducts = category1.SortProductsByPriceAscending();
Console.WriteLine("\nCác sản phẩm được sắp xếp theo giá tăng dần:");
foreach (KeyValuePair<int, Product> kvp in sortedProducts)
{
    Product product = kvp.Value;
    Console.WriteLine(kvp.Value);
}

Dictionary<int, Product> sortedByPriceAndQuantity = category1.SortProductsByPriceAndQuantity();
Console.WriteLine("\nCác sản phẩm được sắp xếp theo giá tăng dần, nếu trùng nhau thì sắp xếp theo giảm dần số lượng:");
foreach (KeyValuePair<int, Product> kvp in sortedByPriceAndQuantity)
{
    Product product = kvp.Value;
    Console.WriteLine(kvp.Value);
}

product5.Name = "iPhone 13 Pro Max";
product5.Price = 27000000;
product5.Quantity = 15;
bool isUpdated = category1.UpdateProduct(product5);
Console.WriteLine("\nSửa thông tin sản phẩm thành công: " + isUpdated);
Console.WriteLine("Danh sách sản phẩm sau khi sửa:");
category1.PrintAllProducts();

int id = 5;
bool isDeleted = category1.DeleteProduct(id);
if(!isDeleted)
{
    Console.WriteLine($"\nKhông tìm thấy sản phẩm với ID {id} để xóa.");
}
else
{
    Console.WriteLine($"\nSản phẩm với ID {id} đã được xóa thành công.");
}
category1.PrintAllProducts();

category1.DeleteProductsByPriceRange(minPrice, maxPrice);
Console.WriteLine($"\nDanh sách sản phẩm sau khi xóa các sản phẩm có giá từ {minPrice:C} đến {maxPrice:C}:");
category1.PrintAllProducts();

LinkedList<Category> categories = new LinkedList<Category>();
categories.AddLast(category1);

Category category2 = new Category
{
    Id = 2,
    Name = "Máy tính xách tay",
    Products = new Dictionary<int, Product>()
};

category2.AddProduct(new Product
{
    Id = 1,
    Name = "Dell XPS 13",
    Quantity = 8,
    Price = 20000000
});

category2.AddProduct(new Product
{
    Id = 2,
    Name = "MacBook Pro 16",
    Quantity = 5,
    Price = 45000000
});

category2.AddProduct(new Product
{
    Id = 3,
    Name = "Lenovo ThinkPad X1 Carbon",
    Quantity = 10,
    Price = 30000000
});

categories.AddFirst(category2);

Console.WriteLine("\nDanh sách các danh mục sản phẩm:");
foreach (var category in categories)
{
    Console.WriteLine("-------------------------");
    Console.WriteLine(category);
    Console.WriteLine("-------------------------");
    category.PrintAllProducts();
}