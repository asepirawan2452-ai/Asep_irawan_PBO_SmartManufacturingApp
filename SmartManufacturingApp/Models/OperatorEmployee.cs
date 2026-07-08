using System.ComponentModel.DataAnnotations;

namespace SmartManufacturingApp.Models;

public class OperatorEmployee : BaseEntity
{
    [Required]
    [StringLength(20)]
    public string EmployeeCode { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string Department { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;
}