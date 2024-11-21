using Garage3._0.Data;
using Garage3._0.Models.Entities;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
namespace Garage3._0.Helper
{
    //Kollar om det finns tillgängliga platser
    public  class AvailabilitySpots
    {
        private readonly ApplicationDbContext _context;
        public async Task<ParkingSpot> GetAvailabilitySpots()
        {
            var isSpotsAvailable = await _context.ParkingSpots
                .FirstOrDefaultAsync(s => s.IsAvailable);
            return isSpotsAvailable;
        }

    }
}
