using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using SimpleERP.API.Controllers;
using SimpleERP.API.Data;
using SimpleERP.API.Models;
using SimpleERP.API.Profiles;
using SimpleERP.API.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DataApiCs");
builder.Services.AddDbContext<ErpDbContext>(options => options.UseSqlServer(connectionString));

/* Banco de Dados em Memória
 * builder.Services.AddDbContext<ClientDbContext>(options => options.UseInMemoryDatabase("SimpleErpDB")); 
 */

//builder.Services.AddSingleton<ProductDbContext>();

builder.Services.AddControllers().AddFluentValidation();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(ClientProfile));
builder.Services.AddTransient<IValidator<CreateClientModel>, CreateClientValidator>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
