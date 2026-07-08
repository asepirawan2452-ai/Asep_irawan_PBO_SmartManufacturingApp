using SmartManufacturingApp.Models;

namespace SmartManufacturingApp.Services.Interfaces;

public interface IMachineService
{
    Task<List<Machine>> GetAllAsync(string? keyword);

    Task<Machine?> GetByIdAsync(Guid id);

    Task CreateAsync(Machine machine);

    Task UpdateAsync(Machine machine);

    Task DeleteAsync(Guid id);
}