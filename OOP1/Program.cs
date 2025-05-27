using OOP1; 
using System.Text;

Console.OutputEncoding = Encoding.UTF8;
// Tạo đối tượng category1 từ lớp Category
Category category1 = new Category();
category1.Id = 1;
category1.Name = "Nước mắm";
// Xuất thông tin bằng cách gọi hàm
category1.PrintInfor();
// giả sử ta đổi giá trị trong ô nhớ đó
category1.Name = "Thuốc trị hôi nách";
Console.WriteLine("Sau khi giá trị:");
category1.PrintInfor();

// sử dụng lớp employee
Console.WriteLine("-------------------------------------EMPLOYEE 1---------------------------------");
Employee employee1 = new Employee();
employee1.Id = 1; // gọi setter property của Id
employee1.Name = "Tèo"; // gọi setter property của Name
employee1.IdCard = "001"; // gọi setter property của IdCard
employee1.Email = "teo@gmail.com"; // gọi setter property của Email
employee1.Phone = "0123456789"; // gọi setter property của Phone
// Xuất thông tin ra
employee1.PrintInfor();

Employee employee2 = new Employee()
{
    Id = 2,
    IdCard = "002",
    Name = "Tý",
    Email = "ty@gmail.com",
    Phone = "0123456789"
};
Console.WriteLine("-------------------------------------EMPLOYEE 2---------------------------------");
employee2.PrintInfor();

Employee employee3 = new Employee();
Console.WriteLine("-------------------------------------EMPLOYEE 3---------------------------------");
employee3.PrintInfor();

Employee employee4 = new Employee(4, "Hà", "003", "ha@gmail.com", "0123456789");
Console.WriteLine("-------------------------------------EMPLOYEE 4 (1)---------------------------------");
employee4.PrintInfor();
Console.WriteLine("-------------------------------------EMPLOYEE 4 (2)---------------------------------");
Console.WriteLine(employee4);

Console.WriteLine("-------------------------------------CUSTOMER 1---------------------------------");
Customer customer1 = new Customer()
{
    CustomerID = "1",
    CustomerName = "Nguyễn Văn A",
    CustomerPhone = "0123456789",
    CustomerEmail = "asd@gmail.com",
    CustomerAddress = "asdasdsad"
};
customer1.PrintInfor();
customer1.CustomerAddress = "Hà Nội";
Console.WriteLine("Sau khi giá trị đổi:");
customer1.PrintInfor();