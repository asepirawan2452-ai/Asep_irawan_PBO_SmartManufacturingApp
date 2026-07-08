using SmartManufacturingApp.Models;

namespace SmartManufacturingApp.Services.Interfaces;

public interface IOperatorService
{
    Task<List<OperatorEmployee>> GetAllAsync(string? keyword);

    Task<OperatorEmployee?> GetByIdAsync(Guid id);

    Task CreateAsync(OperatorEmployee employee);

    Task UpdateAsync(OperatorEmployee employee);

    Task DeleteAsync(Guid id);
}