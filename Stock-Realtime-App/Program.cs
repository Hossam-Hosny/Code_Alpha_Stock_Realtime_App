using Microsoft.EntityFrameworkCore;
using Stock_Realtime_App.Hubs;
using Stock_Realtime_App.Models;
using Stock_Realtime_App.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>{
    options.UseSqlServer(builder.Configuration.GetConnectionString("LocalConnectionString")); 
});
builder.Services.AddSignalR();
builder.Services.AddTransient<IStock, StockService>();
builder.Services.AddHttpClient<IStock, StockService>();
builder.Services.AddHostedService<StockDataFetcherService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Stock}/{action=Index}/{id?}");

app.MapHub<StockHub>("/stockHub");

app.Run();
