using System.ComponentModel.DataAnnotations;

namespace SmartManufacturingApp.Models;

public class Product : BaseEntity
{
    [Required]
    [StringLength(20)]
    [Display(Name = "Product Code")]
    public string ProductCode { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    [Display(Name = "Product Name")]
    public string ProductName { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string Category { get; set; } = string.Empty;

    [Required]
    [StringLength(20)]
    public string Unit { get; set; } = "PCS";

    [Range(0,1000000)]
    [Display(Name="Standard Cost")]
    public decimal StandardCost { get; set; }

    public bool IsActive { get; set; } = true;
}