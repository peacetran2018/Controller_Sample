var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();//Add Controllers as service in the IServiceCollection.

var app = builder.Build();

app.MapControllers();//Add alls action method as endpoints.

app.Run();
