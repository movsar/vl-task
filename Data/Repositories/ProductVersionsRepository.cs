using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories {
    public class ProductVersionsRepository {
        private readonly WarehouseContext _context;

        public ProductVersionsRepository(WarehouseContext context) {
            _context = context;
        }

        public IQueryable<ProductVersionSearchResult> Search(string productName, string productVersionName, float minVolume, float maxVolume) {
            string query = $"SELECT * FROM [dbo].[ProductVersion_Search_Func] ('{productName}', '{productVersionName}', {minVolume}, {maxVolume})";
            
            return _context.ProductVersionSearchResult.FromSqlRaw(query);
        }



        public async Task Add(ProductVersion ProductVersion) {
            _context.ProductVersions.Add(ProductVersion);
            await _context.SaveChangesAsync();
        }
        public async Task<ProductVersion> Get(Guid? id) {
            return await _context.ProductVersions.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task Update(ProductVersion ProductVersion) {
            _context.Attach(ProductVersion).State = EntityState.Modified;
            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                if (!Exists(ProductVersion.Id)) {
                    throw new Exception($"ProductVersion with {ProductVersion.Id} doesn't exist");
                } else {
                    throw;
                }
            }
        }

        public async Task Remove(ProductVersion ProductVersion) {
            _context.Remove(ProductVersion);
            await _context.SaveChangesAsync();
        }

        public bool Exists(Guid id) {
            return _context.ProductVersions.Any(e => e.Id == id);
        }

        public async Task<List<ProductVersion>> ToListAsync() {
            return await _context.ProductVersions.ToListAsync();
        }

        public async Task<IList<ProductVersion>> GetByNames(IQueryable<string> productVersionNames) {
            return await _context.ProductVersions.Where(pv => productVersionNames.Contains(pv.Name)).ToListAsync();
        }
    }
}
