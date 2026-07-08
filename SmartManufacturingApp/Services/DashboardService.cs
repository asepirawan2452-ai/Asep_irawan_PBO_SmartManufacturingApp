using Microsoft.EntityFrameworkCore;
using SmartManufacturingApp.Data;
using SmartManufacturingApp.Models.Enums;
using SmartManufacturingApp.Services.Interfaces;
using SmartManufacturingApp.ViewModels;

namespace SmartManufacturingApp.Services;

public class DashboardService : IDashboardService
{
    private readonly ApplicationDbContext _context;

    public DashboardService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<DashboardViewModel> GetDashboardAsync()
    {
        var model = new DashboardViewModel();

        // Total Master Data
        model.TotalMachines =
            await _context.Machines.CountAsync();

        model.TotalOperators =
            await _context.Operators.CountAsync();

        model.TotalProducts =
            await _context.Products.CountAsync();

        model.TotalWorkOrders =
            await _context.ProductionOrders.CountAsync();

        // Total Produksi
        model.TotalTargetQty =
            await _context.ProductionOrderDetails
                .SumAsync(x => x.TargetQty);

        model.TotalActualQty =
            await _context.ProductionOrderDetails
                .SumAsync(x => x.ActualQty);

        model.TotalRejectQty =
            await _context.ProductionOrderDetails
                .SumAsync(x => x.RejectQty);

        if (model.TotalTargetQty > 0)
        {
            model.OverallEfficiency =
                Math.Round(
                    (decimal)model.TotalActualQty /
                    model.TotalTargetQty * 100,
                    2);
        }
        else
        {
            model.OverallEfficiency = 0;
        }

        // Status Machine
        model.RunningMachine =
            await _context.Machines.CountAsync(x =>
                x.Status == MachineStatus.Running);

        model.IdleMachine =
            await _context.Machines.CountAsync(x =>
                x.Status == MachineStatus.Idle);

        model.MaintenanceMachine =
            await _context.Machines.CountAsync(x =>
                x.Status == MachineStatus.Maintenance);

        // ===========================
        // Chart Produksi 7 Hari
        // ===========================

        var orders = await _context.ProductionOrders
            .Include(x => x.Details)
            .OrderBy(x => x.ProductionDate)
            .ToListAsync();

        var production = orders
            .GroupBy(x => x.ProductionDate.Date)
            .TakeLast(7);

        foreach (var item in production)
        {
            model.ProductionLabels.Add(
                item.Key.ToString("dd/MM"));

            model.ProductionData.Add(
                item.Sum(x =>
                    x.Details.Sum(d => d.ActualQty)));
        }

        return model;
    }
}
