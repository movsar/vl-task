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
        public async Task OnPostAsync() {
            var productName = Request.Form["productName"].ToString();
            var productVersionName = Request.Form["productVersionName"].ToString();
            var minVolumeAsString = float.TryParse(Request.Form["minVolume"], out float minVolume);
            var maxVolumeAsString = float.TryParse(Request.Form["maxVolume"], out float maxVolume);
           
            string query = $"SELECT * FROM [dbo].[ProductVersion_Search_Func] ('{productName}', '{productVersionName}', {minVolume}, {maxVolume})";
            var searchResul = _context.ProductVersionSearchResult.FromSqlRaw(query).Select(p => p.ProductName).ToList();
          
            if (_context.Products != null) {
                Products = await _context.Products.ToListAsync();
            }
        }
    }
}
