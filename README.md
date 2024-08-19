## 1. Creating Controller
  - Controller is a class that is used to group-up a set of actions (or action methods)
### Setup
```C#
//program.cs
var builder = WebApplication.CreateBuilder(args);

//Add Controllers as service in the IServiceCollection
builder.Services.AddControllers();

var app = builder.Build();

//Add all action methods as endpoints.
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
## 2. Multiple action methods

### Usage
```C#
//HomeController.cs
public class HomeController
{
    [Route("/")]
    public string Index(){
        return "Hello from Home screen";
    }

    [Route("about")]
    public string About(){
        return "Hello from About screen";
    }

    [Route("contact-us/{mobile:regex(^\\d{{10}}$)}")]
    public string Contact(int mobile){
        return $"Hello from Contact us Screen with number {mobile}";
    }
}
```