using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.ProductVersions
{
    public class EditModel : PageModel
    {
        private readonly Storage _storage;
        public EditModel(Storage storage) {
            _storage = storage;
        }
        [BindProperty]
        public ProductVersion ProductVersion { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productversion =  await _storage.ProductVersions.Get(id);
            if (productversion == null)
            {
                return NotFound();
            }
            ProductVersion = productversion;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _storage.ProductVersions.Update(ProductVersion);
            return RedirectToPage("./Index");
        }

        private bool ProductVersionExists(Guid id)
        {
          return _storage.ProductVersions.Exists(id);
        }
    }
}
