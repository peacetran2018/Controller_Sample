using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ModelValidations.Models;

namespace ModelBinding_Validation.CustomModelBinder
{
    public class PersonModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            Person person = new Person();
            //Fist Name and Last Name
            if(bindingContext.ValueProvider.GetValue("FirstName").Length > 0){
                person.PersonName = bindingContext.ValueProvider.GetValue("FirstName").FirstValue;
                if(bindingContext.ValueProvider.GetValue("LastName").Length > 0){
                    person.PersonName += " " + bindingContext.ValueProvider.GetValue("LastName");
                }
            }
            //Email
            if(bindingContext.ValueProvider.GetValue("Email").Length > 0){  
                person.Email = bindingContext.ValueProvider.GetValue("Email").FirstValue;
            }

            //Phone
            if(bindingContext.ValueProvider.GetValue("Phone").Length > 0){  
                person.Phone = bindingContext.ValueProvider.GetValue("Phone").FirstValue;
            }

            //Password
            if(bindingContext.ValueProvider.GetValue("Password").Length > 0){  
                person.Password = bindingContext.ValueProvider.GetValue("Password").FirstValue;
            }

            //ConfirmPassword
            if(bindingContext.ValueProvider.GetValue("ConfirmPassword").Length > 0){  
                person.ConfirmPassword = bindingContext.ValueProvider.GetValue("ConfirmPassword").FirstValue;
            }

            //Price
            if(bindingContext.ValueProvider.GetValue("Price").Length > 0){  
                person.Price = Convert.ToDouble(bindingContext.ValueProvider.GetValue("Price").FirstValue);
            }

            //DOB
            if(bindingContext.ValueProvider.GetValue("DateOfBirth").Length > 0){  
                person.DateOfBirth = Convert.ToDateTime(bindingContext.ValueProvider.GetValue("DateOfBirth").FirstValue);
            }

            //Returns model object after reading data from the request
            bindingContext.Result = ModelBindingResult.Success(person);
            return Task.CompletedTask;
        }
    }
}