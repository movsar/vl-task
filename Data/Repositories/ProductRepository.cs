using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories {
    public class ProductRepository {
        private readonly WarehouseContext _context;

        public ProductRepository(WarehouseContext context) {
            _context = context;
        }

        public async Task Add(Product product) {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }
        public async Task<Product> Get(Guid? id) {
            return await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task Update(Product product) {
            _context.Attach(product).State = EntityState.Modified;
            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                if (!Exists(product.Id)) {
                    throw new Exception($"product with {product.Id} doesn't exist");
                } else {
                    throw;
                }
            }
        }

        public async Task Remove(Product product) {
            _context.Remove(product);
            await _context.SaveChangesAsync();
        }

        public bool Exists(Guid id) {
            return _context.Products.Any(e => e.Id == id);
        }

        public async Task<List<Product>> ToListAsync() {
            return await _context.Products.ToListAsync();
        }
    }
}
