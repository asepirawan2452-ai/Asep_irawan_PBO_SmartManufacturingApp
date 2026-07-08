using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

public class ProductionOrderDetailViewModel
{
    [Required(ErrorMessage = "Product wajib dipilih")]
    public Guid ProductId { get; set; }

    [Range(1, 999999, ErrorMessage = "Target harus > 0")]
    public int TargetQty { get; set; }

    public int ActualQty { get; set; }

    public int RejectQty { get; set; }

    public List<SelectListItem> Products { get; set; } = new();
    public string ProductName { get; set; } = "";
    
}