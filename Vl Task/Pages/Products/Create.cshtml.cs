using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Vl_Task.Pages.Products
{
    public class CreateModel : PageModel
    {
        private readonly Storage _storage;
        public CreateModel(Storage storage)
        {
            _storage = storage;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; }
        
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }
            await _storage.Products.Add(Product);

            return RedirectToPage("./Index");
        }
    }
}
