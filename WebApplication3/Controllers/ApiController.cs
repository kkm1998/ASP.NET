using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    [Route("api/[controller]")]
    public class ApiController : Controller
    {
        private readonly IProductRepository repository;

        public ApiController(IProductRepository repository)
        {
            this.repository = repository;
        }
        /// <summary>
        /// Get all products
        /// </summary>
        /// <param name="category">category name</param>
        /// <returns>Products list</returns>
        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<Product>> List(string category)
        {
            return Ok(repository.Products.Where(p => p.Category == category));
        }
        [HttpGet("GetById")]
        public ActionResult<Product> GetById(int id)
        {
            var product = repository.Products.SingleOrDefault(p => p.ID == id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }
        [HttpPost]
        public ActionResult<Product> AddProduct(Product product)
        {
            repository.SaveProduct(product);
            CreatedAtAction(nameof(GetById), new { id = product.ID }, product);
            return Ok(product);
        }

        [HttpDelete]
        public ActionResult DeleteProduct(int productId)
        {
            repository.DeleteProduct(productId);
            return NoContent();
        }
        [HttpPut]
        public ActionResult UpdateProduct(Product product)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (!repository.Products.Any(p => p.ID == product.ID))
                return NotFound();

            repository.SaveProduct(product);

            return NoContent();
        }

    }
}
