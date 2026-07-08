using System;

namespace SmartManufacturingApp.ViewModels;

public class ProductionReportViewModel
{
    public string WorkOrderNumber { get; set; } = string.Empty;

    public DateTime ProductionDate { get; set; }

    public string MachineName { get; set; } = string.Empty;

    public string OperatorName { get; set; } = string.Empty;

    // WAJIB ADA
    public string ProductName { get; set; } = string.Empty;

    public int TargetQty { get; set; }

    public int ActualQty { get; set; }

    public int RejectQty { get; set; }

    public decimal Efficiency { get; set; }
}