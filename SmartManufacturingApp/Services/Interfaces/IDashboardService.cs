using SmartManufacturingApp.ViewModels;

namespace SmartManufacturingApp.Services.Interfaces;

public interface IDashboardService
{
    Task<DashboardViewModel> GetDashboardAsync();
}
