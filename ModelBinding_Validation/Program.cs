using ModelBinding_Validation.CustomModelBinder;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers(options => {
    //options.ModelBinderProviders.Insert(0, new PersonBinderProvider());
    //index 0 is first place
});
//to controller understand XML format
builder.Services.AddControllers().AddXmlSerializerFormatters();
var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.MapControllers();

app.Run();
