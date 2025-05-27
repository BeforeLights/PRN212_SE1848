using OOP2;
using System.Text;

Console.OutputEncoding = System.Text.Encoding.UTF8;

FulltimeEmployee Obama = new FulltimeEmployee();
Obama.Id = 1;
Obama.IdCart = "123";
Obama.Name = "Barack Obama";
Obama.Birthday = new DateTime(1961, 8, 4);
Console.WriteLine($"Thông tin về Obama");
Console.WriteLine($"Id: {Obama.Id}");
Console.WriteLine($"IdCart: {Obama.IdCart}");
Console.WriteLine($"Name: {Obama.Name}");
Console.WriteLine($"Birthday: {Obama.Birthday.ToString("dd/MM/yyyy")}");
Console.WriteLine($"Lương: {Obama.calSalary()}");

Console.WriteLine("--------------------------------------------------");
ParttimeEmployee Trump = new ParttimeEmployee();
Trump.Id = 2;
Trump.IdCart = "456";
Trump.Name = "Donald Trump";
Trump.workingHours = 2; // Số giờ làm việc
Trump.Birthday = new DateTime(1946, 6, 14);
Console.WriteLine($"Thông tin về Trump");
Console.WriteLine($"Id: {Trump.Id}");
Console.WriteLine($"IdCart: {Trump.IdCart}");
Console.WriteLine($"Name: {Trump.Name}");
Console.WriteLine($"Birthday: {Trump.Birthday.ToString("dd/MM/yyyy")}");
Console.WriteLine($"Lương: {Trump.calSalary()}");

Console.WriteLine("--------------------------------------------------");
ParttimeEmployee Biden = new ParttimeEmployee();
Biden.Id = 3;
Biden.IdCart = "789";
Biden.Name = "Joe Biden";
Biden.workingHours = 3; // Số giờ làm việc
Biden.Birthday = new DateTime(1942, 11, 20);
Console.WriteLine(Obama);
Console.WriteLine(Trump);
Console.WriteLine(Biden);