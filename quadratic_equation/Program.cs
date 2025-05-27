using System.Text;

void first_degree_solution(double a, double b)
{
    if (a == 0)
    {
        if (b == 0)
        {
            Console.WriteLine("Phương trình có vô số nghiệm");
        }
        else
        {
            Console.WriteLine("Phương trình vô nghiệm");
        }
    }
    else
    {
        double x = -b / a;
        Console.WriteLine($"Phương trình có nghiệm x = {x}");
    }
}
void quadratic_equation_solution(double a, double b, double c)
{
    if (a == 0)
    {
        first_degree_solution(b, c);
    }
    else
    {
        var delta = b * b - 4 * a * c;
        if (delta > 0)
        {
            var x1 = (-b + Math.Sqrt(delta)) / (2 * a);
            var x2 = (-b - Math.Sqrt(delta)) / (2 * a);
            Console.WriteLine($"Phương trình có 2 nghiệm phân biệt: x1 = {x1}, x2 = {x2}");
        }
        else if (delta == 0)
        {
            var x = -b / (2 * a);
            Console.WriteLine($"Phương trình có nghiệm kép: x = {x}");
        }
        else
        {
            Console.WriteLine("Phương trình vô nghiệm");
        }
    }
}
Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.WriteLine("Giải phương trình bậc 2 ax^2 + bx + c = 0");
Console.Write("Nhập a: ");
var a = double.Parse(Console.ReadLine());
Console.Write("Nhập b: ");
var b = double.Parse(Console.ReadLine());
Console.Write("Nhập c: ");
var c = double.Parse(Console.ReadLine());
quadratic_equation_solution(a, b, c);
Console.ReadLine();