using Garage3._0.Models.Entities;

namespace Garage3._0.Models.ViewModels
{
    public class StatisticsViewModel
    {
        public VehicleType Type { get; set; }
        public string? Color { get; set; }
        public string? Brand { get; set; }
        public int Wheel { get; set; }
        public string? Model { get; set; }
        public decimal ParkingFee { get; set; }
    }
}
