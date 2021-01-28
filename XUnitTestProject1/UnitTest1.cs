using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication3.Controllers;
using WebApplication3.Models;
using Xunit;

namespace WebApplication3.test
{
    public class UnitTest1
    {

        [Fact]
        public void TestAllProducts()
        {
            //Arrange repo
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ID = 1, Name = "Kajak"},
                new Product {ID = 2, Name = "Namiot"},
                new Product {ID = 3, Name = "Pi³ka"},
                new Product {ID = 4, Name = "£ódka"},
                new Product {ID = 5, Name = "Czapka"},
            }.AsQueryable<Product>());

            //Arrange controler
            AdminController controller = new AdminController(mock.Object);
            //Act
            Product[] outcome = GetViewModel<IEnumerable<Product>>(controller.Index())?.ToArray();

            //Assertion
            Assert.Equal(5, outcome.Length);
            Assert.Equal("Kajak", outcome[0].Name);
            Assert.Equal("Namiot", outcome[1].Name);
            Assert.Equal("Pi³ka", outcome[2].Name);
            Assert.Equal("£ódka", outcome[3].Name);
            Assert.Equal("Czapka", outcome[4].Name);
        }

        [Fact]
        public void TestFilterProduct()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[] {
                new Product {ID = 1, Name = "Kajak", Category = "Sport"},
                new Product {ID = 2, Name = "Namiot", Category = "Ekwipunek"},
                new Product {ID = 3, Name = "Pi³ka", Category = "Sport"},
                new Product {ID = 4, Name = "£ódka", Category = "Sport"},
                new Product {ID = 5, Name = "Czapka", Category = "Ubiór"}
            }).AsQueryable<Product>());

            ProductController controller = new ProductController(mock.Object);

            Product[] outcome = GetViewModel<IEnumerable<Product>>(controller.List("Sport")).ToArray();


            Assert.Equal(3, outcome.Length);
            Assert.True(outcome[0].Name == "Kajak" && outcome[0].Category == "Sport");
            Assert.True(outcome[1].Name == "Pi³ka" && outcome[1].Category == "Sport");
            Assert.True(outcome[2].Name == "£ódka" && outcome[2].Category == "Sport");
        }

        [Theory]
        [InlineData(1, "Kajak")]
        [InlineData(3, "Pi³ka")]
        [InlineData(5, "Czapka")]
        public void TestProductByID(int id, string name)
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[] {
                new Product {ID = 1, Name = "Kajak", Category = "Sport"},
                new Product {ID = 2, Name = "Namiot", Category = "Ekwipunek"},
                new Product {ID = 3, Name = "Pi³ka", Category = "Sport"},
                new Product {ID = 4, Name = "£ódka", Category = "Sport"},
                new Product {ID = 5, Name = "Czapka", Category = "Ubiór"}
            }).AsQueryable<Product>());
            ProductController controller = new ProductController(mock.Object);

            Product outcome = GetViewModel<Product>(controller.GetById(id));

            Assert.Equal(outcome.Name, name);
        }

        private T GetViewModel<T>(IActionResult result) where T : class
        {
            return (result as ViewResult)?.ViewData.Model as T;
        }
    }
}
