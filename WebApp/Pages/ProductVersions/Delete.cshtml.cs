using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages.ProductVersions
{
    public class DeleteModel : PageModel
    {
        private readonly Storage _storage;
        public DeleteModel(Storage storage) {
            _storage = storage;
        }

        [BindProperty]
      public ProductVersion ProductVersion { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _storage.ProductVersions == null)
            {
                return NotFound();
            }

            var productversion = await _storage.ProductVersions.Get(id);

            if (productversion == null)
            {
                return NotFound();
            }
            else 
            {
                ProductVersion = productversion;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var productversion = await _storage.ProductVersions.Get(id);

            if (productversion != null)
            {
                ProductVersion = productversion;
                await _storage.ProductVersions.Remove(ProductVersion);
            }

            return RedirectToPage("./Index");
        }
    }
}
