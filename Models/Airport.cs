using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;

namespace BlueStarMVC.EntityFramwork.Models;

public partial class Airport
{
    public Guid Id { get; set; }

    public string AirportName { get; set; } = null!;

    public Guid CountryId { get; set; }

    public string AirportCode { get; set; } = null!;

    public string AirportCity { get; set; } = null!;

    public virtual Country Country { get; set; } = null!;

    public virtual ICollection<FlightDetail> FlightDetailDestinationAirports { get; set; } = new List<FlightDetail>();

    public virtual ICollection<FlightDetail> FlightDetailSourceAirports { get; set; } = new List<FlightDetail>();
}