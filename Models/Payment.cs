using System;
using System.Collections.Generic;

namespace BlueStarMVC.EntityFramwork.Models;

public partial class Payment
{
    public Guid Id { get; set; }

    public Guid ReservationIdid { get; set; }

    public bool Status { get; set; }

    public decimal Amount { get; set; }

    public DateTime PaymentDate { get; set; }

    public virtual Reservation ReservationId { get; set; } = null!;
}