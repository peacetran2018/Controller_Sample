## 1. Creating Controller
  - Controller is a class that is used to group-up a set of actions (or action methods)
### Setup
```C#
//program.cs
var builder = WebApplication.CreateBuilder(args);

//Add Controllers to service
builder.Services.AddControllers();

var app = builder.Build();

//Enable controller routing
app.MapControllers();

app.Run();
```

### Usage
```C#
//HomeController.cs

namepace Controllers;

public class HomeController{
  [Route("url1")]
  public string Method1(){
    return "Hello from method1";
  }
}
```
