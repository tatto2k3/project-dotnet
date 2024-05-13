using BlueStarMVC.EntityFramwork.Models;
using System;
using System.Collections.Generic;

namespace BlueStarMVC.EntityFramwork.Models;

public partial class User
{
    public Guid Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string VerifyCode { get; set; } = null!;

    public bool IsVerified { get; set; }

    public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();

    public virtual ICollection<TokenRemainLogin> TokenRemainLogins { get; set; } = new List<TokenRemainLogin>();

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}