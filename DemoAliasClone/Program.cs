using System.Text;
using DemoAliasClone;

Console.OutputEncoding = Encoding.UTF8;

Customer customer1 = new Customer
{
    Id = 1,
    Name = "Nguyễn Văn A"
};

Customer customer2 = new Customer
{
    Id = 2,
    Name = "Trần Thị B"
};

customer1 = customer2; // Gán customer1 bằng customer2, không phải clone, customer1 trỏ đến vùng nhớ của customer2

/* * 
 * Lúc này sẽ có 2 tình huống :
 * 
 * (1)
 * Ô nhớ alpha mà customer1 trỏ đến sẽ bị giải phóng (nếu không còn biến nào khác trỏ đến nó)
 * -> Hệ điều hành sẽ giải phóng vùng nhớ này và không còn truy cập được nữa.
 * việc giải phóng vùng nhớ là cơ chế gom rác tự động được gọi là Automatic Garbage Collection (GC) trong C#.
 * 
 * (2)
 * Khi gán customer1 = customer2, cả hai biến đều trỏ đến cùng một đối tượng trong bộ nhớ.
 * Thay đổi thuộc tính của customer2 sẽ ảnh hưởng đến customer1 vì chúng cùng trỏ đến một đối tượng,
 * chúng cùng quản lý vùng nhớ này. Đây được gọi là Alias, tức là hai biến khác nhau nhưng trỏ đến cùng một vùng nhớ.
 * 
 * Ví dụ:
 */
customer1.Name = "Nguyễn Văn C"; // Thay đổi tên của customer1
Console.WriteLine(customer2.Name); // In ra "Nguyễn Văn C", vì customer1 và customer2 trỏ đến cùng một đối tượng

Customer customer3 = new Customer();
Customer customer4 = customer3; // customer4 là alias của customer3
customer3 = customer1; // customer3 giờ trỏ đến đối tượng khác, không còn là alias của customer4 nữa