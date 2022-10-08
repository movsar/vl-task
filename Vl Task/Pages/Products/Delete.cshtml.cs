﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Vl_Task.Data;
using Vl_Task.Models;

namespace Vl_Task.Pages.Products
{
    public class DeleteModel : PageModel
    {
        private readonly Vl_Task.Data.WarehouseContext _context;

        public DeleteModel(Vl_Task.Data.WarehouseContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Product Product { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FirstOrDefaultAsync(m => m.Id == id);

            if (product == null)
            {
                return NotFound();
            }
            else 
            {
                Product = product;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products.FindAsync(id);

            if (product != null)
            {
                Product = product;
                _context.Products.Remove(Product);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
