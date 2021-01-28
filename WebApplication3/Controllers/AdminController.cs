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
        private IProductRepository repo;

        public AdminController(IProductRepository repo)
        {
            this.repo = repo;
        }
        public ViewResult Index()
        {
            return View(repo.Products);
        }
        public ViewResult Create()
        {
            return View("Edit", new Product());
        }
        [HttpPost]
        public IActionResult Delete(int productID)
        {
            Product product = repo.DeleteProduct(productID);
            if (product != null)
            {
                TempData["message"] = $"Produkt {product.Name} został usunięty";
            }
            //return RedirectToAction("Index");
            return View("Index", repo.Products);
        }
        public ViewResult Edit(int productID)
        {
            return View(repo.Products.First(x => x.ID == productID));
        }
        [HttpPost]
        public IActionResult Save(Product product)
        {
            if (ModelState.IsValid)
            {
                repo.SaveProduct(product);
                TempData["message"] = $"Produkt {product.Name} został zapisany";
                return RedirectToAction("Index");
            }
            else
            {
                return View("Edit", product);
            }
        }


    }
}
