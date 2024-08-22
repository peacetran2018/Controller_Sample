using Controllers_Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Controllers_Web.Controllers;

public class HomeController : Controller
{
    [Route("/")]
    public ContentResult Index()
    {
        //return "Hello from Home screen";
        // return new ContentResult() {
        //     Content = "<h1>Hello from home screen</h1>",
        //     ContentType = "text/html",
        //     StatusCode = 400
        // };
        return Content("<h1>Hello from home screen</h1>", "text/html");
    }

    [Route("about")]
    public string About()
    {
        return "Hello from About screen";
    }

    [Route("contact-us/{mobile:regex(^\\d{{10}}$)}")]
    public string Contact(int mobile)
    {
        return $"Hello from Contact us Screen with number {mobile}";
    }

    [Route("person")]
    public JsonResult Person()
    {
        Person person = new Person(){
            Id = Guid.NewGuid(),
            FirstName = "Peace",
            LastName = "Tran",
            Age = 34
        };
        //return new JsonResult(person);
        return Json(person);
        //return "{\"Key\":\"Value\"}";
    }

    [Route("file-download")]
    public VirtualFileResult FileDownload(){
        //return new VirtualFileResult("/sample.pdf", "application/pdf");
        return File("/sample.pdf", "application/pdf");
    }

    [Route("file-download2")]
    public PhysicalFileResult FileDownload2(){
        //return new PhysicalFileResult("/Volumes/Peace_SSD/learning/dotnet/Controller_Sample/Controllers_Web/wwwroot/sample.pdf", "application/pdf");
        return PhysicalFile("/Volumes/Peace_SSD/learning/dotnet/Controller_Sample/Controllers_Web/wwwroot/sample.pdf", "application/pdf");
    }

    [Route("file-download3")]
    public FileContentResult FileDownload3(){
        byte[] bytes = System.IO.File.ReadAllBytes("/Volumes/Peace_SSD/learning/dotnet/Controller_Sample/Controllers_Web/wwwroot/sample.pdf");
        //return new FileContentResult(bytes, "application/pdf");
        return File(bytes, "application/pdf");
    }
}
