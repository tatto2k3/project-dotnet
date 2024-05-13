using BlueStarMVC.EntityFramwork.Models;
using System;
using System.Collections.Generic;

namespace BlueStarMVC.EntityFramwork.Models;

public partial class SeatDetail
{
    public Guid Id { get; set; }

    public string SeatCode { get; set; } = null!;

    public Guid ClassId { get; set; }

    public Guid FlightId { get; set; }

    public Guid SeatAdditionalServiceId { get; set; }

    public bool IsBooked { get; set; }

    public virtual TravelClass Class { get; set; } = null!;

    public virtual FlightDetail Flight { get; set; } = null!;

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    public virtual AdditionalSeatService SeatAdditionalService { get; set; } = null!;
}