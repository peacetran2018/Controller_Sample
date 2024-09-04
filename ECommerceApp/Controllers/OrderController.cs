using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ECommerceApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ECommerceApp.Controllers
{
    public class OrderController : Controller
    {
        [Route("order")]
        public IActionResult Index([Bind(nameof(Order.InvoicePrice), nameof(Order.OrderDate), nameof(Order.Products))]Order order)
        {
            if(!ModelState.IsValid){
                var errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);
                var stringError = string.Join("\n", errors);
                return BadRequest(stringError);
            }
            order.OrderNo = Random.Shared.Next(1,9999);
            return Content(" {\"Order No\" : " + order.OrderNo +"}", "application/json");
        }
    }
}