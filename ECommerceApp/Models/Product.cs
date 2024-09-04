using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceApp.Models
{
    public class Product
    {
        [Required]
        public int? ProductCode { get; set; }

        [Required]
        public double? Price { get; set; }
        
        [Required]
        public int? Quantity { get; set; }
    }
}