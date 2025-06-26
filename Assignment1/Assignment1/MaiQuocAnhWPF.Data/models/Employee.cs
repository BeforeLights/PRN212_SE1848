using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaiQuocAnhWPF.Data.models
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string JobTitle { get; set; }
        public string BirthDate { get; set; }
        public string HireDate { get; set; }
        public string Address { get; set; }
    }
}
