using FluentValidation.AspNetCore;
using SimpleERP.API.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentityConfiguration(builder.Configuration);

builder.Services.AddApiConfiguration(builder.Configuration);

builder.Services.AddSwaggerConfiguration();

builder.Services.AddAutoMapperConfiguration();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddFluentValidationAutoValidation();

builder.Services.ResolveDependencies();

var app = builder.Build();

app.UseSwaggerConfiguration();

app.UseApiConfiguration(app.Environment);

app.Run();