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
    public class IndexModel : PageModel
    {
        private readonly Vl_Task.Data.WarehouseContext _context;

        public IndexModel(Vl_Task.Data.WarehouseContext context)
        {
            _context = context;
        }

        public IList<Product> Products { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Products != null)
            {
                Products = await _context.Products.ToListAsync();
            }
        }
    }
}
