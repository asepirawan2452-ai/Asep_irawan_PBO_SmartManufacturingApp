using Microsoft.AspNetCore.Mvc;
using SmartManufacturingApp.Services.Interfaces;

namespace SmartManufacturingApp.Controllers;

public class ReportController : Controller
{
    private readonly IReportService _service;
    private readonly IReportPdfService _pdfService;
    private readonly IReportExcelService _excelService;

    public ReportController(
        IReportService service,
        IReportPdfService pdfService,
        IReportExcelService excelService)
    {
        _service = service;
        _pdfService = pdfService;
        _excelService = excelService;
    }

    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> Daily(DateTime? date)
    {
        var start = date?.Date;
        var end = start?.AddDays(1).AddSeconds(-1);

        var data = await _service.GetProductionReportAsync(start, end);

        ViewBag.Date = date;

        return View(data);
    }

    public async Task<IActionResult> Monthly(int? month, int? year)
    {
        month ??= DateTime.Today.Month;
        year ??= DateTime.Today.Year;

        var start = new DateTime(year.Value, month.Value, 1);
        var end = start.AddMonths(1).AddSeconds(-1);

        var data = await _service.GetProductionReportAsync(start, end);

        ViewBag.Month = month;
        ViewBag.Year = year;

        return View(data);
    }

    public async Task<IActionResult> Efficiency()
    {
        var data = await _service.GetProductionReportAsync(null, null);

        return View(data);
    }

    public async Task<IActionResult> ExportPdf(
        DateTime? startDate,
        DateTime? endDate)
    {
        var data = await _service.GetProductionReportAsync(startDate, endDate);

        var pdf = _pdfService.GenerateProductionReport(data);

        return File(
            pdf,
            "application/pdf",
            $"ProductionReport_{DateTime.Now:yyyyMMdd}.pdf");
    }

    public async Task<IActionResult> ExportExcel(
        DateTime? startDate,
        DateTime? endDate)
    {
        var data = await _service.GetProductionReportAsync(startDate, endDate);

        var excel = _excelService.GenerateProductionReport(data);

        return File(
            excel,
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            $"ProductionReport_{DateTime.Now:yyyyMMdd}.xlsx");
    }
}