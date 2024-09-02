using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ModelBinding_Validation.CustomValidators;

namespace ModelValidations.Models
{
    public class Person : IValidatableObject
    {
        //Validate PersonName cannot but NULL or BLANK
        //To custom error message Required(ErrorMessage = "Message")
        [Required(ErrorMessage = "{0} cannot be NULL or BLANK")]
        [Display(Name = "Person Name")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "{0} cannot less than {2} or greater than {1}")]
        [RegularExpression("^[A-Za-z .]*$", ErrorMessage = "{0} should contain only alphabets, space and dot.")]
        public string? PersonName { get; set; }

        [EmailAddress(ErrorMessage = "{0} should be a proper email address")]
        public string? Email { get; set; }

        [Phone()]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Invalid {0} number")]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "{0} cannot be blank")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "{0} cannot be blank")]
        [Compare("Password", ErrorMessage = "{0} and {1} are not match")]
        [Display(Name = "Confirm Password")]
        public string? ConfirmPassword { get; set; }
        [Range(0, 999.99, ErrorMessage = "{0} cannot out of range between {1} to {2}")]
        public double? Price { get; set; }

        //[MinimumYearValidation(18, ErrorMessage = "Date Of Birth at least {0}")]
        [MinimumYearValidation(18)]
        [BindNever]
        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "From Date")]
        public DateTime? FromDate { get; set; }

        [DateRangeValidator("FromDate", ErrorMessage = "'From Date' should be older than or equal to 'To Date'")]
        [Display(Name = "To Date")]
        public DateTime? ToDate { get; set; }

        public int? Age { get; set; }
        public override string ToString()
        {
            return $"Person object - Person Name: {PersonName}, Email: {Email}, Phone: {Phone}, Password: {Password}, Confirm Password: {ConfirmPassword}, Price: {Price}";
        }

        //This method will be executed when all validation above are clear
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (DateOfBirth.HasValue == false && Age.HasValue == false)
            {
                yield return new ValidationResult("Either DOB or Age cannot be blank", new[] { nameof(Age) });
            }
        }
    }
}