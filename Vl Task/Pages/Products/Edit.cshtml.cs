using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Vl_Task.Pages.Products
{
    public class EditModel : PageModel
    {
        private readonly Storage _storage;
        public EditModel(Storage storage) {
            _storage = storage;
        }
        [BindProperty]
        public Product Product { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _storage.Products.Get(id);
            if (product == null)
            {
                return NotFound();
            }
            Product = await product;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _storage.Products.Update(Product);

            return RedirectToPage("./Index");
        }

        private bool ProductExists(Guid id)
        {
          return _storage.Products.Exists(id);
        }
    }
}
