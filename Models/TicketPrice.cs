using BlueStarMVC.EntityFramwork.Models;
using System;
using System.Collections.Generic;

namespace BlueStarMVC.EntityFramwork.Models;

public partial class TicketPrice
{
    public Guid TicketPriceId { get; set; }

    public Guid FlightId { get; set; }

    public Guid ClassId { get; set; }

    public int AvailableSeats { get; set; }

    public decimal Price { get; set; }

    public virtual TravelClass Class { get; set; } = null!;

    public virtual FlightDetail Flight { get; set; } = null!;
}