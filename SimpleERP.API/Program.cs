using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SimpleERP.API.Controllers;
using SimpleERP.API.Data;
using SimpleERP.API.Extensions;
using SimpleERP.API.Models;
using SimpleERP.API.Profiles;
using SimpleERP.API.Validators;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DataApiCs");
builder.Services.AddDbContext<ErpDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDbContext<IdentityDataContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<IdentityDataContext>()
    .AddErrorDescriber<IdentityMessagesToPortuguese>()
    .AddDefaultTokenProviders();

// JWT 

var appSettingsSection = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(appSettingsSection);

var appSettings = appSettingsSection.Get<AppSettings>();
var key = Encoding.ASCII.GetBytes(appSettings.Secret);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => {
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters 
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = appSettings.ValidIn,
        ValidIssuer = appSettings.Issuer
    };
});

/* Banco de Dados em Memória
 * builder.Services.AddDbContext<ClientDbContext>(options => options.UseInMemoryDatabase("SimpleErpDB")); 
 */

//builder.Services.AddSingleton<ProductDbContext>();

builder.Services.AddControllers().AddFluentValidation();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Mapper's
builder.Services.AddAutoMapper(typeof(ClientProfile));
builder.Services.AddAutoMapper(typeof(ProductProfile));

// Validator's
builder.Services.AddTransient<IValidator<CreateClientModel>, CreateClientValidator>();
builder.Services.AddTransient<IValidator<UpdateClientModel>, UpdateClientValidator>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
