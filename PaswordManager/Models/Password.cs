using System;
using System.Collections.Generic;

namespace PaswordManager.Models;

public partial class Password
{
    public Guid Id { get; set; }

    public string Hash { get; set; } = null!;

    public string Salt { get; set; } = null!;

    public DateTime CreateDate { get; set; }

    public DateTime? EditDate { get; set; }

    public DateTime? DeleteDate { get; set; }

    public virtual ICollection<Card> Cards { get; } = new List<Card>();

    public virtual ICollection<PasswordHistory> PasswordHistories { get; } = new List<PasswordHistory>();
}
