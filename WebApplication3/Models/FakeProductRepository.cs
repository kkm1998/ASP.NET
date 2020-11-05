using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models
{
    public class FakeProductRepository : IProductRepository
    {
        public IQueryable<Product> Products => new List<Product> {
            new Product {ID=1, Name = "AA",Description="aaaaaa",Price=12,Category="XYZ"},
            new Product {ID=2, Name = "BB",Description="bbbbbb",Price=11,Category="XYZ"},
            new Product {ID=3, Name = "CC",Description="cccccc",Price=9,Category="XYZ"},
            new Product {ID=4, Name = "DD",Description="dddddd",Price=15,Category="XYZ"},
        }.AsQueryable<Product>();
    }
}
