using Microsoft.EntityFrameworkCore;
using NHManager.Blazor.Data;
using NHManager.Blazor.Models;

namespace NHManager.Blazor.Services;

public interface IProductService
{
    Task<List<Product>> GetAllAsync(bool activeOnly = false);
    Task<Product?> GetByIdAsync(int id);
    Task<Product> CreateAsync(Product product, string userName);
    Task<Product> UpdateAsync(Product product, string userName);
    Task DeleteAsync(int id, string userName);
}

public class ProductService : IProductService
{
    private readonly AppDbContext _context;

    public ProductService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Product>> GetAllAsync(bool activeOnly = false)
    {
        var query = _context.Products.Where(p => p.Valid);

        if (activeOnly)
        {
            query = query.Where(p => p.Active);
        }

        return await query.OrderBy(p => p.Name).ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _context.Products
            .FirstOrDefaultAsync(p => p.Id == id && p.Valid);
    }

    public async Task<Product> CreateAsync(Product product, string userName)
    {
        product.CreatedAt = DateTime.Now;
        product.UpdatedAt = DateTime.Now;
        product.CreatedBy = userName;
        product.UpdatedBy = userName;
        product.Valid = true;

        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<Product> UpdateAsync(Product product, string userName)
    {
        product.UpdatedAt = DateTime.Now;
        product.UpdatedBy = userName;

        _context.Products.Update(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task DeleteAsync(int id, string userName)
    {
        var product = await _context.Products.FindAsync(id);
        if (product != null)
        {
            product.Valid = false;
            product.UpdatedAt = DateTime.Now;
            product.UpdatedBy = userName;
            await _context.SaveChangesAsync();
        }
    }
}
