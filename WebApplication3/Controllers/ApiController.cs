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
        private readonly IProductRepository repo;

        public ApiController(IProductRepository repo)
        {
            this.repo = repo;
        }
        /// <summary>
        /// Get all products
        /// </summary>
        /// <param name="category">category name</param>
        /// <returns>Products list</returns>
        [HttpGet("GetAll")]
        public ActionResult<List<Product>> List()
        {
            return Ok(repo.Products.AsEnumerable().ToList());
        }
        /// <summary>
        /// Delete product
        /// </summary>
        /// <param name="ID">ProductID</param>
        /// <returns>Nothing</returns>
        [HttpDelete("{ID}")]
        public ActionResult DeleteProduct(int ID)
        {
            repo.DeleteProduct(ID);
            return NoContent();
        }
        /// <summary>
        /// Get product by ID
        /// </summary>
        /// <param name="ID">ProductID</param>
        /// <returns>Product</returns>
        [HttpGet("GetById {ID}")]
        public ActionResult<Product> GetById(int ID)
        {
            var product = repo.Products.SingleOrDefault(x => x.ID == ID);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        /// <summary>
        /// Adds a product
        /// </summary>
        /// <param name="product">product</param>
        /// <returns>Product</returns>
        [HttpPost]
        public ActionResult<Product> AddProduct(Product product)
        {
            repo.SaveProduct(product);
            return Ok(product);
        }
        /// <summary>
        /// Updates a product
        /// </summary>
        /// <param name="product">product</param>
        /// <returns>Product</returns>
        [HttpPut("{ID}")]
        public ActionResult UpdateProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (!repo.Products.Any(x => x.ID == product.ID))
            {
                return NotFound();
            }
            repo.SaveProduct(product);
            return NoContent();
        }

    }
}
