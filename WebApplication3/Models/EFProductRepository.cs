using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models
{
    public class EFProductRepository : IProductRepository
    {
        private readonly AppDbContext context; /// <summary>
        /// Databasecontext
        /// </summary>
        /// <param name="ctx"></param>
        public EFProductRepository(AppDbContext ctx)
        {
            this.context = ctx;
        }
        public IQueryable<Product> Products => context.Products;
        public void SaveProduct(Product product)
        {
            if (product.ID == 0)
            {
                context.Products.Add(product);
            }
            else
            {
                Product database = context.Products.First(p => p.ID == product.ID);
                if (database != null)
                {
                    database.Name = product.Name;
                    database.Description = product.Description;
                    database.Price = product.Price;                  
                    database.Category = product.Category;
                }
            }
            context.SaveChanges();
        }
        public Product DeleteProduct(int ID)
        {
                Product product = context.Products.First(p => p.ID == ID);
                if (product != null)
                {
                context.Products.Remove(product);
                context.SaveChanges();
            }
            return product;
       
        }
    }
}
