using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP1
{
    public class Customer
    {
        //POCO approach
        public string CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerAddress { get; set; }

        public void PrintInfor()
        {
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Blue;
            string msg = $"CustomerID: {CustomerID}\nCustomerName: {CustomerName}\nCustomerPhone: {CustomerPhone}\nCustomerEmail: {CustomerEmail}\nCustomerAddress: {CustomerAddress}";
            Console.WriteLine(msg);
            Console.ResetColor();
        }
    }
}
