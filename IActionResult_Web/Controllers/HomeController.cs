using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IActionResult_Web.Controllers;
public class HomeController : Controller
{
    [Route("bookstore")]
    public IActionResult Index()
    {
        //IActionResult can be used for all return action methods in same method
        if (!Request.Query.ContainsKey("bookid"))
        {
            //Response.StatusCode = 400;
            //return Content("Book ID is not supplied");
            return BadRequest("Book ID is not supplied");
        }

        if (string.IsNullOrEmpty(Convert.ToString(Request.Query["bookid"])))
        {
            //Response.StatusCode = 400;
            //return Content("Book ID cannot be blank");
            return BadRequest("Book ID cannot be blank");
        }

        int bookid = Convert.ToInt32(Request.Query["bookid"]);
        if (bookid < 0)
        {
            //Response.StatusCode = 400;
            //return Content("Book ID cannot be less than 0");
            return BadRequest("Book ID cannot be less than 0");
        }
        if (bookid > 1000)
        {
            //Response.StatusCode = 400;
            //return Content("Book ID cannot be greater than 1000");
            return NotFound("Book ID cannot be greater than 1000");
        }
        var isLoggedIn = Convert.ToBoolean(Request.Query["isloggedin"]);
        if (!isLoggedIn)
        {
            return Unauthorized("User must be authenticated");
        }

        //return File("/sample.pdf", "application/pdf");
        //Redirect to result and short way
        //return new RedirectToActionResult("books","store", new {bookid = bookid});//status code 302 - Found
        //return new  RedirectToActionResult("books", "store", new { }, true);//status code 301 - Permanent moved
        //return RedirectToAction("books","store", new {id = bookid});
        //return RedirectToActionPermanent("books", "store", new {id = bookid});

        //local redirect result and short way
        //302
        //return new LocalRedirectResult($"store/books/{bookid}");//provide fullpath
        //return LocalRedirect($"store/books/{bookid}");
        //301
        //return new LocalRedirectResult($"store/books/{bookid}", true);//301
        return LocalRedirectPermanent($"store/books/{bookid}");
    }
}