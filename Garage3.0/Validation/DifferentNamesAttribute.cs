using Garage3._0.Areas.Identity.Pages.Account;
using Garage3._0.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace Garage3._0.Validation
{
    public class DifferentNamesAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = (RegisterModel.InputModel)validationContext.ObjectInstance;
            
             // if (model.fu == model.LastName)
          
            if (model.FName == value.ToString())
            {
                return new ValidationResult("First name and last name cannot be the same.");
            }

            return ValidationResult.Success;
        }
    }
}
