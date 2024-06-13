using MarketWeb_API.Helper;
using MarketWeb_Business.Repository;
using MarketWeb_Business.Repository.IRepository;
using MarketWeb_DataAccess;
using MarketWeb_DataAccess.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//--------------Ajout de Bearer à Swagger-----------------
//builder.Services.AddSwaggerGen(); //Code par defaut


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "TangWeb_Api", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please Bearer and then token in the field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                   {
                     new OpenApiSecurityScheme
                     {
                       Reference = new OpenApiReference
                       {
                         Type = ReferenceType.SecurityScheme,
                         Id = "Bearer"
                       }
                      },
                      new string[] { }
                    }
                });
});

//--------------------Partie de la coonnextion à la BD SqlServer ------------------------------

builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
    options.UseSqlServer //Appele du type de la BD
        (   //Recuperation des infos du fichier appSetting.json
            builder.Configuration.GetConnectionString("DefaultConnection")));

//---------------------Defintition d'Identity au DbContext-------------------------------------------------------
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddDefaultTokenProviders()
    .AddEntityFrameworkStores<ApplicationDbContext>();

//---------------------AutoMapper-------------------------------------------------------
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//---------------------Injection des Repository et de leur Interface-------------------------------
builder.Services.AddScoped<IProductRepository, ProductRepository>();

//-----------------------Autorisation d'accès à l'API depuis MarketWweb---------------
builder.Services.AddCors(o=>o.AddPolicy("MarketWeb", builder =>
{
    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyMethod();
}));

//-------------------------- Setting of API -------------------------------

var APISettingSection = builder.Configuration.GetSection("APISettings");
builder.Services.Configure<APISettings>(APISettingSection);

//--------------------------- Add authentication to API ----------------------
var apiSettings = APISettingSection.Get<APISettings>();
var key = Encoding.ASCII.GetBytes(apiSettings.SecretKey);


builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = true;
    x.SaveToken = true;
    x.TokenValidationParameters = new()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidAudience = apiSettings.ValidAudience,
        ValidIssuer = apiSettings.ValidIssuer,
        ClockSkew = TimeSpan.Zero
    };
});
//------------------------------


var app = builder.Build();

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // allow any origin  
    .AllowCredentials());               // allow credentials 

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//---------------------Routing-------------------------------------------------------
app.UseRouting();
//-------------------Use l'autorisation du Cors MarketWeb ----------------
app.UseCors("MarketWeb");

//--------------ajout de l'authentification, toujours derrière l'autorisation
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
