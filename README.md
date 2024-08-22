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
## 4. JsonResult
  - JsonResult can represent an object in Javascript Object Notation (JSON) Format.

### Syntax
```C#
  return JsonResult
```

### Usage
```C#
  public JsonResult Person()
  {
      Person person = new Person(){
          Id = Guid.NewGuid(),
          FirstName = "Peace",
          LastName = "Tran",
          Age = 34
      };
      return new JsonResult(person);
  }
```
  - Shorter way

### Syntax
```C#
  return Json
```

### Usage
```C#
  public JsonResult Person()
  {
      Person person = new Person(){
          Id = Guid.NewGuid(),
          FirstName = "Peace",
          LastName = "Tran",
          Age = 34
      };
      return Json(person);
  }
```
### Output
```JSON
  {
    Id = xxxxxxxxxxxx,
    FirstName = "Peace",
    LastName = "Tran",
    Age = 34
  }
```
## 5. FileResults
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
## 6. IActionResult
  - It is the parent interface for all action result classes such as ContentResult,JsonResult, RediectResult, StatusCodeResult, ViewResult etc.
  - By mentioning the return type as IActionResult, you can return either of the subtypes of IActionResult.
  
### Syntax
```C#
  public IActionResult Index(){
    if(condition){
      return Content("");
    }
    if(condition){
      return Json(OBJECT);
    }
    return File("File Path", "Content Type");
  }
```
### Usage
```C#
    [Route("/")]
    public IActionResult Index(){
        //IActionResult can be used for all return action methods in same method
        if(!Request.Query.ContainsKey("bookid")){
            Response.StatusCode = 400;
            return Content("Book ID is not supplied");
        }

        if(string.IsNullOrEmpty(Convert.ToString(Request.Query["bookid"]))){
            Response.StatusCode = 400;
            return Content("Book ID cannot be blank");
        }

        int bookid = Convert.ToInt32(Request.Query["bookid"]);
        if(bookid < 0){
            Response.StatusCode = 400;
            return Content("Book ID cannot be less than 0");
        }
        if(bookid > 1000){
            Response.StatusCode = 400;
            return Content("Book ID cannot be greater than 1000");
        }
        Response.StatusCode = 200;
        return File("/sample.pdf", "application/pdf");
    }
```