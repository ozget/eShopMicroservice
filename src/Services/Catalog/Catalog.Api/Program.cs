

using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddMediatR(config =>
{   //komut ve sorgular�n nerede olacag�n� s�yler
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
}).UseLightweightSessions();//crud i�lemleri icin bunu kulland�k

if (builder.Environment.IsDevelopment())// sadece geli�tirme ortam�nda ve ilk program cal��t�r�ld�g�nda olu�acak
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
    });//json format�nda HealthCheck g�rmek icin 

app.Run();
