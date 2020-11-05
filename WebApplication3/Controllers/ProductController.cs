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
        private readonly IProductRepository productRepository;

        public ProductController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        public ViewResult ListAll() => View(productRepository.Products);
        public ViewResult List(string category) => View(productRepository.Products.Where(p=>p.Category == category));
        public IActionResult Index()
        {
            return View();
        }
    }
}
