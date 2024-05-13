using BlueStarMVC.EntityFramwork.Models;
using System;
using System.Collections.Generic;

namespace   BlueStarMVC.EntityFramwork.Models;

public partial class ReservationMapAddionalFoodService
{
    public Guid ReservationId { get; set; }

    public Guid AdditionalFoodServiceId { get; set; }

    public int NumberOfMeals { get; set; }

    public virtual AddionalFoodService AdditionalFoodService { get; set; } = null!;

    public virtual Reservation Reservation { get; set; } = null!;
}