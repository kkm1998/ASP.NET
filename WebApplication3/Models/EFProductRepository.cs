using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models
{
    public class EFProductRepository : IProductRepository
    {
        private readonly AppDbContext ctx; /// <summary>
        /// Databasecontext
        /// </summary>
        /// <param name="ctx"></param>
        public EFProductRepository(AppDbContext ctx)
        {
            this.ctx = ctx;
        }
        public IQueryable<Product> Products => ctx.Products;
        public void SaveProduct(Product product)
        {
            if (product.ID == 0)
            {
                ctx.Products.Add(product);
            }
            else
            {
                Product dbEntry = ctx.Products
                    .FirstOrDefault(p => p.ID == product.ID);
                if (dbEntry != null)
                {
                    dbEntry.Name = product.Name;
                    dbEntry.Description = product.Description;
                    dbEntry.Price = product.Price;                  
                    dbEntry.Category = product.Category;

                }
            }
            ctx.SaveChanges();
        }
        public Product DeleteProduct(int ID)
        {
                Product dbEntry = ctx.Products
                    .FirstOrDefault(p => p.ID == ID);
                if (dbEntry != null)
                {

                ctx.Products.Remove(dbEntry);
                ctx.SaveChanges();
            }
            return dbEntry;
       
        }
    }
}
