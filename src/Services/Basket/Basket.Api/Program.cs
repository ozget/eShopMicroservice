var builder = WebApplication.CreateBuilder(args);
//dependency injection kýsmý

builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(typeof(Program).Assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});


//http pipeline
var app = builder.Build();
app.MapCarter();


app.Run();
