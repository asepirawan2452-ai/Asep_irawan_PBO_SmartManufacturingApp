using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using SmartManufacturingApp.Services.Interfaces;
using SmartManufacturingApp.ViewModels;

namespace SmartManufacturingApp.Services;

public class ReportPdfService : IReportPdfService
{
    public byte[] GenerateProductionReport(
        List<ProductionReportViewModel> data)
    {
        QuestPDF.Settings.License = LicenseType.Community;

        return Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(30);

                page.Header()
                    .Text("SMART MANUFACTURING REPORT")
                    .FontSize(20)
                    .Bold();

                page.Content()
                    .Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                        });

                        table.Header(header =>
                        {
                            header.Cell().Text("WO");
                            header.Cell().Text("Machine");
                            header.Cell().Text("Product");
                            header.Cell().Text("Actual");
                            header.Cell().Text("Efficiency");
                        });

                        foreach (var item in data)
                        {
                            table.Cell().Text(item.WorkOrderNumber);
                            table.Cell().Text(item.MachineName);
                            table.Cell().Text(item.ProductName);
                            table.Cell().Text(item.ActualQty.ToString());
                            table.Cell().Text($"{item.Efficiency:N2}%");
                        }
                    });

                page.Footer()
                    .AlignCenter()
                    .Text(x =>
                    {
                        x.Span("Generated : ");
                        x.Span(DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
                    });
            });
        }).GeneratePdf();
    }
}