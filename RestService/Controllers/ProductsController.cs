using Data;
using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlTypes;

namespace RestService.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : Controller {
        private Storage _storage;

        public ProductsController(Storage storage) {
            _storage = storage;
        }
        [HttpGet]
        public IEnumerable<Product> Index() {
            // Get all products
            return _storage.Products.GetAll();
        }

        [HttpGet("details")]
        public Task<Product> Details(Guid? id) {
            return _storage.Products.Get(id);
        }

        [HttpPost("create")]
        public async Task<ActionResult<Product>> Create(Product product) {
            await _storage.Products.Add(product);
            return product;
        }

        [HttpPut("edit")]
        public async Task<ActionResult<Product>> Edit(Product product) {
            if (product.Id == Guid.Empty) {
                Redirect("/error");
            }

            await _storage.Products.Update(product);
            return product;
        }

        [HttpDelete("delete")]
        public async Task<ActionResult> Delete(Product product) {
            if (product.Id == Guid.Empty) {
                return NotFound();
            }
            await _storage.Products.Remove(product);

            return Ok();
        }
    }
}
