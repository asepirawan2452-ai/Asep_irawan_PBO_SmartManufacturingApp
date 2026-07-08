using SmartManufacturingApp.ViewModels;

namespace SmartManufacturingApp.Services.Interfaces;

public interface IReportExcelService
{
    byte[] GenerateProductionReport(
        List<ProductionReportViewModel> data);
}