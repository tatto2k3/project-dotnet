using System;
using System.Collections.Generic;

namespace BlueStarMVC.Models;

public partial class Chuyenbay
{
    public string FlyId { get; set; } = null!;

    public string? PlId { get; set; }

    public string? AirportId { get; set; }

    public string? FromLocation { get; set; }

    public string? ToLocation { get; set; }

    public string? DepartureTime { get; set; }

    public string? ArrivalTime { get; set; }

    public string? DepartureDay { get; set; }

    public int? OriginalPrice { get; set; }
}
