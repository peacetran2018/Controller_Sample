using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ModelBinding_Validation.CustomValidators
{
    public class DateRangeValidatorAttribute : ValidationAttribute
    {
        public string OtherPropertyName { get; set; }
        public DateRangeValidatorAttribute(string otherPropertyName){
            OtherPropertyName = otherPropertyName;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value != null){
                //Get to_date
                DateTime to_date = Convert.ToDateTime(value);

                //Get preference from property from_date
                PropertyInfo? otherProperty = validationContext.ObjectType.GetProperty(OtherPropertyName);

                if(otherProperty != null){
                    DateTime from_date = Convert.ToDateTime(otherProperty.GetValue(validationContext.ObjectInstance));
                    if(from_date > to_date){
                        return new ValidationResult(ErrorMessage);
                    }
                    else{
                        return ValidationResult.Success;
                    }
                }
            }
            return null;
        }
    }
}