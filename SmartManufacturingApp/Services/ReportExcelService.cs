using ClosedXML.Excel;
using SmartManufacturingApp.Services.Interfaces;
using SmartManufacturingApp.ViewModels;

namespace SmartManufacturingApp.Services;

public class ReportExcelService : IReportExcelService
{
    public byte[] GenerateProductionReport(
        List<ProductionReportViewModel> data)
    {
        using var workbook = new XLWorkbook();

        var worksheet = workbook.Worksheets.Add("Production Report");

        // Title
        worksheet.Cell("A1").Value = "SMART MANUFACTURING REPORT";
        worksheet.Range("A1:I1").Merge();
        worksheet.Cell("A1").Style.Font.Bold = true;
        worksheet.Cell("A1").Style.Font.FontSize = 16;
        worksheet.Cell("A1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

        worksheet.Cell("A2").Value = $"Generated : {DateTime.Now:dd/MM/yyyy HH:mm}";

        // Header
        worksheet.Cell(4, 1).Value = "No";
        worksheet.Cell(4, 2).Value = "Work Order";
        worksheet.Cell(4, 3).Value = "Date";
        worksheet.Cell(4, 4).Value = "Machine";
        worksheet.Cell(4, 5).Value = "Operator";
        worksheet.Cell(4, 6).Value = "Product";
        worksheet.Cell(4, 7).Value = "Target";
        worksheet.Cell(4, 8).Value = "Actual";
        worksheet.Cell(4, 9).Value = "Reject";
        worksheet.Cell(4, 10).Value = "Efficiency (%)";

        var header = worksheet.Range("A4:J4");

        header.Style.Font.Bold = true;
        header.Style.Fill.BackgroundColor = XLColor.DarkBlue;
        header.Style.Font.FontColor = XLColor.White;

        int row = 5;
        int no = 1;

        foreach (var item in data)
        {
            worksheet.Cell(row, 1).Value = no++;
            worksheet.Cell(row, 2).Value = item.WorkOrderNumber;
            worksheet.Cell(row, 3).Value = item.ProductionDate.ToString("dd/MM/yyyy");
            worksheet.Cell(row, 4).Value = item.MachineName;
            worksheet.Cell(row, 5).Value = item.OperatorName;
            worksheet.Cell(row, 6).Value = item.ProductName;
            worksheet.Cell(row, 7).Value = item.TargetQty;
            worksheet.Cell(row, 8).Value = item.ActualQty;
            worksheet.Cell(row, 9).Value = item.RejectQty;
            worksheet.Cell(row, 10).Value = item.Efficiency;
            row++;
        }

        worksheet.Columns().AdjustToContents();

        worksheet.Range($"A4:J{row - 1}")
                 .Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

        worksheet.Range($"A4:J{row - 1}")
                 .Style.Border.InsideBorder = XLBorderStyleValues.Thin;

        using var stream = new MemoryStream();

        workbook.SaveAs(stream);

        return stream.ToArray();
    }
}