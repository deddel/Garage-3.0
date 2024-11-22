using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Garage3._0.Models.Entities
{
    public class ParkingSpot
    {
        [Key]
        public int SpotId { get; set; } //Primärnyckel
        public string? RegNumber { get; set; }
        public bool IsAvailable { get; set; } = true; //Som standardvärde så är platsen ledig

        //Navigationproperty till ParkedVehicle
        [ForeignKey(nameof(RegNumber))]
        public ParkedVehicle ParkedVehicle { get; set; }
    }
}
