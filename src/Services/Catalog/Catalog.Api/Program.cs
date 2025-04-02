var builder = WebApplication.CreateBuilder(args);

DependencyContextAssemblyCatalog dependencyContextAssemblyCatalog = new DependencyContextAssemblyCatalog([typeof(Program).Assembly]);

builder.Services.AddCarter(dependencyContextAssemblyCatalog);


builder.Services.AddMediatR(config =>
{   //komut ve sorgularýn nerede olacagýný söyler
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});






var app = builder.Build();

app.MapCarter();

app.Run();
