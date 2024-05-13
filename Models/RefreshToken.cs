using System;
using System.Collections.Generic;

namespace   BlueStarMVC.EntityFramwork.Models;

public partial class RefreshToken
{
    public Guid Id { get; set; }

    public string Token { get; set; } = null!;

    public Guid UserId { get; set; }

    public bool IsUsed { get; set; }

    public virtual User User { get; set; } = null!;
}