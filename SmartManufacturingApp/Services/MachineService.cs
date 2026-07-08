using Microsoft.EntityFrameworkCore;
using SmartManufacturingApp.Data;
using SmartManufacturingApp.Models;
using SmartManufacturingApp.Services.Interfaces;

namespace SmartManufacturingApp.Services;

public class MachineService : IMachineService
{
    private readonly ApplicationDbContext _context;

    public MachineService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Machine>> GetAllAsync(string? keyword)
    {
        var query = _context.Machines.AsQueryable();

        if (!string.IsNullOrWhiteSpace(keyword))
        {
            query = query.Where(x =>
                x.MachineCode.Contains(keyword) ||
                x.MachineName.Contains(keyword) ||
                x.Location.Contains(keyword));
        }

        return await query
            .OrderBy(x => x.MachineName)
            .ToListAsync();
    }

    public async Task<Machine?> GetByIdAsync(Guid id)
    {
        return await _context.Machines.FindAsync(id);
    }

    public async Task CreateAsync(Machine machine)
    {
        machine.CreatedAt = DateTime.Now;
        machine.UpdatedAt = DateTime.Now;

        _context.Machines.Add(machine);

        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Machine machine)
    {
        machine.UpdatedAt = DateTime.Now;

        _context.Machines.Update(machine);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var machine = await _context.Machines.FindAsync(id);

        if (machine == null)
            return;

        _context.Machines.Remove(machine);

        await _context.SaveChangesAsync();
    }
}