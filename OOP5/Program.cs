/*
 * Sử dụng Generic List để quản lý nhân sự với đầy đủ
 * CRUD
 * C -> Create -> Tạo dữ liệu mới
 * R -> Read -> Xem, lọc, tìm kiếm, sắp xếp, thống kê....
 * U -> Update -> Cập nhật dữ liệu (Sửa)
 * D -> Delete -> Xóa dữ liệu
 */

// Câu 1 : Tạo 5 nhân viên; 3 nhân viên fulltime, 2 nhân viên partime
// Lưu vào Generic List
using System.Text;
using OOP2;
Console.OutputEncoding = Encoding.UTF8;


List<Employee> employees = new List<Employee>();
OOP2.FulltimeEmployee fe1 = new OOP2.FulltimeEmployee
{
    Id = 1,
    IdCart = "123456789",
    Name = "Nguyen Van A",
    Birthday = new DateTime(1990, 1, 1),
};
employees.Add(fe1);

ParttimeEmployee pe1 = new ParttimeEmployee
{
    Id = 2,
    IdCart = "987654321",
    Name = "Nguyen Van B",
    Birthday = new DateTime(1995, 5, 5),
    workingHours = 5 // Số giờ làm việc
};
employees.Add(pe1);

ParttimeEmployee pe2 = new ParttimeEmployee
{
    Id = 3,
    IdCart = "456789123",
    Name = "Nguyen Van C",
    Birthday = new DateTime(1992, 3, 3),
    workingHours = 4 // Số giờ làm việc
};
employees.Add(pe2);

FulltimeEmployee fe2 = new OOP2.FulltimeEmployee
{
    Id = 4,
    IdCart = "321654987",
    Name = "Nguyen Van D",
    Birthday = new DateTime(1988, 8, 8),
};
employees.Add(fe2);

// Câu 2: Xuất toàn bộ nhân sự
Console.WriteLine("Danh sách nhân sự:");
foreach (Employee emp in employees)
{
    Console.WriteLine(emp);
}

Console.WriteLine("---------------------------------------------------");

// Câu 3: Lọc ra các nhân sự chính thức
// Cách 1
List<FulltimeEmployee> fulltimeEmployees = employees.OfType<FulltimeEmployee>().ToList();
Console.WriteLine("\nDanh sách nhân sự chính thức:");
foreach (FulltimeEmployee emp in fulltimeEmployees)
{
    Console.WriteLine(emp);
}

Console.WriteLine("---------------------------------------------------");

// Câu 4: Tính tổng tiền lương phải trả cho nhân viên chính thức
double totalSalary = fulltimeEmployees.Sum(emp => emp.calSalary());
Console.WriteLine($"\nTổng tiền lương phải trả cho nhân viên chính thức: {totalSalary}");

Console.WriteLine("---------------------------------------------------");

// Câu 5: Tính tổng tiền lương phải trả cho nhân viên partime
double totalParttimeSalary = employees.OfType<ParttimeEmployee>().Sum(emp => emp.calSalary());
Console.WriteLine($"\nTổng tiền lương phải trả cho nhân viên partime: {totalParttimeSalary}");