using System;
using System.Collections.Generic;

namespace BlueStarMVC.EntityFramwork.Models;

public partial class TokenRemainLogin
{
    public Guid Id { get; set; }

    public string TokenId { get; set; } = null!;

    public Guid UserId { get; set; }

    public bool IsExpired { get; set; }

    public virtual User User { get; set; } = null!;
}
