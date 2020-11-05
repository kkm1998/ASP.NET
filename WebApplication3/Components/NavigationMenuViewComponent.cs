using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models;

namespace WebApplication3.Components
{
    public class NavigationMenuViewComponent:ViewComponent
    {
        private readonly IProductRepository _productRepository;
        public NavigationMenuViewComponent(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public IViewComponentResult Invoke()
        {
            return View(_productRepository.Products.Select(x =>x.Category).Distinct().OrderBy(x => x));
        }

    }
}
