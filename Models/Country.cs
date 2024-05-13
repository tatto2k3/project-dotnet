using BlueStarMVC.EntityFramwork.Models;
using System;
using System.Collections.Generic;

namespace BlueStarMVC.EntityFramwork.Models;

public partial class Country
{
    public Guid Id { get; set; }

    public string ContryName { get; set; } = null!;

    public string CountryCode { get; set; } = null!;

    public bool Active { get; set; }

    public virtual ICollection<Airport> Airports { get; set; } = new List<Airport>();
}