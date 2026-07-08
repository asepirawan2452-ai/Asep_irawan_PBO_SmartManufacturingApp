using System.ComponentModel.DataAnnotations;
using SmartManufacturingApp.Models.Enums;

namespace SmartManufacturingApp.Models;

public class Machine : BaseEntity
{
    [Required]
    [StringLength(20)]
    [Display(Name = "Machine Code")]
    public string MachineCode { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    [Display(Name = "Machine Name")]
    public string MachineName { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string Location { get; set; } = string.Empty;

    [Required]
    public MachineStatus Status { get; set; } = MachineStatus.Running;

    public decimal Temperature { get; set; }

    public decimal SpeedRPM { get; set; }

    public decimal Utilization { get; set; }

    public DateTime LastMaintenance { get; set; } = DateTime.Today;

    public DateTime NextMaintenance { get; set; } = DateTime.Today.AddMonths(1);
}