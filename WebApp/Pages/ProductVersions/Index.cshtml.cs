using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages.ProductVersions {
    public class IndexModel : PageModel {
        private readonly Storage _storage;
        public IndexModel(Storage storage) {
            _storage = storage;
        }

        public IList<ProductVersion> ProductVersions { get; set; } = default!;

        public async Task OnGetAsync() {
            ProductVersions = await _storage.ProductVersions.ToListAsync();
        }
        public async Task OnPostAsync() {
            var productName = Request.Form["productName"].ToString();
            var productVersionName = Request.Form["productVersionName"].ToString();
            var minVolumeAsString = float.TryParse(Request.Form["minVolume"], out float minVolume);
            var maxVolumeAsString = float.TryParse(Request.Form["maxVolume"], out float maxVolume);

            string query = $"SELECT * FROM [dbo].[ProductVersion_Search_Func] ('{productName}', '{productVersionName}', {minVolume}, {maxVolume})";
            var productVersionNames = _storage.ProductVersions.Search(productName, productVersionName, minVolume, maxVolume).Select(p => p.ProductVersionName);
            ProductVersions = await _storage.ProductVersions.GetByNames(productVersionNames);
        }
    }
}