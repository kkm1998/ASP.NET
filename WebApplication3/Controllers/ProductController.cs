using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository repo;

        public ProductController(IProductRepository repo)
        {
            this.repo = repo;
        }
        public IActionResult Index()
        {
            return View();
        }
        public ViewResult ListAll()
        {
            return View(repo.Products);
        }
        public ViewResult List(string category)
        {
            return View(repo.Products.Where(x => x.Category == category));
        }
        public ViewResult GetById(int ID)
        {
            return View(repo.Products.Single(x => x.ID == ID));
        }

    }
}
