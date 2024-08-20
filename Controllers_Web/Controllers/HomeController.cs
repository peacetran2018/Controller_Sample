using Microsoft.AspNetCore.Mvc;

namespace Controllers_Web.Controllers;

public class HomeController : Controller
{
    [Route("/")]
    public ContentResult Index(){
        //return "Hello from Home screen";
        // return new ContentResult() {
        //     Content = "<h1>Hello from home screen</h1>",
        //     ContentType = "text/html",
        //     StatusCode = 400
        // };
        return Content("<h1>Hello from home screen</h1>", "text/html");
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
