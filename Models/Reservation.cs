using BlueStarMVC.EntityFramwork.Models;
using System;
using System.Collections.Generic;

namespace BlueStarMVC.EntityFramwork.Models;

public partial class Reservation
{
    public Guid Id { get; set; }

    public Guid PassengerIdid { get; set; }

    public Guid SeatDetailsIdid { get; set; }

    public DateTime DateOfReservation { get; set; }

    public string RervationCode { get; set; } = null!;

    public bool IsConfirmed { get; set; }

    public virtual Passenger PassengerId { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<ReservationMapAddionalFoodService> ReservationMapAddionalFoodServices { get; set; } = new List<ReservationMapAddionalFoodService>();

    public virtual SeatDetail SeatDetailsId { get; set; } = null!;

    public virtual ICollection<AddionalFoodService> AddionalFoodServices { get; set; } = new List<AddionalFoodService>();
}