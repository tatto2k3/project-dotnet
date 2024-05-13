using System;
using System.Collections.Generic;

namespace BlueStarMVC.Models;

public partial class Customer
{
    public string CId { get; set; } = null!;

    public string? NumId { get; set; }

    public string? Fullname { get; set; }

    public string? Point { get; set; }

    public string? Mail { get; set; }

   // public virtual Account MailNavigation { get; set; }


}
