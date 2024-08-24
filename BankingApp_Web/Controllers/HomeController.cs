using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BankingApp_Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BankingApp_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly AccountDetailsModel accountDetailsModel;
        private List<AccountDetailsModel> accountDetailsModels;
        public HomeController(){
            accountDetailsModel = new AccountDetailsModel(){
                AccountNumber = 1001,
                AccountHolderName = "Example Name",
                CurrentBalance = 5000
            };

            accountDetailsModels = new List<AccountDetailsModel>();
            accountDetailsModels.Add(accountDetailsModel);
        }
        [Route("/")]
        public IActionResult Index()
        {
            return Content("<h1>Welcome to the Best Bank</h1>","text/html");
        }

        [Route("account-details")]
        public IActionResult AcountDetails(){
            return Json(accountDetailsModel);
        }

        [Route("account-statement")]
        public IActionResult AccountStatement(){
            return File("/sample.pdf", "application/pdf");
        }

        [Route("get-current-balance/{accountNumber?}")]
        public IActionResult GetCurrentBalance(int? accountNumber){
            if(accountNumber == null){
                return BadRequest("Account number should be supplied");
            }

            if(accountNumber != 1001){
                return BadRequest("Account number should be 1001");
            }

            var accountDetail = accountDetailsModels.FirstOrDefault(x => x.AccountNumber == accountNumber);
            
            return Content($"{accountDetail?.CurrentBalance}");
        }
    }
}