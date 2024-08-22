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
## 3. ContentResult
  - ContentResult can represent any type of response, such as text/plain, text/html, application/pdf, application,json

### Syntax
```C#
return ContentResult
```
### Usage
```C#
public ContentResult Index(){
  return new ContentResult(){
    Content = "<h1>Hello World</h1>",
    ContentType = "text/html"
  };
}
```
### Output
# Hello World

  - Shortcut way, we can inheritant from Controller class then we can replace by Content("Content", "Content Type")
### Syntax
```C#
return Content("Content", "Content Type");
```

### Usage
```C#
public class HomeController : Controller{
  public ContentResult Indext(){
    return Content("<h1>Hello World</h1>", "text/html");
  }
}
```
### Output
# Hello World

## 4. FileResults
  - There are 3 types of FileResults: VirtualFileResult, PhysicalFileResult and FileContentResult.

### Syntax
```C#
  return new VirtualFileResult("File relative path", "Content type");
  return new PhysicalFileResult("File Absolute path", "Content type");
  return new FileContentResult(byte_array, "Content type");
```

### Usage
```C#
  //program.cs
  ...
  app.UseStaticFiles();//enable static files - default folder name wwwroot
  ...

  //homecontroller.cs
  [Route("file-download")]
    public VirtualFileResult FileDownload(){
        return new VirtualFileResult("/sample.pdf", "application/pdf");
    }

    [Route("file-download2")]
    public PhysicalFileResult FileDownload2(){
        return new PhysicalFileResult("/Volumes/Peace_SSD/learning/dotnet/Controller_Sample/Controllers_Web/wwwroot/sample.pdf", "application/pdf");
    }

    [Route("file-download3")]
    public FileContentResult FileDownload3(){
        byte[] bytes = System.IO.File.ReadAllBytes("/Volumes/Peace_SSD/learning/dotnet/Controller_Sample/Controllers_Web/wwwroot/sample.pdf");
        return new FileContentResult(bytes, "application/pdf");
    }
```
  - Shorter way

### Syntax
```C#
  //For virtual content result and file content result
  return File("File relative path in wwwroot folder", "Content Type");
  //For physical content result
  return PhysicalFile("File absolute path", "Content Type")
```

### Usage
```C#
  [Route("file-download")]
    public VirtualFileResult FileDownload(){
        return File("/sample.pdf", "application/pdf");
    }

    [Route("file-download2")]
    public PhysicalFileResult FileDownload2(){
        return PhysicalFile("/Volumes/Peace_SSD/learning/dotnet/Controller_Sample/Controllers_Web/wwwroot/sample.pdf", "application/pdf");
    }

    [Route("file-download3")]
    public FileContentResult FileDownload3(){
        byte[] bytes = System.IO.File.ReadAllBytes("/Volumes/Peace_SSD/learning/dotnet/Controller_Sample/Controllers_Web/wwwroot/sample.pdf");
        return File(bytes, "application/pdf");
    }
```