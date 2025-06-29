using BusinessObjects;

namespace DataAccessLayer
{
    public class CategoryDAO
    {
        public static List<Category> categories = new List<Category>();
        public static List<Category> GetCategories()
        {
            try
            {
                using var context = new MyStoreContext();
                categories = context.Categories.ToList();
                return categories;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
