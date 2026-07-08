using SmartManufacturingApp.Models;

namespace SmartManufacturingApp.Services.Interfaces;

public interface IProductService
{
    Task<List<Product>> GetAllAsync(string? keyword);

    Task<Product?> GetByIdAsync(Guid id);

    Task CreateAsync(Product product);

    Task UpdateAsync(Product product);

    Task DeleteAsync(Guid id);
}