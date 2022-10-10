using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Pages.ProductVersions
{
    public class CreateModel : PageModel
    {
        private readonly Storage _storage;
        public CreateModel(Storage storage) {
            _storage = storage;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public ProductVersion ProductVersion { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            _storage.ProductVersions.Add(ProductVersion);
            await _storage.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
