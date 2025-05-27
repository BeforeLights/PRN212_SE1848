using OOP2;
using OOP4_Resume_OOP2;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;

FulltimeEmployee fe = new FulltimeEmployee
{
    Id = 1,
    IdCart = "123456789",
    Name = "Nguyen Van A",
    Birthday = new DateTime(1990, 1, 1),
};

Console.WriteLine(fe);
Console.WriteLine($"Tuổi: {fe.Tuoi()}");

Console.WriteLine("---------------------------------------------------");

Console.WriteLine(fe.IsBirthday());