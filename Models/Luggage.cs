using System;
using System.Collections.Generic;

namespace BlueStarMVC.Models;

public partial class Luggage
{
    public string LuggageCode { get; set; } = null!;

    public string? Mass { get; set; }

    public int? Price { get; set; }
}
