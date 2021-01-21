using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{   
    [Authorize]
    public class AdminController : Controller
    {
        private IProductRepository repository;

        public AdminController(IProductRepository repo)
        {
            repository = repo;
        }
        public ViewResult Index() => View(repository.Products);
        public ViewResult Edit(int productID) => View(repository.Products.FirstOrDefault(p => p.ID == productID));
        [HttpPost]
        public IActionResult Save(Product product)
        {
            if (ModelState.IsValid)
            {
               
                repository.SaveProduct(product);
                TempData["message"] = $"Zapisano {product.Name}";
                return RedirectToAction("Index");
            }
            else
            {
                return View("Edit", product);
            }
        }
        public ViewResult Create() => View("Edit", new Product());
        [HttpPost]
        public IActionResult Delete (int productID)
        {
            Product deleteProduct = repository.DeleteProduct(productID);
            if(deleteProduct != null)
            {
                TempData["message"] = $"Usunięto {deleteProduct.Name}";
            }
            return RedirectToAction("Index");
        }
    }
}
