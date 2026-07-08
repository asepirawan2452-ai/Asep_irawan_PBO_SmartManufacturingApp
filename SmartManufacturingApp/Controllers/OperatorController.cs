using Microsoft.AspNetCore.Mvc;
using SmartManufacturingApp.Models;
using SmartManufacturingApp.Services.Interfaces;

namespace SmartManufacturingApp.Controllers;

public class OperatorController : Controller
{
    private readonly IOperatorService _service;

    public OperatorController(IOperatorService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index(string? keyword)
    {
        ViewBag.Keyword = keyword;
        return View(await _service.GetAllAsync(keyword));
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(OperatorEmployee employee)
    {
        if (!ModelState.IsValid)
            return View(employee);

        await _service.CreateAsync(employee);

        TempData["Success"] = "Operator berhasil ditambahkan.";

        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Edit(Guid id)
    {
        var data = await _service.GetByIdAsync(id);

        if (data == null)
            return NotFound();

        return View(data);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(OperatorEmployee employee)
    {
        if (!ModelState.IsValid)
            return View(employee);

        await _service.UpdateAsync(employee);

        TempData["Success"] = "Operator berhasil diperbarui.";

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Details(Guid id)
    {
        var data = await _service.GetByIdAsync(id);

        if (data == null)
            return NotFound();

        return View(data);
    }

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

        TempData["Success"] = "Operator berhasil dihapus.";

        return RedirectToAction(nameof(Index));
    }

}