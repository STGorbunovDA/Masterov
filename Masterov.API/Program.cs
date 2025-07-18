using System.Reflection;
using System.Text.Json.Serialization;
using AutoMapper;
using FluentValidation;
using Masterov.API.Extensions;
using Masterov.API.Middlewares;
using Masterov.Domain.DI;
using Masterov.Domain.Models;
using Masterov.Storage.DI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(policy => policy.AddPolicy("default", opt =>
{
    opt.AllowAnyHeader();
    opt.AllowCredentials();
    opt.AllowAnyMethod();
    opt.SetIsOriginAllowed(_ => true);
}));

builder.Services.AddLogging();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCustomSwagger();
builder.Services.AddValidatorsFromAssemblyContaining<FinishedProductDomain>();
builder.Services.AddJwtAuthentication(builder.Configuration);

builder.Services
    .AddStorage(builder.Configuration.GetConnectionString("AppDbConnectionString"))
    .AddDomain();

builder.Services.AddAutoMapper(config => config.AddMaps(Assembly.GetExecutingAssembly()));

builder.Logging.AddConsole();

var app = builder.Build();
var mapper = app.Services.GetRequiredService<IMapper>();
mapper.ConfigurationProvider.AssertConfigurationIsValid();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("default");

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseMiddleware<ErrorHandlingMiddleware>();

app.Run();