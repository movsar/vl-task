using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vl_Task.Data;
using Vl_Task.Models;

namespace Vl_Task.Pages.ProductVersions
{
    public class EditModel : PageModel
    {
        private readonly Vl_Task.Data.WarehouseContext _context;

        public EditModel(Vl_Task.Data.WarehouseContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ProductVersion ProductVersion { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.ProductVersion == null)
            {
                return NotFound();
            }

            var productversion =  await _context.ProductVersion.FirstOrDefaultAsync(m => m.Id == id);
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

            _context.Attach(ProductVersion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductVersionExists(ProductVersion.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ProductVersionExists(Guid id)
        {
          return _context.ProductVersion.Any(e => e.Id == id);
        }
    }
}
