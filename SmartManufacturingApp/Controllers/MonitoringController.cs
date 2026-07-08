using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartManufacturingApp.Data;

namespace SmartManufacturingApp.Controllers;

public class MonitoringController : Controller
{
    private readonly ApplicationDbContext _context;

    public MonitoringController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var machines = await _context.Machines
            .OrderBy(x => x.MachineName)
            .ToListAsync();

        return View(machines);
    }
}