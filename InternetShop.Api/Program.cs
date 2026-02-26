using CorrelationId;
using CorrelationId.DependencyInjection;
using InternetShop.Api.Middleware;
using InternetShop.Api.Modules;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
builder.Services.AddCore(builder.Configuration);
builder.Services.UseCoreLogging();
builder.Services.AddDefaultCorrelationId();

builder.Services.AddHttpLogging(options => { });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCorrelationId();

app.UseMiddleware<HealthCheckMiddleware>();

app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseHttpLogging();

app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();
