using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeViewWpfApp.models
{
    public class SampleDataset
    {
        public static Dictionary<int,Category> GenerateDataset()
        { 
            Dictionary<int,Category> categories=new Dictionary<int, Category>();
            Category c1 = new Category() { Id = 1,Name="Nước ngọt"};
            Category c2 = new Category() { Id = 2, Name = "Bia - Rượu" };
            Category c3 = new Category() { Id = 3, Name = "Thức ăn nhanh" };
            categories.Add(c1.Id, c1);
            categories.Add(c2.Id, c2);
            categories.Add(c3.Id, c3);
            Product p1 = new Product() { Id = 1, Name = "Coca", Quantity = 10, Price = 25 };
            Product p2 = new Product() { Id = 2, Name = "Pepsi", Quantity = 20, Price = 35 };
            Product p3 = new Product() { Id = 3, Name = "Sting", Quantity = 14, Price = 15 };
            Product p4 = new Product() { Id = 4, Name = "Redbull", Quantity = 16, Price = 43 };
            Product p5 = new Product() { Id = 5, Name = "Xá Xị", Quantity = 16, Price = 12 };
            Product p6 = new Product() { Id = 6, Name = "Bia Sài Gòn", Quantity = 10, Price = 75 };
            Product p7 = new Product() { Id = 7, Name = "Bia Tiger", Quantity = 4, Price = 25 };
            Product p8 = new Product() { Id = 8, Name = "Bia Ken", Quantity = 3, Price = 36 };
            Product p9 = new Product() { Id = 9, Name = "Bia 333", Quantity = 25, Price = 25 };
            Product p10 = new Product() { Id = 10, Name = "Bia Sư Tử", Quantity = 12, Price = 35 };
            Product p11 = new Product() { Id = 11, Name = "Khoai tây chiên", Quantity = 63, Price = 24 };
            Product p12 = new Product() { Id = 12, Name = "Khoai Lang Nướng", Quantity = 23, Price = 13 };
            Product p13 = new Product() { Id = 13, Name = "Bắp nướng", Quantity = 13, Price = 25 };
            Product p14 = new Product() { Id = 14, Name = "snack", Quantity = 10, Price = 7 };
            Product p15 = new Product() { Id = 15, Name = "súc xích", Quantity = 35, Price = 6 };

            c1.Products.Add(p1.Id, p1);
            c1.Products.Add(p2.Id, p2);
            c1.Products.Add(p3.Id, p3);
            c1.Products.Add(p4.Id, p4);
            c1.Products.Add(p5.Id, p5);

            c2.Products.Add(p6.Id, p6);
            c2.Products.Add(p7.Id, p7);
            c2.Products.Add(p8.Id, p8);
            c2.Products.Add(p9.Id, p9);
            c2.Products.Add(p10.Id, p10);

            c3.Products.Add(p11.Id, p11);
            c3.Products.Add(p12.Id, p12);
            c3.Products.Add(p13.Id, p13);
            c3.Products.Add(p14.Id, p14);
            c3.Products.Add(p15.Id, p15);

            return categories;
        }
    }
}
