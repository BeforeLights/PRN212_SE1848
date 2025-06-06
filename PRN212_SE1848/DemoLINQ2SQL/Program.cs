using DemoLINQ2SQL;
using System.Text;

Console.OutputEncoding = System.Text.Encoding.UTF8;
string connectionString = @"server=DESKTOP-2L7O5BN\SQLEXPRESS;database = MyStore;uid=sa;pwd=12345";
MyStoreDataContext context = new MyStoreDataContext(connectionString);

// Câu 1: Truy vấn toàn bộ danh mục
var dsdm = context.Categories;
Console.WriteLine("--- Danh sách danh mục ---");
foreach (var category in dsdm)
{
    Console.WriteLine($"Category ID: {category.CategoryID}, Name: {category.CategoryName}");
}

Console.WriteLine("-----------------------------------\n");
// Câu 2: Lấy thông tin chi tiết danh mục khi biết mã
int madm = 7;
Category cate = context.Categories.FirstOrDefault(c => c.CategoryID == madm);
if (cate != null)
{
    Console.WriteLine($"Chi tiết danh mục ID {madm}: Tên: {cate.CategoryName}");
}
else
{
    Console.WriteLine($"Không tìm thấy danh mục với ID {madm}.");
    Console.WriteLine(cate.CategoryID + "\t" + cate.CategoryName);
}

// Câu 3: Dùng Query Syntax để truy vấn toàn bộ sản phẩm
var dssp= from p in context.Products
          select p;

Console.WriteLine("-----------------------------------\n");
Console.WriteLine("--- Danh sách sản phẩm ---");
foreach (var product in dssp)
{
    Console.WriteLine($"Product ID: {product.ProductID}, Name: {product.ProductName}, Price: {product.UnitPrice}");
}

Console.WriteLine("------------------------------------\n");
// Câu 4: Dùng Query Syntax và Anonymous type để lọc ra các sản phẩm
// nhưng chỉ lấy mã sản phẩm và đơn giá
// sắp xếp giảm dần
var dssp4 = from p in context.Products
          orderby p.UnitPrice descending
          select new { p.ProductID, p.UnitPrice };

Console.WriteLine("--- Danh sách sản phẩm (chỉ mã và đơn giá, sắp xếp giảm dần) ---");
foreach (var product in dssp4)
{
    Console.WriteLine($"Product ID: {product.ProductID}, Price: {product.UnitPrice}");
}

Console.WriteLine("-------------------------------------\n");

// Câu 5: Sửa câu 4 theo extension method (Method syntax)
var dssp5 = context.Products
    .OrderByDescending(p => p.UnitPrice)
    .Select(p => new { p.ProductID, p.UnitPrice });

Console.WriteLine("--- Danh sách sản phẩm (chỉ mã và đơn giá, sắp xếp giảm dần) ---");
foreach (var product in dssp5)
{
    Console.WriteLine($"Product ID: {product.ProductID}, Price: {product.UnitPrice}");
}

Console.WriteLine("-------------------------------------\n");

// Câu 6: Lọc ra top 3 sản phẩm có giá lớn nhất hệ thống (method syntax)
var top3Products = context.Products
    .OrderByDescending(p => p.UnitPrice)
    .Take(3)
    .Select(p => new { p.ProductID, p.ProductName, p.UnitPrice });

Console.WriteLine("--- Danh sách sản phẩm ---");
foreach (var product in top3Products)
{
    Console.WriteLine($"Product ID: {product.ProductID}, Price: {product.UnitPrice}");
}

Console.WriteLine("--------------------------------------\n");

// Câu 7 sửa tên danh mục khi biết mã
int madm_edit = 8;
Category cate_edit = context.Categories.FirstOrDefault(c => c.CategoryID == madm_edit);
if (cate_edit != null)
{
    cate_edit.CategoryName = "Danh mục mới";
    context.SubmitChanges(); // Xác nhận lưu thay đổi vào cơ sở dữ liệu
    Console.WriteLine($"Đã sửa tên danh mục ID {madm_edit} thành '{cate_edit.CategoryName}'.");
}
else
{
    Console.WriteLine($"Không tìm thấy danh mục với ID {madm_edit}.");
}

// Câu 8 : Xóa danh mục khi biết mã
// Chỉ xóa danh mục trống
int madm_delete = 4;
Category cate_remove = context.Categories.FirstOrDefault(c => c.CategoryID == madm_delete);
if (cate_remove != null)
{
    context.Categories.DeleteOnSubmit(cate_remove);
    context.SubmitChanges(); // Xác nhận xóa khỏi cơ sở dữ liệu
    Console.WriteLine($"Đã xóa danh mục ID {madm_delete}.");
}
else
{
    Console.WriteLine($"Không tìm thấy danh mục với ID {madm_delete}.");
}

Console.WriteLine("--------------------------------------\n");
// Câu 9: Xóa danh mục nếu như không có bất kì sản phẩm nào
// lưu ý: là xóa cùng 1 lúc nhiều danh mục, mà các danh mục này
// không có chứa bất kỳ 1 sản phẩm nào

// Cách 1
var emptyCategories = context.Categories
    .Where(c => !context.Products.Any(p => p.CategoryID == c.CategoryID))
    .ToList();

foreach (var category in emptyCategories)
{
    context.Categories.DeleteOnSubmit(category);
}
context.SubmitChanges(); // Xác nhận xóa khỏi cơ sở dữ liệu

// Cách 2
var dsdm_empty_product = context.Categories
    .Where(c => c.Products.Count() == 0)
    .ToList();
context.Categories.DeleteAllOnSubmit(dsdm_empty_product);
context.SubmitChanges(); // Xác nhận xóa khỏi cơ sở dữ liệu

// Câu 10: Thêm mới 1 danh mục
Category c_new = new Category();
c_new.CategoryName = "Hàng lậu từ Trung Quốc";
context.Categories.InsertOnSubmit(c_new);
context.SubmitChanges();

// Câu 11: Thêm mới nhiều danh mục
List<Category> list = new List<Category> {};
list.Add(new Category() { CategoryName = "Hàng gia dụng"});
list.Add(new Category() { CategoryName = "Hàng điện tử"});
list.Add(new Category() { CategoryName = "Hàng phụ kiện"});
context.Categories.InsertAllOnSubmit(list);
context.SubmitChanges();
