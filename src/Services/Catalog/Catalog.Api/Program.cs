var builder = WebApplication.CreateBuilder(args);

DependencyContextAssemblyCatalog dependencyContextAssemblyCatalog = new DependencyContextAssemblyCatalog([typeof(Program).Assembly]);

builder.Services.AddCarter(dependencyContextAssemblyCatalog);


builder.Services.AddMediatR(config =>
{   //komut ve sorgular�n nerede olacag�n� s�yler
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});






var app = builder.Build();

app.MapCarter();

app.Run();
