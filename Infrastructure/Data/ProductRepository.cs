using Core.Interfaces;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext _context;

        public ProductRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<Product> GetProductbyIdAsync(int id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await _context.Products
                .Include(p => p.ProductType)
                .Include(p => p.ProductBrand)
                .FirstOrDefaultAsync(p => p.Id == id);
#pragma warning restore CS8603 // Possible null reference return.
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            return await _context.Products
                .Include(p => p.ProductType)
                .Include(p => p.ProductBrand)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductsBrandsAsync()
        {
            return await _context.ProductBrands.ToListAsync();
        }

        public async Task<IReadOnlyList<ProductType>> GetProductsTypesAsync()
        {
            return await _context.ProductTypes.ToListAsync();
        }
    }
}
