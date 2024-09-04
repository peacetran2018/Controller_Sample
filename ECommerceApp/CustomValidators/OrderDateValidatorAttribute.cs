using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceApp.CustomValidators
{
    public class OrderDateValidatorAttribute : ValidationAttribute
    {
        public string DefaultErrorMessage { get; set; } = "Order date should be greater than or equal 01-01-2000";
        public DateTime TargetDateTime {get; set; }
        public OrderDateValidatorAttribute(){

        }
        public OrderDateValidatorAttribute(string TargetDateTime){
            this.TargetDateTime = Convert.ToDateTime(TargetDateTime);
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext){
            if(value != null){
                DateTime orderDate = (DateTime)value;
                if(orderDate < TargetDateTime){
                    return new ValidationResult(string.Format(ErrorMessage ?? DefaultErrorMessage, TargetDateTime.ToString("yyyy-MM-dd")), new string[] { nameof(validationContext.MemberName)});
                }
                return ValidationResult.Success;
            }
            return null;
        }
    }
}