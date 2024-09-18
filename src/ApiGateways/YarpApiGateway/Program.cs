var builder = WebApplication.CreateBuilder(args);

// Add servcies to the container.
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var app = builder.Build();

// Configuration the http request pipline
app.MapReverseProxy();

app.Run();
