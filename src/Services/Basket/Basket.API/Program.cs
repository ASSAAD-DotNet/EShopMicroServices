var builder = WebApplication.CreateBuilder(args);

var assembly = typeof(Program).Assembly;
builder.Services.AddCarter();
builder.Services.AddMediatR(conf =>
{
    conf.RegisterServicesFromAssembly(assembly);
    conf.AddOpenBehavior(typeof(ValidationBehavior<,>));
    conf.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddMarten(options => {
    options.Connection(builder.Configuration.GetConnectionString("DataBase")!);
    options.Schema.For<ShoppingCart>().Identity(c => c.UseerName);
}).UseLightweightSessions();


var app = builder.Build();


app.MapCarter();

app.Run();
