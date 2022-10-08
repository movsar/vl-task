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
    public class IndexModel : PageModel
    {
        private readonly Vl_Task.Data.WarehouseContext _context;

        public IndexModel(Vl_Task.Data.WarehouseContext context)
        {
            _context = context;
        }

        public IList<ProductVersion> ProductVersion { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.ProductVersions != null)
            {
                ProductVersion = await _context.ProductVersions.ToListAsync();
            }
        }
    }
}
