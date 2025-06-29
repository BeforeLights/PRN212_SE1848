using BusinessObjects;

namespace DataAccessLayer
{
    public class ProductDAO
    {
        public static List<Product> products = new List<Product>();
        public static List<Product> GetProducts()
        {
            try
            {
                using var db = new MyStoreContext();
                products = db.Products.ToList();
                return products;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public static void SaveProduct(Product p)
        {
            try
            {
                using var context = new MyStoreContext();
                context.Products.Add(p);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public static void UpdateProduct(Product p)
        {
            try
            {
                using var context = new MyStoreContext();
                context.Entry<Product>(p).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public static void DeleteProduct(Product p)
        {
            try
            {
                using var context = new MyStoreContext();
                var p1 = context.Products.SingleOrDefault(c => c.ProductID == p.ProductID);
                context.Products.Remove(p1);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public static Product GetProductById(int id)
        {
            using var db = new MyStoreContext();
            return db.Products.FirstOrDefault(c => c.ProductID.Equals(id));
        }
    }
}
