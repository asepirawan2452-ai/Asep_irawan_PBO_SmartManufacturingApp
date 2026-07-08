using Microsoft.EntityFrameworkCore;
using SmartManufacturingApp.Data;
using SmartManufacturingApp.Services.Interfaces;
using SmartManufacturingApp.ViewModels;

namespace SmartManufacturingApp.Services;

public class ReportService : IReportService
{
    private readonly ApplicationDbContext _context;

    public ReportService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ProductionReportViewModel>> GetProductionReportAsync(
        DateTime? startDate,
        DateTime? endDate)
    {
        var query = _context.ProductionOrderDetails
            .Include(x => x.Product)
            .Include(x => x.ProductionOrder!)
                .ThenInclude(x => x.Machine)
            .Include(x => x.ProductionOrder!)
                .ThenInclude(x => x.OperatorEmployee)
            .AsQueryable();

        if (startDate.HasValue)
        {
            query = query.Where(x =>
                x.ProductionOrder!.ProductionDate >= startDate.Value);
        }

        if (endDate.HasValue)
        {
            query = query.Where(x =>
                x.ProductionOrder!.ProductionDate <= endDate.Value);
        }

        return await query
            .OrderByDescending(x => x.ProductionOrder!.ProductionDate)
            .Select(x => new ProductionReportViewModel
            {
                WorkOrderNumber = x.ProductionOrder!.WorkOrderNumber,

                ProductionDate = x.ProductionOrder.ProductionDate,

                MachineName = x.ProductionOrder.Machine!.MachineName,

                OperatorName = x.ProductionOrder.OperatorEmployee!.FullName,

                TargetQty = x.TargetQty,

                ActualQty = x.ActualQty,

                RejectQty = x.RejectQty,

                Efficiency = x.TargetQty == 0
                    ? 0
                    : ((decimal)x.ActualQty / x.TargetQty) * 100
            })
            .ToListAsync();
    }
}