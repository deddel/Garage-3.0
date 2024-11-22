using System.ComponentModel.DataAnnotations;

namespace Garage3._0.Models.ViewModels
{
    public class ParkedVehicleViewModel
    {
        public int VehicleTypeId { get; set; }
        [Required]
        [RegularExpression(@"^[A-Za-z]{3}[0-9]{3}$", ErrorMessage = "Registration number must follow the format ABC123.")]
        public string? RegistrationNumber { get; set; }
        public string? Color { get; set; }
        public string? Brand { get; set; }
        public string? VehicleModel { get; set; }
        public int Wheel { get; set; }
        public int ParkingSpotId { get; set; }
    }
}
