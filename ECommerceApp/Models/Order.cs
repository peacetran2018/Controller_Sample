using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ECommerceApp.CustomValidators;

namespace ECommerceApp.Models
{
    public class Order
    {
        public int? OrderNo { get; set; }

        [Required(ErrorMessage = "{0} cannot be bank")]
        [Display(Name ="Order Date")]
        [OrderDateValidator("2000-01-01", ErrorMessage = "Order Date cannot be earlier 2000")]
        public DateTime? OrderDate { get; set; }

        [Required(ErrorMessage = "{0} cannot be bank")]
        [Display(Name = "Invoice Price")]
        [InvoicePriceValidator]
        public double InvoicePrice { get; set; }

        [ListProductValidator]
        public List<Product> Products { get; set; } = new List<Product>();
    }
}