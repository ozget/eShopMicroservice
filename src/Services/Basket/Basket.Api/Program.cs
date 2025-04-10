var builder = WebApplication.CreateBuilder(args);
//dependency injection kýsmý

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
    opts.Schema.For<ShoppingCart>().Identity(x => x.UserName);//bir Id bulunmadýðý için unique bir deðer olduðunu söylüyoruz
}).UseLightweightSessions();





//http pipeline
var app = builder.Build();
app.MapCarter();


app.Run();
