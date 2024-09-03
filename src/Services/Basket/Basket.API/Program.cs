using Discount.Grpc;

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

builder.Services
    .AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(options =>
    {
        options.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]!);
    })
    .ConfigurePrimaryHttpMessageHandler(() =>
    {
        var handler = new HttpClientHandler()
        {
            ServerCertificateCustomValidationCallback =
             HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        };
        return handler;
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
