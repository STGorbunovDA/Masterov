using System.Reflection;
using AutoMapper;
using FluentValidation;
using Masterov.API.Extensions;
using Masterov.API.Middlewares;
using Masterov.Domain.DI;
using Masterov.Domain.Models;
using Masterov.Storage.DI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddStorage(builder.Configuration.GetConnectionString("AppDbConnectionString"))
    .AddDomain();

builder.Services.AddValidatorsFromAssemblyContaining<ProductDomain>();
builder.Services.AddCustomSwagger();
builder.Services.AddAutoMapper(config => config.AddMaps(Assembly.GetExecutingAssembly()));
builder.Logging.AddConsole();

var app = builder.Build();
var mapper = app.Services.GetRequiredService<IMapper>();
mapper.ConfigurationProvider.AssertConfigurationIsValid();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();
app.MapControllers();
app.UseMiddleware<ErrorHandlingMiddleware>();

app.Run();