using System.ComponentModel;
using Garage3._0.Models.Entities;

namespace Garage3._0.Models.ViewModels
{
    public class ParkedViewModel
    {
        public int Id { get; set; }
        public VehicleType Type { get; set; }

        [DisplayName("Registration Number")]
        public string? RegistrationNumber { get; set; }
        public DateTime ArrivalTime { get; set; }

        [DisplayName("Parked Time")]
        public TimeSpan ParkedTime { get; set; }
    }
}
