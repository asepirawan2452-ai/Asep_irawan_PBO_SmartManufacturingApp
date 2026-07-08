using Microsoft.EntityFrameworkCore;
using SmartManufacturingApp.Data;
using SmartManufacturingApp.Services;
using SmartManufacturingApp.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(
            builder.Configuration.GetConnectionString("DefaultConnection"))
    ));

// ===========================
// Services
// ===========================
builder.Services.AddScoped<IDashboardService, DashboardService>();
builder.Services.AddScoped<IMachineService, MachineService>();
builder.Services.AddScoped<IOperatorService, OperatorService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductionOrderService, ProductionOrderService>();

builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<IReportPdfService, ReportPdfService>();
builder.Services.AddScoped<IReportExcelService, ReportExcelService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}");

app.Run();