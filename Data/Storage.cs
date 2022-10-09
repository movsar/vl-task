using Data.Models;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Data {
    public class Storage {
        public ProductRepository Products { get; }
        public ProductVersionsRepository ProductVersions { get; }

        private WarehouseContext _context;
        public Storage(WarehouseContext context, ProductRepository productsRepository, ProductVersionsRepository productVersionsRepository) {
            _context = context;
            Products = productsRepository;
            ProductVersions = productVersionsRepository;
        }

        public async Task<int> SaveChangesAsync() {
            return await _context.SaveChangesAsync();
        }
    }
}