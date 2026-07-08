using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SmartManufacturingApp.ViewModels;

public class ProductionOrderViewModel
{
    [Required]
    [Display(Name = "Work Order Number")]
    public string WorkOrderNumber { get; set; } = string.Empty;

    [Required]
    public DateTime ProductionDate { get; set; } = DateTime.Now;
    public string ProductName { get; set; } = "";

    [Required]
    public Guid MachineId { get; set; }

    [Required]
    public Guid OperatorEmployeeId { get; set; }

    public List<ProductionOrderDetailViewModel> Details { get; set; } = new();

    public List<SelectListItem> Machines { get; set; } = new();

    public List<SelectListItem> Operators { get; set; } = new();

    public List<SelectListItem> Products { get; set; } = new();
    
    public Guid Id { get; set; }
}