using Microsoft.AspNetCore.Mvc;

namespace Controllers_Web.Controllers;

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
