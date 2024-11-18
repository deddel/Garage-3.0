namespace Garage3._0.Models.ViewModels
{
    public class ReceiptViewModel
    {
        public int Id { get; set; }
        public string? RegistrationNumber { get; set; }
        public DateTime ArrivalTime { get; set; }
        public DateTime DepartureTime { get; set; }
        public TimeSpan ParkedTime { get; set; }
        public decimal? ParkedFee { get; set; }
    }
}
