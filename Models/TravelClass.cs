using BlueStarMVC.EntityFramwork.Models;
using System;
using System.Collections.Generic;

namespace BlueStarMVC.EntityFramwork.Models;

public partial class TravelClass
{
    public Guid Id { get; set; }

    public string TravelClassName { get; set; } = null!;

    public virtual ICollection<SeatDetail> SeatDetails { get; set; } = new List<SeatDetail>();

    public virtual ICollection<TicketPrice> TicketPrices { get; set; } = new List<TicketPrice>();

    public virtual ICollection<ServiceForClass> ServiceForClasses { get; set; } = new List<ServiceForClass>();
}