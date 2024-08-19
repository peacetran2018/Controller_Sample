using Microsoft.AspNetCore.Mvc;

namespace Controllers_Web.Controllers;

public class HomeController
{
    [Route("url1")]
    public string Method1(){
        return "Hello from method1";
    }
}
