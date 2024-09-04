using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ECommerceApp.Models;

namespace ECommerceApp.CustomValidators
{
    public class InvoicePriceValidatorAttribute : ValidationAttribute
    {
        public string DefaultErrorMessage { get; set; } = "Invoice price should be equal total price in product list";
        public InvoicePriceValidatorAttribute()
        {

        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                PropertyInfo? propertyInfo = validationContext.ObjectType.GetProperty(nameof(Order.Products));
                if (propertyInfo != null)
                {
                    List<Product> products = (List<Product>)propertyInfo.GetValue(validationContext.ObjectInstance)!;
                    double? totalPrice = 0.0;
                    foreach (var item in products)
                    {
                        totalPrice += item.Price * item.Quantity;
                    }
                    var invoicePrice = (double)value;
                    if (totalPrice > 0)
                    {
                        if (invoicePrice != totalPrice)
                        {
                            return new ValidationResult(string.Format(ErrorMessage ?? DefaultErrorMessage, invoicePrice), new string[] { nameof(validationContext.MemberName) });
                        }
                    }
                    else
                    {
                        return new ValidationResult("Cannot find Total price");
                    }
                    return ValidationResult.Success;
                }
                return null;
            }
            return null;
        }
    }
}