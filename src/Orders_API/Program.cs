using System.Reflection;
using BLL.Services;
using BLL.Services.Interfaces;
using BLL.Validators;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Orders_API.Helpers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddControllers();
builder.Services.AddDbContext<ShopDbContext>(options => options.UseInMemoryDatabase("ShopDb"));

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddScoped<IProductValidator, ProductValidator>();
builder.Services.AddScoped<IOrderValidator, OrderValidator>();

builder.Services.AddAutoMapper(typeof(AutomapperProfile));

builder.Services.AddSwaggerGen(c =>
{
     c.SwaggerDoc("v1", new OpenApiInfo { Title = "Pryaniky Shop API" });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
});
builder.Services.AddEndpointsApiExplorer();

// Add AWS Lambda support. When application is run in Lambda Kestrel is swapped out as the web server with Amazon.Lambda.AspNetCoreServer. This
// package will act as the webserver translating request and responses between the Lambda event source and ASP.NET Core.
builder.Services.AddAWSLambdaHosting(LambdaEventSource.RestApi);

var app = builder.Build();

app.UseExceptionHandler();

app.UseAuthorization();
app.MapControllers();

app.UseSwagger(c =>
{
    c.RouteTemplate = "swagger/{documentName}/swagger.json";
});

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("v1/swagger.json", "Pryaniky Shop API");
    c.RoutePrefix = "swagger";
});

app.UseAuthorization();

// Seed the database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ShopDbContext>();
    DbInitializer.Initialize(context);
}

app.Run();