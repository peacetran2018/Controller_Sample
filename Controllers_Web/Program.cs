var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();//Add Controllers as service in the IServiceCollection.

var app = builder.Build();
app.UseStaticFiles();//enable static files - default folder name wwwroot
app.UseRouting();
app.MapControllers();//Add alls action method as endpoints.

app.Run();
