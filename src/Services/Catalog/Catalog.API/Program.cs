var builder = WebApplication.CreateBuilder(args);

var assembly = typeof(Program).Assembly;

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(assembly);
builder.Services.AddCarter();

builder.Services.AddMarten(options => { 
    options.Connection(builder.Configuration.GetConnectionString("DataBase")!); 
}).UseLightweightSessions();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();


if (builder.Environment.IsDevelopment())
{
    builder.Services.InitializeMartenWith<CatalogInitailData>();
} 


var app = builder.Build();

app.MapCarter();
app.UseExceptionHandler(option=> { });

app.Run();
