using Microsoft.AspNetCore.Mvc;
using SmartManufacturingApp.Models;
using SmartManufacturingApp.Services.Interfaces;

namespace SmartManufacturingApp.Controllers;

public class ProductController : Controller
{
    private readonly IProductService _service;

    public ProductController(IProductService service)
    {
        _service = service;
    }

    // =========================
    // INDEX
    // =========================
    public async Task<IActionResult> Index(string? keyword)
    {
        ViewBag.Keyword = keyword;
        var data = await _service.GetAllAsync(keyword);

        return View(data);
    }

    // =========================
    // CREATE
    // =========================
    public IActionResult Create()
    {
        return View(new Product
        {
            IsActive = true // default aktif
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Product product)
    {
        if (!ModelState.IsValid)
            return View(product);

        await _service.CreateAsync(product);

        TempData["Success"] = "Product berhasil ditambahkan.";

        return RedirectToAction(nameof(Index));
    }

    // =========================
    // EDIT
    // =========================
    public async Task<IActionResult> Edit(Guid id)
    {
        var product = await _service.GetByIdAsync(id);

        if (product == null)
            return NotFound();

        return View(product);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, Product product)
    {
        if (id != product.Id)
            return BadRequest();

        if (!ModelState.IsValid)
            return View(product);

        var existing = await _service.GetByIdAsync(id);

        if (existing == null)
            return NotFound();

        await _service.UpdateAsync(product);

        TempData["Success"] = "Product berhasil diperbarui.";

        return RedirectToAction(nameof(Index));
    }

    // =========================
    // DETAILS
    // =========================
    public async Task<IActionResult> Details(Guid id)
    {
        var product = await _service.GetByIdAsync(id);

        if (product == null)
            return NotFound();

        return View(product);
    }

    // =========================
    // DELETE
    // =========================
    public async Task<IActionResult> Delete(Guid id)
    {
        var product = await _service.GetByIdAsync(id);

        if (product == null)
            return NotFound();

        return View(product);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var existing = await _service.GetByIdAsync(id);

        if (existing == null)
            return NotFound();

        await _service.DeleteAsync(id);

        TempData["Success"] = "Product berhasil dihapus.";

        return RedirectToAction(nameof(Index));
    }
}