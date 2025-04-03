var builder = WebApplication.CreateBuilder(args);

DependencyContextAssemblyCatalog dependencyContextAssemblyCatalog = new DependencyContextAssemblyCatalog([typeof(Program).Assembly]);

builder.Services.AddCarter(dependencyContextAssemblyCatalog);


builder.Services.AddMediatR(config =>
{   //komut ve sorgular�n nerede olacag�n� s�yler
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();//crud i�lemleri icin bunu kulland�k





var app = builder.Build();

app.MapCarter();

app.Run();
