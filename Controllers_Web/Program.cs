var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();//Add Controllers to service

var app = builder.Build();

app.MapControllers();//Enable map controller routing

app.Run();
