using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using MobileStore.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Retrieve connection string from configuration
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Check if connectionString is null or empty
if (string.IsNullOrEmpty(connectionString))
{
    // Handle the case where connectionString is not configured properly
    Console.WriteLine("Connection string is not configured.");
    Environment.ExitCode = 1;
}
else
{
    // Configure MySQL DbContext
    builder.Services.AddDbContext<MobileContext>(options =>
    options.UseMySQL(connectionString)
           .LogTo(Console.WriteLine, LogLevel.Information));

}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app != null)
{
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
        pattern: "{controller=Mobiles}/{action=Index}/{id?}");

    app.Run();
}
else
{
    // Handle the case where app is null, maybe throw an exception or log an error
    Console.WriteLine("Application failed to build. Exiting...");
    Environment.ExitCode = 1;
}
