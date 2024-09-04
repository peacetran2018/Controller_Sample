using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ECommerceApp.Models;

namespace ECommerceApp.CustomValidators
{
    public class ListProductValidatorAttribute : ValidationAttribute
    {
        public string DefaultErrorMessage { get; set; } = "Product list should have at least 1 item";

        public ListProductValidatorAttribute(){

        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value != null){
                List<Product> products = (List<Product>)value;

                if(products.Count == 0){
                    return new ValidationResult(DefaultErrorMessage);
                }
                return ValidationResult.Success;
            }
            return null;
        }
    }
}