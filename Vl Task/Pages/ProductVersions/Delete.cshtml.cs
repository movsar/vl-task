using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Vl_Task.Data;
using Vl_Task.Models;

namespace Vl_Task.Pages.ProductVersions
{
    public class DeleteModel : PageModel
    {
        private readonly Vl_Task.Data.WarehouseContext _context;

        public DeleteModel(Vl_Task.Data.WarehouseContext context)
        {
            _context = context;
        }

        [BindProperty]
      public ProductVersion ProductVersion { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.ProductVersions == null)
            {
                return NotFound();
            }

            var productversion = await _context.ProductVersions.FirstOrDefaultAsync(m => m.Id == id);

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
            if (id == null || _context.ProductVersions == null)
            {
                return NotFound();
            }
            var productversion = await _context.ProductVersions.FindAsync(id);

            if (productversion != null)
            {
                ProductVersion = productversion;
                _context.ProductVersions.Remove(ProductVersion);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
