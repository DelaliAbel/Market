using MarketWeb_Business.Repository;
using MarketWeb_Business.Repository.IRepository;
using MarketWeb_DataAccess.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//--------------------Partie de la coonnextion à la BD SqlServer ------------------------------

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer //Appele du type de la BD
        (   //Recuperation des infos du fichier appSetting.json
            builder.Configuration.GetConnectionString("DefaultConnection")));
//---------------------AutoMapper-------------------------------------------------------
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//---------------------Injection des Repository et de leur Interface-------------------------------------------------------
builder.Services.AddScoped<IProductRepository, ProductRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//---------------------Routing-------------------------------------------------------
app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
