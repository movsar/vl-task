using Data;
using Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace RestService.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class ProductVersionsController : Controller {
        private Storage _storage;

        public ProductVersionsController(Storage storage) {
            _storage = storage;
        }
        [HttpGet]
        public IEnumerable<ProductVersion> Index() {
            // Get all products
            return _storage.ProductVersions.GetAll();
        }

        [HttpGet("details")]
        public Task<ProductVersion> Details(Guid? id) {
            return _storage.ProductVersions.Get(id);
        }

        [HttpPost("create")]
        public async Task<ActionResult<ProductVersion>> Create(ProductVersion productVersions)  {
            await _storage.ProductVersions.Add(productVersions);
            return productVersions;
        }

        [HttpPut("edit")]
        public async Task<ActionResult<ProductVersion>> Edit(ProductVersion productVersions) {
            if (productVersions.Id == Guid.Empty) {
                Redirect("/error");
            }

            await _storage.ProductVersions.Update(productVersions);
            return productVersions;
        }

        [HttpDelete("delete")]
        public async Task<ActionResult> Delete(ProductVersion productVersions) {
            if (productVersions.Id == Guid.Empty) {
                return NotFound();
            }
            await _storage.ProductVersions.Remove(productVersions);

            return Ok();
        }
    }
}
