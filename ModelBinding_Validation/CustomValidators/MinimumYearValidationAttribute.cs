using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ModelBinding_Validation.CustomValidators
{
    public class MinimumYearValidationAttribute : ValidationAttribute
    {
        public int MinimumYear{get;set;}
        public string DefaultErrorMessage {get;set;} = "Age should be greater than 18";
        public MinimumYearValidationAttribute(){

        }

        public MinimumYearValidationAttribute(int MinimumYear){
            this.MinimumYear = MinimumYear;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext){
            if(value != null){
                DateTime date = (DateTime)value;
                if((DateTime.Now.Year - date.Year) < MinimumYear){
                    return new ValidationResult(string.Format(ErrorMessage ?? DefaultErrorMessage, MinimumYear));
                }
                else{
                    return ValidationResult.Success;
                }
            }

            return null;
        }
    }
}