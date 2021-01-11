using CommonLayer.Models;
using Repository;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace RepositoryLayer.Services
{
    public class ProductRL:IProductRL
    {
        private readonly BookStoreContext context;

        public ProductRL(BookStoreContext context)
        {
            this.context = context;
        }
        public List<Product> GetAllProducts()
        {
            try
            {
                var employeeRecord = from p in this.context.products.ToList() select p;
                var car = from c in this.context.cartItems.ToList() select c;
                List<Product> products = new List<Product>();
                foreach (Product item in employeeRecord)
                {
                    var r = this.context.cartItems.Where(x =>
                                                    x.product_id == item.product_id).FirstOrDefault();

                    if (r != null)
                    {
                        item.addedToCart = true;
                    }
                    else
                    {
                        item.addedToCart = false;
                    }
                    products.Add(item);
                }
                return products;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
