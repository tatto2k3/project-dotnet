using System;
using System.Collections.Generic;

namespace BlueStarMVC.Models;

public partial class Ticket
{
    public string TId { get; set; } = null!;

    public string Cccd { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string FlyId { get; set; } = null!;

    public string KgId { get; set; } = null!;

    public string SeatId { get; set; } = null!;

    public string? FoodId { get; set; }

    public long TicketPrice { get; set; }

    public string Mail { get; set; } = null!;

    public string? DisId { get; set; }
}
