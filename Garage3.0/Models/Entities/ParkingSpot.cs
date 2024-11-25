using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Garage3._0.Models.Entities
{
    public class ParkingSpot
    {
       // [Key]
        public int Id { get; set; }//Primärnyckel

        public int SpotNumber { get; set; }
                                   // public string? ParkedVehicleRegistrationNumber { get; set; }
        public bool IsAvailable { get; set; } = true; //Som standardvärde så är platsen ledig
        
        //Navigationproperty till ParkedVehicle
       // public ParkedVehicle ParkedVehicle { get; set; }
    }
}
