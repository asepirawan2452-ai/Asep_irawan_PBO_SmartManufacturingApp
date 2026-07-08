namespace SmartManufacturingApp.ViewModels;

public class DashboardViewModel
{
    public int TotalMachines { get; set; }

    public int TotalOperators { get; set; }

    public int TotalProducts { get; set; }

    public int TotalWorkOrders { get; set; }

    public int TotalTargetQty { get; set; }

    public int TotalActualQty { get; set; }

    public int TotalRejectQty { get; set; }

    public decimal OverallEfficiency { get; set; }

    // Monitoring

    public int RunningMachine { get; set; }

    public int IdleMachine { get; set; }

    public int MaintenanceMachine { get; set; }
    public List<string> ProductionLabels { get; set; } = new();

    public List<int> ProductionData { get; set; } = new();
    
}
