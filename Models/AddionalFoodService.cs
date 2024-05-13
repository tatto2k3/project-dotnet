using System;
using System.Collections.Generic;

namespace BlueStarMVC.EntityFramwork.Models;

public partial class AddionalFoodService
{
    public Guid Id { get; set; }

    public string FoodName { get; set; } = null!;

    public string FoodDescription { get; set; } = null!;

    public decimal FoodPrice { get; set; }

    public virtual ICollection<FoodForFlight> FoodForFlights { get; set; } = new List<FoodForFlight>();

    public virtual ICollection<ReservationMapAddionalFoodService> ReservationMapAddionalFoodServices { get; set; } = new List<ReservationMapAddionalFoodService>();

    public virtual ICollection<FlightDetail> FlightDetails { get; set; } = new List<FlightDetail>();

    public virtual ICollection<Reservation> ReservationDetails { get; set; } = new List<Reservation>();
}