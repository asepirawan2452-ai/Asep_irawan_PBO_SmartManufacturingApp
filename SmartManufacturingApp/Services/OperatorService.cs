using Microsoft.EntityFrameworkCore;
using SmartManufacturingApp.Data;
using SmartManufacturingApp.Models;
using SmartManufacturingApp.Services.Interfaces;

namespace SmartManufacturingApp.Services;

public class OperatorService : IOperatorService
{
    private readonly ApplicationDbContext _context;

    public OperatorService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<OperatorEmployee>> GetAllAsync(string? keyword)
    {
        var query = _context.Operators.AsQueryable();

        if (!string.IsNullOrWhiteSpace(keyword))
        {
            query = query.Where(x =>
                x.EmployeeCode.Contains(keyword) ||
                x.FullName.Contains(keyword) ||
                x.Department.Contains(keyword));
        }

        return await query
            .OrderBy(x => x.FullName)
            .ToListAsync();
    }

    public async Task<OperatorEmployee?> GetByIdAsync(Guid id)
    {
        return await _context.Operators.FindAsync(id);
    }

    public async Task CreateAsync(OperatorEmployee employee)
    {
        employee.CreatedAt = DateTime.Now;
        employee.UpdatedAt = DateTime.Now;

        _context.Operators.Add(employee);

        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(OperatorEmployee employee)
    {
        Console.WriteLine("ID DARI FORM : " + employee.Id);

        var all = await _context.Operators.ToListAsync();

        foreach (var op in all)
        {
            Console.WriteLine($"DATABASE : {op.Id} - {op.FullName}");
        }

        var existing = await _context.Operators
            .FirstOrDefaultAsync(x => x.Id == employee.Id);

        if (existing == null)
            throw new Exception($"Operator dengan ID {employee.Id} tidak ditemukan.");

        existing.EmployeeCode = employee.EmployeeCode;
        existing.FullName = employee.FullName;
        existing.Department = employee.Department;
        existing.IsActive = employee.IsActive;
        existing.UpdatedAt = DateTime.Now;

        await _context.SaveChangesAsync();
    }
    public async Task DeleteAsync(Guid id)
    {
        var employee = await _context.Operators.FindAsync(id);

        if (employee == null)
            return;

        _context.Operators.Remove(employee);

        await _context.SaveChangesAsync();
    }

}