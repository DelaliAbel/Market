using MarketWeb_Business.Repository;
using MarketWeb_Business.Repository.IRepository;
using MarketWeb_DataAccess.Data;
using MarketWeb_Server.Data;
using MarketWeb_Server.Service;
using MarketWeb_Server.Service.IService;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

//--------------------Partie de la coonnextion à la BD SqlServer ------------------------------

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer //Appele du type de la BD
        (   //Recuperation des infos du fichier appSetting.json
            builder.Configuration.GetConnectionString("DefaultConnection")));

//----------------------Injection des Repository et de leur Interface----------------
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IFileUpload, FileUpload>();
builder.Services.AddScoped<IProductPriceRepository, ProductPriceRepository>();


//---------------------AutoMapper-------------------------------------------------------
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());



var app = builder.Build();




// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
