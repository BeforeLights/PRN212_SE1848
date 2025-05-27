using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OOP3_ExtensionMethod
{
    public static class Pokemon
    {
        public static int TongTuMotToiN(this int n)
        {
            int sum = 0;
            for (int i = 0; i <= n; i++)
            {
                sum += i;
            }
            return sum;
        }

        public static int CongTuHaiSo(this int a, int b)
        {
            return a + b;
        }

        public static void TaoMang(this int[] arr)
        {
            Random random = new Random();
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = random.Next(100); // Giả sử bạn muốn tạo số ngẫu nhiên từ 0 đến 99
            }
        }

        public static void InMang(this int[] arr)
        {
            Console.WriteLine("Mảng các số nguyên:");
            foreach (int i in arr)
            {
                Console.Write(i + "\t");
            }
            Console.WriteLine();
        }

        public static void SapXepMangTangDan(this int[] arr)
        {
            int i = 0, j = 0, temp = 0;
            for (i = 0; i < arr.Length - 1; i++)
            {
                for (j = i + 1; j < arr.Length; j++)
                {
                    if (arr[i] > arr[j])
                    {
                        temp = arr[i];
                        arr[i] = arr[j];
                        arr[j] = temp;
                    }
                }
            }
            Console.WriteLine("Mảng đã được sắp xếp tăng dần:");
            InMang(arr);
        }

        public static void SapXepMangGiamDan(this int[] arr)
        {
            Console.WriteLine("Mảng sau khi sắp xếp giảm dần:");
            // Cách 1 : Sử dụng thuật toán sắp xếp nổi bọt (Bubble Sort)
            arr = arr.OrderByDescending(x => x).ToArray();
            // Cách 2 : Sử dụng LINQ
            // Array.Sort(arr, (x, y) => y.CompareTo(x)); // Sắp sxếp mảng theo thứ tự giảm dần
            // Cách 3 : Sử dụng phương thức Sort của mảng
            // Array.Sort(arr);
            // Array.Reverse(arr); // Đảo ngược mảng đã sắp xếp theo thứ tự tăng dần
            InMang(arr);
        }
    }
}
