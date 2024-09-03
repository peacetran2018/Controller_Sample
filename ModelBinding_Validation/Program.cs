var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
//to controller understand XML format
builder.Services.AddControllers().AddXmlSerializerFormatters();
var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.MapControllers();

app.Run();
