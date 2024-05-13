using System;
using System.Collections.Generic;

namespace BlueStarMVC.EntityFramwork.Models;

public partial class AdditionalSeatService
{
    public Guid Id { get; set; }

    public string SeatLevel { get; set; } = null!;

    public string SeatType { get; set; } = null!;

    public decimal SeatPrice { get; set; }

    public virtual ICollection<SeatDetail> SeatDetails { get; set; } = new List<SeatDetail>();
}