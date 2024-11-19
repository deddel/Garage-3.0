using Microsoft.AspNetCore.Identity;

namespace Garage3._0.Models.Entities
{
    public class ApplicationUser: IdentityUser
    {
        public string Password {  get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string SocialSecurityNr { get; set; }

        public ICollection<ParkedVehicle> Vehicles { get; set; } = new List<ParkedVehicle>();

    }
}
