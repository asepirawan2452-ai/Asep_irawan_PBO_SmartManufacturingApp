using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmartManufacturingApp.Data;
using SmartManufacturingApp.Models;
using SmartManufacturingApp.Services.Interfaces;
using SmartManufacturingApp.ViewModels;

namespace SmartManufacturingApp.Services;

public class ProductionOrderService : IProductionOrderService
{
    private readonly ApplicationDbContext _context;

    public ProductionOrderService(ApplicationDbContext context)
    {
        _context = context;
    }

    // =========================
    // GET ALL
    // =========================
    public async Task<List<ProductionOrder>> GetAllAsync()
    {
        return await _context.ProductionOrders
            .Include(x => x.Machine)
            .Include(x => x.OperatorEmployee)
            .Include(x => x.Details)
                .ThenInclude(x => x.Product)
            .OrderByDescending(x => x.ProductionDate)
            .ToListAsync();
    }

    // =========================
    // GET BY ID
    // =========================
    public async Task<ProductionOrder?> GetByIdAsync(Guid id)
    {
        return await _context.ProductionOrders
            .Include(x => x.Details)
                .ThenInclude(x => x.Product)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    // =========================
    // CREATE VIEW MODEL
    // =========================
    public async Task<ProductionOrderViewModel> GetCreateViewModelAsync()
    {
        var vm = new ProductionOrderViewModel();

        vm.Machines = await _context.Machines
            .Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.MachineName
            })
            .ToListAsync();

        vm.Operators = await _context.Operators
            .Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.FullName
            })
            .ToListAsync();

        var products = await _context.Products
            .Where(x => x.IsActive)
            .Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.ProductName
            })
            .ToListAsync();

        vm.Products = products;

        vm.Details.Add(new ProductionOrderDetailViewModel
        {
            Products = products
        });

        return vm;
    }

    // =========================
    // CREATE
    // =========================
    public async Task CreateAsync(ProductionOrderViewModel model)
    {
        var order = new ProductionOrder
        {
            Id = Guid.NewGuid(),
            WorkOrderNumber = model.WorkOrderNumber,
            ProductionDate = model.ProductionDate,
            MachineId = model.MachineId,
            OperatorEmployeeId = model.OperatorEmployeeId
        };

        foreach (var item in model.Details)
        {
            order.Details.Add(new ProductionOrderDetail
            {
                ProductId = item.ProductId,
                TargetQty = item.TargetQty,
                ActualQty = 0,
                RejectQty = 0
            });
        }

        _context.ProductionOrders.Add(order);
        await _context.SaveChangesAsync();
    }

    // =========================
    // EDIT VIEW MODEL
    // =========================
    public async Task<ProductionOrderViewModel?> GetEditViewModelAsync(Guid id)
    {
        var data = await _context.ProductionOrders
            .Include(x => x.Details)
            .ThenInclude(x => x.Product)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (data == null)
            return null;

        var vm = new ProductionOrderViewModel
        {
            Id = data.Id,
            WorkOrderNumber = data.WorkOrderNumber,
            ProductionDate = data.ProductionDate,
            MachineId = data.MachineId,
            OperatorEmployeeId = data.OperatorEmployeeId
        };

        vm.Machines = await _context.Machines
            .Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.MachineName
            })
            .ToListAsync();

        vm.Operators = await _context.Operators
            .Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.FullName
            })
            .ToListAsync();

        var products = await _context.Products
            .Where(x => x.IsActive)
            .Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.ProductName
            })
            .ToListAsync();

        vm.Products = products;

        vm.Details = data.Details.Select(d => new ProductionOrderDetailViewModel
        {
            ProductId = d.ProductId,
            TargetQty = d.TargetQty,
            ActualQty = d.ActualQty,
            RejectQty = d.RejectQty,
            Products = products
        }).ToList();

        return vm;
    }

    // =========================
    // EDIT RESULT VIEW MODEL (🔥 FIX PENTING)
    // =========================
    public async Task<ProductionOrderViewModel?> GetEditResultViewModelAsync(Guid id)
    {
        var data = await _context.ProductionOrders
            .Include(x => x.Details)
            .ThenInclude(x => x.Product)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (data == null)
            return null;

        var vm = new ProductionOrderViewModel
        {
            Id = data.Id,
            WorkOrderNumber = data.WorkOrderNumber,
            ProductionDate = data.ProductionDate,
            MachineId = data.MachineId,
            OperatorEmployeeId = data.OperatorEmployeeId,

            Details = data.Details.Select(d => new ProductionOrderDetailViewModel
            {
                ProductId = d.ProductId,
                ProductName = d.Product?.ProductName ?? "",
                TargetQty = d.TargetQty,
                ActualQty = d.ActualQty,
                RejectQty = d.RejectQty
            }).ToList()
        };

        return vm;
    }

    // =========================
    // UPDATE (EDIT)
    // =========================
    public async Task UpdateAsync(ProductionOrderViewModel model)
    {
        var data = await _context.ProductionOrders
            .Include(x => x.Details)
            .FirstOrDefaultAsync(x => x.Id == model.Id);

        if (data == null)
            throw new Exception("Data tidak ditemukan");

        data.WorkOrderNumber = model.WorkOrderNumber;
        data.ProductionDate = model.ProductionDate;
        data.MachineId = model.MachineId;
        data.OperatorEmployeeId = model.OperatorEmployeeId;

        _context.ProductionOrderDetails.RemoveRange(data.Details);

        data.Details = model.Details.Select(x => new ProductionOrderDetail
        {
            ProductId = x.ProductId,
            TargetQty = x.TargetQty,
            ActualQty = x.ActualQty,
            RejectQty = x.RejectQty
        }).ToList();

        await _context.SaveChangesAsync();
    }

    // =========================
    // UPDATE RESULT (🔥 FIX FINAL AMAN)
    // =========================
    public async Task UpdateProductionResultAsync(ProductionOrderViewModel model)
    {
        var order = await _context.ProductionOrders
            .Include(x => x.Details)
            .FirstOrDefaultAsync(x => x.Id == model.Id);

        if (order == null || model.Details == null)
            return;

        var details = order.Details.ToList();

        for (int i = 0; i < details.Count; i++)
        {
            if (i < model.Details.Count)
            {
                details[i].ActualQty = model.Details[i].ActualQty;
                details[i].RejectQty = model.Details[i].RejectQty;
            }
        }

        await _context.SaveChangesAsync();
    }

    // =========================
    // DELETE
    // =========================
    public async Task DeleteAsync(Guid id)
    {
        var order = await _context.ProductionOrders
            .Include(x => x.Details)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (order == null)
            return;

        _context.ProductionOrders.Remove(order);
        await _context.SaveChangesAsync();
    }
}