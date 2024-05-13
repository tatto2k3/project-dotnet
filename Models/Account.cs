using System;
using System.Collections.Generic;

namespace BlueStarMVC.Models;

public partial class Account
{
    public string Email { get; set; } = null!;

    public string? Password { get; set; }

    public string? Name { get; set; }
    public string? Position { get; set; }

    // public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
}
