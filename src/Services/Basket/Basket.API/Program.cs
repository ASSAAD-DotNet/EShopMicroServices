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
    options.Schema.For<ShoppingCart>().Identity(c => c.UserName);
}).UseLightweightSessions();


builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.Decorate<IBasketRepository, CachedBasketRepository>();

builder.Services.AddStackExchangeRedisCache(Option =>
{
    Option.Configuration = builder.Configuration.GetConnectionString("Redis");
});

builder.Services.AddExceptionHandler<CustomExceptionHandler>();


builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("DataBase")!)
    .AddRedis(builder.Configuration.GetConnectionString("Redis")!);


var app = builder.Build();


app.MapCarter();
app.UseExceptionHandler(option => { });
app.MapHealthChecks("/Health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();
