using SmartManufacturingApp.ViewModels;

namespace SmartManufacturingApp.Services.Interfaces;

public interface IReportPdfService
{
    byte[] GenerateProductionReport(
        List<ProductionReportViewModel> data);
}