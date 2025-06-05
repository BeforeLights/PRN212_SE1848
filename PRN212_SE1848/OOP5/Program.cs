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

Console.WriteLine("---------------------------------------------------");

// Câu 6: Cập nhật (Edit) thông tin nhân viên
Console.WriteLine("\n== Cập nhật thông tin nhân viên ==");
bool EditEmployee(int employeeId)
{
    Employee? employeeToEdit = employees.FirstOrDefault(e => e.Id == employeeId);

    if (employeeToEdit == null)
    {
        Console.WriteLine($"Không tìm thấy nhân viên có ID: {employeeId}");
        return false;
    }

    Console.WriteLine($"Đang chỉnh sửa nhân viên: {employeeToEdit}");

    Console.Write("Nhập tên mới (để trống nếu không thay đổi): ");
    string? newName = Console.ReadLine();
    if (!string.IsNullOrWhiteSpace(newName))
    {
        employeeToEdit.Name = newName;
    }

    Console.Write("Nhập ID Card mới (để trống nếu không thay đổi): ");
    string? newIdCard = Console.ReadLine();
    if (!string.IsNullOrWhiteSpace(newIdCard))
    {
        employeeToEdit.IdCart = newIdCard;
    }

    Console.Write("Nhập ngày sinh mới (định dạng dd/MM/yyyy, để trống nếu không thay đổi): ");
    string? newBirthdayStr = Console.ReadLine();
    if (!string.IsNullOrWhiteSpace(newBirthdayStr))
    {
        if (DateTime.TryParseExact(newBirthdayStr, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime newBirthday))
        {
            employeeToEdit.Birthday = newBirthday;
        }
        else
        {
            Console.WriteLine("Định dạng ngày không hợp lệ, giữ nguyên ngày sinh cũ.");
        }
    }

    // Nếu là nhân viên partime, cập nhật số giờ làm việc
    if (employeeToEdit is ParttimeEmployee parttime)
    {
        Console.Write("Nhập số giờ làm việc mới (để trống nếu không thay đổi): ");
        string? newHoursStr = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(newHoursStr) && int.TryParse(newHoursStr, out int newHours))
        {
            parttime.workingHours = newHours;
        }
    }

    Console.WriteLine("Thông tin sau khi cập nhật:");
    Console.WriteLine(employeeToEdit);
    return true;
}

// Câu 7: Xóa nhân viên
Console.WriteLine("\n== Xóa nhân viên ==");
bool DeleteEmployee(int employeeId)
{
    Employee? employeeToDelete = employees.FirstOrDefault(e => e.Id == employeeId);

    if (employeeToDelete == null)
    {
        Console.WriteLine($"Không tìm thấy nhân viên có ID: {employeeId}");
        return false;
    }

    Console.WriteLine($"Bạn có chắc chắn muốn xóa nhân viên sau? (y/n)");
    Console.WriteLine(employeeToDelete);

    string? confirmation = Console.ReadLine()?.ToLower();

    if (confirmation == "y")
    {
        employees.Remove(employeeToDelete);
        Console.WriteLine("Đã xóa nhân viên thành công.");
        return true;
    }
    else
    {
        Console.WriteLine("Hủy xóa nhân viên.");
        return false;
    }
}

// Thử nghiệm chức năng Edit
Console.WriteLine("\nThử nghiệm chức năng Edit:");
Console.Write("Nhập ID nhân viên cần chỉnh sửa: ");
if (int.TryParse(Console.ReadLine(), out int editId))
{
    EditEmployee(editId);
}
else
{
    Console.WriteLine("ID không hợp lệ!");
}

// Thử nghiệm chức năng Delete
Console.WriteLine("\nThử nghiệm chức năng Delete:");
Console.Write("Nhập ID nhân viên cần xóa: ");
if (int.TryParse(Console.ReadLine(), out int deleteId))
{
    DeleteEmployee(deleteId);
}
else
{
    Console.WriteLine("ID không hợp lệ!");
}

// Hiển thị danh sách nhân viên sau khi thực hiện các thao tác
Console.WriteLine("\nDanh sách nhân viên sau khi cập nhật:");
foreach (Employee emp in employees)
{
    Console.WriteLine(emp);
}