using Microsoft.AspNetCore.Mvc;
using SmartManufacturingApp.Models;
using SmartManufacturingApp.Services.Interfaces;

namespace SmartManufacturingApp.Controllers;

public class MachineController : Controller
{
    private readonly IMachineService _machineService;

    public MachineController(IMachineService machineService)
    {
        _machineService = machineService;
    }

    public async Task<IActionResult> Index(string? keyword)
    {
        var data = await _machineService.GetAllAsync(keyword);

        ViewBag.Keyword = keyword;

        return View(data);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Machine machine)
    {
        if (!ModelState.IsValid)
            return View(machine);

        await _machineService.CreateAsync(machine);

        TempData["Success"] = "Machine berhasil ditambahkan.";

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(Guid id)
    {
        var machine = await _machineService.GetByIdAsync(id);

        if (machine == null)
            return NotFound();

        return View(machine);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Machine machine)
    {
        if (!ModelState.IsValid)
            return View(machine);

        await _machineService.UpdateAsync(machine);

        TempData["Success"] = "Machine berhasil diperbarui.";

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Details(Guid id)
    {
        var machine = await _machineService.GetByIdAsync(id);

        if (machine == null)
            return NotFound();

        return View(machine);
    }

    public async Task<IActionResult> Delete(Guid id)
    {
        var machine = await _machineService.GetByIdAsync(id);

        if (machine == null)
            return NotFound();

        return View(machine);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        await _machineService.DeleteAsync(id);

        TempData["Success"] = "Machine berhasil dihapus.";

        return RedirectToAction(nameof(Index));
    }
}