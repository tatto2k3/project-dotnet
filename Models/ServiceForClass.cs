using System;
using System.Collections.Generic;

namespace BlueStarMVC.EntityFramwork.Models;

public partial class ServiceForClass
{
    public Guid Id { get; set; }

    public string ServiceName { get; set; } = null!;

    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public string ServcieDescription { get; set; } = null!;

    public virtual ICollection<TravelClass> TravelClasses { get; set; } = new List<TravelClass>();
}