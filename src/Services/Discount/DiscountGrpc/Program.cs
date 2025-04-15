using DiscountGrpc.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddGrpcReflection();




var app = builder.Build();


app.MapGrpcService<GreeterService>();

if (app.Environment.IsDevelopment())

    app.MapGrpcReflectionService();



app.Run();
