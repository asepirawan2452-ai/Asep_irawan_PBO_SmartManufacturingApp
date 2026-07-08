using Microsoft.EntityFrameworkCore;
using SmartManufacturingApp.Data;
using SmartManufacturingApp.Models;
using SmartManufacturingApp.Services.Interfaces;

namespace SmartManufacturingApp.Services;

public class ProductService : IProductService
{
    private readonly ApplicationDbContext _context;

    public ProductService(ApplicationDbContext context)
    {
        _context = context;
    }

    // =========================
    // GET ALL
    // =========================
    public async Task<List<Product>> GetAllAsync(string? keyword)
    {
        var query = _context.Products
            .AsNoTracking() // 🔥 lebih cepat (read only)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(keyword))
        {
            query = query.Where(x =>
                x.ProductCode.Contains(keyword) ||
                x.ProductName.Contains(keyword) ||
                x.Category.Contains(keyword));
        }

        return await query
            .OrderBy(x => x.ProductName)
            .ToListAsync();
    }

    // =========================
    // GET BY ID
    // =========================
    public async Task<Product?> GetByIdAsync(Guid id)
    {
        return await _context.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    // =========================
    // CREATE
    // =========================
    public async Task CreateAsync(Product product)
    {
        product.Id = Guid.NewGuid(); // 🔥 pastikan ID aman
        product.CreatedAt = DateTime.Now;
        product.UpdatedAt = DateTime.Now;

        _context.Products.Add(product);

        await _context.SaveChangesAsync();
    }

    // =========================
    // UPDATE (FIXED)
    // =========================
    public async Task UpdateAsync(Product product)
    {
        var existing = await _context.Products
            .FirstOrDefaultAsync(x => x.Id == product.Id);

        if (existing == null)
            throw new Exception("Product tidak ditemukan");

        // 🔥 mapping manual (AMAN)
        existing.ProductCode = product.ProductCode;
        existing.ProductName = product.ProductName;
        existing.Category = product.Category;
        existing.Unit = product.Unit;
        existing.StandardCost = product.StandardCost;
        existing.IsActive = product.IsActive;
        existing.UpdatedAt = DateTime.Now;

        await _context.SaveChangesAsync();
    }

    // =========================
    // DELETE
    // =========================
    public async Task DeleteAsync(Guid id)
    {
        var data = await _context.Products
            .FirstOrDefaultAsync(x => x.Id == id);

        if (data == null)
            return;

        _context.Products.Remove(data);

        await _context.SaveChangesAsync();
    }
}