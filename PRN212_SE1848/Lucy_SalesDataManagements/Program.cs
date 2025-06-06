using Lucy_SalesDataManagements;
using System.Text;

Console.OutputEncoding = System.Text.Encoding.UTF8;
string connectionString = @"server=DESKTOP-2L7O5BN\SQLEXPRESS;database = Lucy_SalesData;uid=sa;pwd=12345";

// This should match what's in your designer.cs file
Lucy_SalesDataDataContext context = new Lucy_SalesDataDataContext(connectionString);

// Câu 1: Lọc ra toàn bộ khách hàng
var dskh = context.Customers.ToList();
Console.WriteLine("--- Danh sách khách hàng ---");
foreach (var d in dskh)
{
    Console.WriteLine($"Customer ID: {d.CustomerID}, Name: {d.ContactName}");
}

// Câu 2: TÌm chi tiết thông tin khách hàng
// khi biết mã khách hàng
int customerId = 1; // Giả sử mã khách hàng là 1
Customer customer = context.Customers.FirstOrDefault(c => c.CustomerID == customerId);
if (customer != null)
{
    Console.WriteLine($"Chi tiết khách hàng ID {customerId}: Tên: {customer.ContactName}");
}
else
{
    Console.WriteLine($"Không tìm thấy khách hàng với ID {customerId}.");
}

// Câu 3: Từ kết quả của câu 2, lọc ra danh sách các hóa đơn của khách hàng
// Các cột dữ liệu gồm mã hóa đơn, ngày lập hóa đơn
if (customer != null)
{
    var dshd = customer.Orders.Select(od => new 
    { 
        od.OrderID, 
        od.OrderDate 
    }).ToList();
    var dshd2 = customer.Orders.Select(od => new 
    { 
        od.OrderID, 
        od.OrderDate 
    }).ToList();
    foreach (var order in dshd)
    {
        Console.WriteLine($"Order ID: {order.OrderID}, Order Date: {order.OrderDate}");
    }
}

// Câu 4: Từ kết quả câu 3
// Bổ sung thêm cột Trị giá của đơn hàng cho mỗi hóa đơn
if (customer != null)
{
    var dshd_with_value = customer.Orders.Select(od => new
    {
        od.OrderID,
        od.OrderDate,
        TotalValue = od.Order_Details.Sum(detail => 
            detail.UnitPrice * detail.Quantity * (decimal)(1 - detail.Discount))
    }).ToList();

    Console.WriteLine("\n--- Danh sách hóa đơn kèm trị giá ---");
    foreach (var order in dshd_with_value)
    {
        Console.WriteLine($"Order ID: {order.OrderID}, Order Date: {order.OrderDate}, Total Value: {order.TotalValue:C}");
    }
}


