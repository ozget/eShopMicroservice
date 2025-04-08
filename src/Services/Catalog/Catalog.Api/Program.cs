
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


builder.Services.AddExceptionHandler<CustomExceptionHandler>();//dependency injection



var app = builder.Build();

app.MapCarter();

app.UseExceptionHandler(op =>{});

app.Run();
