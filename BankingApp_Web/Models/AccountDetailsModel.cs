using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.FileSystemGlobbing.Internal.PathSegments;

namespace BankingApp_Web.Models
{
    public class AccountDetailsModel
    {
        public int AccountNumber{get;set;}
        public string? AccountHolderName{get;set;}
        public int CurrentBalance{get;set;}
    }
}