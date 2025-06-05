using System.Text;
void first_degree_solution(double a, double b)
{
        if (a == 0) {
        if (b == 0) {
            Console.WriteLine("Phương trình có vô số nghiệm");
        } else {
            Console.WriteLine("Phương trình vô nghiệm");
        }
    } else {
        double x = -b / a;
        Console.WriteLine($"Phương trình có nghiệm x = {x}");
    }
}
Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.WriteLine("Giải phương trình bậc nhất ax + b = 0");
double a, b;
Console.Write("Nhập a: ");
a = double.Parse(Console.ReadLine());
Console.Write("Nhập b: ");
b = double.Parse(Console.ReadLine());
first_degree_solution(a, b);
Console.ReadLine();

List<double> list = new List<double>();
list.ForEach(Console.WriteLine);