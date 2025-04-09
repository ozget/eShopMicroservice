

using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddMediatR(config =>
{   //komut ve sorgularýn nerede olacagýný söyler
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));//pipeline
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));//pipeline
});

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);


DependencyContextAssemblyCatalog dependencyContextAssemblyCatalog = new DependencyContextAssemblyCatalog([typeof(Program).Assembly]);
builder.Services.AddCarter(dependencyContextAssemblyCatalog);


builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();//crud iþlemleri icin bunu kullandýk

if (builder.Environment.IsDevelopment())// sadece geliþtirme ortamýnda ve ilk program calýþtýrýldýgýnda oluþacak
    builder.Services.InitializeMartenWith<CatalogInitialData>();


builder.Services.AddExceptionHandler<CustomExceptionHandler>();//dependency injection

builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("Database"));

var app = builder.Build();

app.MapCarter();

app.UseExceptionHandler(op =>{});

app.UseHealthChecks("/health",
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });//json formatýnda HealthCheck görmek icin 

app.Run();
