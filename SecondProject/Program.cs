namespace SecondProject

{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("Minh họa lấy giá trị từ" + " outside argument");
            if (args.Length > 0)
            {
                int sum = 0;
                for (int i = 0; i < args.Length; i++)
                {
                    int item = int.Parse(args[i]);
                    sum += item;
                    Console.WriteLine(item);
                }
                Console.WriteLine($"Tổng các số là: {sum}");
            }
            Console.ReadLine();
        }
    }
}