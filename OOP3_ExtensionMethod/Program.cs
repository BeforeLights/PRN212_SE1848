using OOP3_ExtensionMethod;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;
int n1 = 5;
int n2 = 10;
Console.WriteLine($"Tổng từ 1 đến {n1} là: {n1.TongTuMotToiN()}");
Console.WriteLine($"Tổng từ 1 đến {n2} là: {n2.TongTuMotToiN()}");
Console.WriteLine($"Tổng từ 1 đến 8 là: {8.TongTuMotToiN()}");

Console.WriteLine("---------------------------------------------------");

Console.WriteLine($"Tổng của {n1} và {n2} là: {n1.CongTuHaiSo(n2)}");
Console.WriteLine($"Tổng của 8 và 7 là: {8.CongTuHaiSo(7)}");

Console.WriteLine("---------------------------------------------------");

int[] arr = new int[8];
arr.TaoMang();
Console.WriteLine("Mảng sau khi tạo ngẫu nhiên");
arr.InMang();
arr.SapXepMangTangDan();

Console.WriteLine("---------------------------------------------------");

int[] arr2 = new int[5];
arr2.TaoMang();
Console.WriteLine("Mảng thứ hai sau khi tạo ngẫu nhiên");
arr2.InMang();
arr2.SapXepMangGiamDan();
