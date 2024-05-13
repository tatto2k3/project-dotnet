namespace BlueStarMVC.Models
{
    public class Seat
    {
        public string SeatId { get; set; }
        public string SeatType { get; set; }
        public string FlightId { get; set; }
        public bool IsBooked { get; set; }
    }
}
