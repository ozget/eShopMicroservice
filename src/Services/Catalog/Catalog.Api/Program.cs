var builder = WebApplication.CreateBuilder(args);

DependencyContextAssemblyCatalog dependencyContextAssemblyCatalog = new DependencyContextAssemblyCatalog([typeof(Program).Assembly]);

builder.Services.AddCarter(dependencyContextAssemblyCatalog);


builder.Services.AddMediatR(config =>
{   //komut ve sorgularýn nerede olacagýný söyler
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();//crud iþlemleri icin bunu kullandýk





var app = builder.Build();

app.MapCarter();

app.Run();
