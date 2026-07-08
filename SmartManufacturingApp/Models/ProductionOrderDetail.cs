using System.ComponentModel.DataAnnotations;

namespace SmartManufacturingApp.Models;

public class ProductionOrderDetail : BaseEntity
{
    [Required]
    public Guid ProductionOrderId { get; set; }

    public ProductionOrder ProductionOrder { get; set; } = null!;

    [Required]
    public Guid ProductId { get; set; }

    public Product Product { get; set; } = null!;

    [Range(1, 100000)]
    public int TargetQty { get; set; }

    public int ActualQty { get; set; }

    public int RejectQty { get; set; }

    public decimal Efficiency
    {
        get
        {
            if (TargetQty == 0)
                return 0;

            return Math.Round(
                (decimal)ActualQty / TargetQty * 100,
                2);
        }
    }
}