using SmartManufacturingApp.ViewModels;

namespace SmartManufacturingApp.Services.Interfaces;

public interface IReportService
{
    Task<List<ProductionReportViewModel>> GetProductionReportAsync(
        DateTime? startDate,
        DateTime? endDate);
}