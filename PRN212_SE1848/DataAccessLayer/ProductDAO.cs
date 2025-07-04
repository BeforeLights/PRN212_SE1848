﻿using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class ProductDAO
    {
        static List<Product> products = new List<Product>();
        public void GenerateSampleDataset()
        {
            products.Add(new Product() { Id=1,Name="Coca",Quantity=10,Price=100});
            products.Add(new Product() { Id = 2, Name = "Pepsi", Quantity = 30, Price = 500 });
            products.Add(new Product() { Id = 3, Name = "Sting", Quantity = 20, Price = 300 });
            products.Add(new Product() { Id = 4, Name = "Redbull", Quantity = 25, Price = 400 });
            products.Add(new Product() { Id = 5, Name = "Lavie", Quantity = 20, Price = 200 });
        }
        public List<Product> GetProducts()
        {
            return products;
        }
        public bool SaveProduct(Product product)
        {
            Product old=products.FirstOrDefault(p=>p.Id==product.Id);
            if (old != null)
                return false;//mã sản phẩm này đã tồn tại, ko thêm mới được
            products.Add(product);
            return true;//thêm mới thành công
        }
        public Product GetProduct(int id)
        {
            return products.FirstOrDefault(p => p.Id == id); 
        }
        public bool UpdateProduct(Product product)
        {
            //Bước 1: Tìm xem product muốn sửa này có tồn tại ko?
            Product old=products.FirstOrDefault(p=>p.Id==product.Id);
            if (old == null)//không tìm thấy
                return false;
            old.Name = product.Name;
            old.Quantity=product.Quantity;
            old.Price=product.Price;
            return true;
        }
        public bool DeleteProduct(int id)
        {
            Product p=GetProduct(id);
            if (p != null)
            {
                products.Remove(p);

                return true;
            }
            return false;
        }
        public bool DeleteProduct(Product product)
        {
            if(product==null) 
                return false;
            return DeleteProduct(product.Id);
        }
    }
}
