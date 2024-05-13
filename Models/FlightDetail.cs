using BlueStarMVC.EntityFramwork.Models;
using System;
using System.Collections.Generic;

namespace BlueStarMVC.EntityFramwork.Models;

public partial class FlightDetail
{
    public Guid Id { get; set; }

    public Guid SourceAirportId { get; set; }

    public Guid DestinationAirportId { get; set; }

    public DateTime DepartureDate { get; set; }

    public DateTime DepartureTime { get; set; }

    public DateTime ArrivalTime { get; set; }

    public string FilghtName { get; set; } = null!;

    public string AirlineType { get; set; } = null!;

    public virtual Airport DestinationAirport { get; set; } = null!;

    public virtual ICollection<FoodForFlight> FoodForFlights { get; set; } = new List<FoodForFlight>();

    public virtual ICollection<SeatDetail> SeatDetails { get; set; } = new List<SeatDetail>();

    public virtual Airport SourceAirport { get; set; } = null!;

    public virtual ICollection<TicketPrice> TicketPrices { get; set; } = new List<TicketPrice>();

    public virtual ICollection<AddionalFoodService> AddionalFoodServices { get; set; } = new List<AddionalFoodService>();
}