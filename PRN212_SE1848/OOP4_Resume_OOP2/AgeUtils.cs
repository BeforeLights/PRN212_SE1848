using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOP2;
namespace OOP4_Resume_OOP2
{
    public static class AgeUtils
    {
        public static int Tuoi(this Employee emp)
        {
            return DateTime.Now.Year - emp.Birthday.Year;
        }

        public static String IsBirthday(this Employee emp)
        {
            if (emp.Birthday.Month == DateTime.Now.Month && emp.Birthday.Day == DateTime.Now.Day)
            {
                return "Hôm nay là sinh nhật của bạn!";
            }
            else
            {
                return "Hôm nay không phải là sinh nhật của bạn!";
            }
        }
    }
}
