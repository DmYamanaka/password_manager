using System;
using System.Collections.Generic;
using PaswordManager.Models;

namespace PasswordManager.Models.User;

public partial class User
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Photo { get; set; }

    public string Hash { get; set; } = null!;

    public string Salt { get; set; } = null!;

    public DateTime CreateDate { get; set; }

    public DateTime? EditDate { get; set; }

    public DateTime? DeleteDate { get; set; }

    public string EMail { get; set; }

    public string? Phone { get; set; }

    public virtual ICollection<Card> Cards { get; } = new List<Card>();

    public virtual ICollection<UserPasswordHistory> UserPasswordHistories { get; } = new List<UserPasswordHistory>();
}
