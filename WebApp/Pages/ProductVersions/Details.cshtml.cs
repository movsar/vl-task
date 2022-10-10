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
    public class DetailsModel : PageModel
    {
        private readonly Storage _storage;
        public DetailsModel(Storage storage) {
            _storage = storage;
        }

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
    }
}
