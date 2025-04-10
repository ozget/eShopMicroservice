var builder = WebApplication.CreateBuilder(args);
//dependency injection k�sm�

builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(typeof(Program).Assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
    opts.Schema.For<ShoppingCart>().Identity(x => x.UserName);//bir Id bulunmad��� i�in unique bir de�er oldu�unu s�yl�yoruz
}).UseLightweightSessions();





//http pipeline
var app = builder.Build();
app.MapCarter();


app.Run();
