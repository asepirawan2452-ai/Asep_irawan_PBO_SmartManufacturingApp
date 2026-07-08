using Microsoft.AspNetCore.Mvc;
using SmartManufacturingApp.Services.Interfaces;
using SmartManufacturingApp.ViewModels;

namespace SmartManufacturingApp.Controllers;

public class ProductionOrderController : Controller
{
    private readonly IProductionOrderService _service;

    public ProductionOrderController(IProductionOrderService service)
    {
        _service = service;
    }

    // ==========================
    // INDEX
    // ==========================
    public async Task<IActionResult> Index()
    {
        var data = await _service.GetAllAsync();
        return View(data);
    }

    // ==========================
    // CREATE (GET)
    // ==========================
    public async Task<IActionResult> Create()
    {
        var model = await _service.GetCreateViewModelAsync();
        return View(model);
    }

    // ==========================
    // CREATE (POST)
    // ==========================
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProductionOrderViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var vm = await _service.GetCreateViewModelAsync();

            model.Machines = vm.Machines;
            model.Operators = vm.Operators;
            model.Products = vm.Products;

            // 🔥 isi ulang dropdown detail
            foreach (var item in model.Details)
            {
                item.Products = vm.Products;
            }

            return View(model);
        }

        await _service.CreateAsync(model);

        TempData["Success"] = "Production Order berhasil dibuat.";
        return RedirectToAction(nameof(Index));
    }

    // ==========================
    // DETAILS
    // ==========================
    public async Task<IActionResult> Details(Guid id)
    {
        var data = await _service.GetByIdAsync(id);

        if (data == null)
            return NotFound();

        return View(data);
    }

    // ==========================
    // DELETE
    // ==========================
    public async Task<IActionResult> Delete(Guid id)
    {
        var data = await _service.GetByIdAsync(id);

        if (data == null)
            return NotFound();

        return View(data);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        await _service.DeleteAsync(id);

        TempData["Success"] = "Production Order berhasil dihapus.";
        return RedirectToAction(nameof(Index));
    }

    // ==========================
    // EDIT (GET)
    // ==========================
    public async Task<IActionResult> Edit(Guid id)
    {
        var model = await _service.GetEditViewModelAsync(id);

        if (model == null)
            return NotFound();

        return View(model);
    }

    // ==========================
    // EDIT (POST)
    // ==========================
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(ProductionOrderViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var vm = await _service.GetCreateViewModelAsync();

            model.Machines = vm.Machines;
            model.Operators = vm.Operators;
            model.Products = vm.Products;

            foreach (var item in model.Details)
            {
                item.Products = vm.Products;
            }

            return View(model);
        }

        await _service.UpdateAsync(model);

        TempData["Success"] = "Production Order berhasil diupdate.";

        return RedirectToAction(nameof(Index));
    }

    // ==========================
    // EDIT RESULT
    // ==========================
    public async Task<IActionResult> EditResult(Guid id)
    {
        var data = await _service.GetByIdAsync(id);

        if (data == null)
            return NotFound();

        return View(data);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditResult(ProductionOrderViewModel model)
    {
        await _service.UpdateProductionResultAsync(model);

        TempData["Success"] = "Production Result berhasil diperbarui.";

        return RedirectToAction(nameof(Index));
    }
    
}