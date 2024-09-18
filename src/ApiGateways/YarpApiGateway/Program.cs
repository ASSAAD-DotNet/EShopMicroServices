var builder = WebApplication.CreateBuilder(args);

// Add servcies to the container.

var app = builder.Build();

// Configuration the http request pipline

app.Run();
