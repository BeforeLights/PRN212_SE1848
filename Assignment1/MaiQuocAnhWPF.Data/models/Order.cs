using System;
using System.Collections.ObjectModel;

namespace MaiQuocAnhWPF.Data.models
{
    public class Order
    {
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public int EmployeeID { get; set; }
        public DateTime OrderDate { get; set; }
        public ObservableCollection<OrderDetail> OrderDetails { get; set; } = new();
    }
}
