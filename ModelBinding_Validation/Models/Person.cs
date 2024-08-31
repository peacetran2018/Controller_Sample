using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ModelValidations.Models
{
    public class Person
    {
        //Validate PersonName cannot but NULL or BLANK
        //To custom error message Required(ErrorMessage = "Message")
        [Required(ErrorMessage = "Person Name cannot be NULL or BLANK")]
        public string? PersonName { get; set; }
        public string? Email {get; set; }
        public string? Phone { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public double? Price { get; set; }

        public override string ToString()
        {
            return $"Person object - Person Name: {PersonName}, Email: {Email}, Phone: {Phone}, Password: {Password}, Confirm Password: {ConfirmPassword}, Price: {Price}";
        }
    }
}