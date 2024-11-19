using Microsoft.AspNetCore.Identity;

namespace Garage3._0.Models.Entities
{
    public class ApplicationUser: IdentityUser
    {
        public string FName { get; set; }
        public string LName { get; set; }
        public string SocialSecurityNr { get; set; }

        //Nav prop
        public ICollection<ParkedVehicle> Vehicles { get; set; } = new List<ParkedVehicle>();

    }
}
