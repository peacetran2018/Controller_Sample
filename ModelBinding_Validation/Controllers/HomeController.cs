using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ModelBinding_Validation.Models;
using ModelValidations.Models;

namespace ModelBinding_Validation.Controllers
{
    public class HomeController : Controller
    {
        [Route("bookstore/{bookid?}/{isloggedin?}")]//it is called route parameters 
        public IActionResult Index(int? bookid, [FromRoute]bool? isloggedin, Book book)
        {
            if (bookid.HasValue == false)
            {
                return BadRequest("Book ID is not supplied or empty");
            }

            if (bookid < 0)
            {
                return BadRequest("Book ID cannot be less than 0");
            }
            if (bookid > 1000)
            {
                return NotFound("Book ID cannot be greater than 1000");
            }
            
            if (isloggedin == false)
            {
                return StatusCode(401);
            }

            //return RedirectToAction("Books", "Store", new { id = bookid });
            return Content($"Book information {book.ToString()}");
        }

        [Route("register")]
        public IActionResult Register(Person person)
        {
            if(!ModelState.IsValid){
                // List<string> errors = new List<string>();
                // foreach(var value in ModelState.Values){
                //     foreach(var error in value.Errors){
                //         errors.Add(error.ErrorMessage);
                //     }
                // }
                // var errorString = string.Join("\n", errors);
                // return BadRequest(errorString);
                
                List<string> errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
                var errorString = string.Join("\n", errors);
                return BadRequest(errorString);
            }
            return Content($"{person.ToString()}");
        }
    }
}