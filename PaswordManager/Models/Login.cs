using System;
using System.Collections.Generic;

namespace PaswordManager.Models;

public partial class Login
{
    public Guid Id { get; set; }

    public string Login1 { get; set; } = null!;

    public DateTime CreateDate { get; set; }

    public DateTime? EditDate { get; set; }

    public DateTime? DeleteDate { get; set; }

    public virtual ICollection<Card> Cards { get; } = new List<Card>();
}
