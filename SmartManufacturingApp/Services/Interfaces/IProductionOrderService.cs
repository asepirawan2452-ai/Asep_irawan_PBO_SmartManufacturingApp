using SmartManufacturingApp.Models;
using SmartManufacturingApp.ViewModels;

namespace SmartManufacturingApp.Services.Interfaces;

public interface IProductionOrderService
{
    Task<List<ProductionOrder>> GetAllAsync();
    Task<ProductionOrder?> GetByIdAsync(Guid id);

    Task<ProductionOrderViewModel> GetCreateViewModelAsync();
    Task<ProductionOrderViewModel?> GetEditViewModelAsync(Guid id);

    Task<ProductionOrderViewModel?> GetEditResultViewModelAsync(Guid id); // 🔥 TAMBAHAN
    

    Task CreateAsync(ProductionOrderViewModel model);
    Task UpdateAsync(ProductionOrderViewModel model);

    Task UpdateProductionResultAsync(ProductionOrderViewModel model); // 🔥 FIX
    
    

    Task DeleteAsync(Guid id);
}