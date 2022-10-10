using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages.Products {
    public class DeleteModel : PageModel {
        private readonly Storage _storage;
        public DeleteModel(Storage storage) {
            _storage = storage;
        }
        [BindProperty]
        public Product Product { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id) {
            if (id == null) {
                return NotFound();
            }

            var product = await _storage.Products.Get(id);

            if (product == null) {
                return NotFound();
            } else {
                Product = product;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id) {
            if (id == null || _storage.Products == null) {
                return NotFound();
            }
            var product = await _storage.Products.Get(id);

            if (product != null) {
                Product = product;
                await _storage.Products.Remove(Product);
            }

            return RedirectToPage("./Index");
        }
    }
}
