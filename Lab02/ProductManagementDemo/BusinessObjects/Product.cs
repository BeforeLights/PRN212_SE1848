using System.ComponentModel.DataAnnotations;

namespace BusinessObjects
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int CategoryID { get; set; }
        public short UnitsInStock { get; set; }
        public Decimal UnitPrice { get; set; }
    }
}