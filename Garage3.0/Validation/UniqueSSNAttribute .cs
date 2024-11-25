using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using Garage3._0.Models.Entities;

public class UniqueSSNAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var userManager = (UserManager<ApplicationUser>)validationContext.GetService(typeof(UserManager<ApplicationUser>));
        var ssn = value?.ToString();

        if (!string.IsNullOrEmpty(ssn))
        {
            // Check if SSN already exists in the database
            var existingUser = userManager.Users.FirstOrDefault(u => u.SocialSecurityNr == ssn);

            if (existingUser != null)
            {
                return new ValidationResult("This Social Security Number is already in use.");
            }
        }

        return ValidationResult.Success;
    }
}
