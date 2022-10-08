using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Vl_Task.Pages.Products {
    public class IndexModel : PageModel {
        private readonly Storage _storage;
        public IndexModel(Storage storage) {
            _storage = storage;
        }

        public IList<Product> Products { get; set; } = default!;

        public async Task OnGetAsync() {
            Products = await _storage.Products.ToListAsync();
        }

    }
}
