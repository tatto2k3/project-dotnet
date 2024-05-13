using BlueStarMVC.EntityFramwork.Models;
using System;
using System.Collections.Generic;

namespace BlueStarMVC.EntityFramwork.Models;

public partial class FoodForFlight
{
    public Guid FoodId { get; set; }

    public Guid FlightId { get; set; }

    public Guid FoodServiceId { get; set; }

    public Guid FlightDetailId { get; set; }

    public virtual FlightDetail FlightDetail { get; set; } = null!;

    public virtual AddionalFoodService FoodService { get; set; } = null!;
}