using System.ComponentModel.DataAnnotations;
using SmartManufacturingApp.Models.Enums;

namespace SmartManufacturingApp.Models;

public class ProductionOrder : BaseEntity
{
    [Required]
    [Display(Name = "Work Order Number")]
    public string WorkOrderNumber { get; set; } = string.Empty;

    [Required]
    public DateTime ProductionDate { get; set; } = DateTime.Today;

    [Required]
    public Guid MachineId { get; set; }

    public Machine? Machine { get; set; }

    [Required]
    public Guid OperatorEmployeeId { get; set; }

    public OperatorEmployee? OperatorEmployee { get; set; }

    public WorkOrderStatus Status { get; set; } = WorkOrderStatus.Waiting;

    public ICollection<ProductionOrderDetail> Details { get; set; }
        = new List<ProductionOrderDetail>();
}