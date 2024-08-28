using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ModelBinding_Validation.Controllers
{
    public class HomeController : Controller
    {
        [Route("bookstore/{bookid?}/{isloggedin?}")]//it is called route parameters 
        public IActionResult Index(int? bookid, bool? isloggedin)
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
            return RedirectToAction("Books", "Store", new { id = bookid });
        }
    }
}