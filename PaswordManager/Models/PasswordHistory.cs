using System;
using System.Collections.Generic;

namespace PaswordManager.Models;

public partial class PasswordHistory
{
    public Guid Id { get; set; }

    public Guid IdPassword { get; set; }

    public DateTime CreateDate { get; set; }

    public virtual Password IdPasswordNavigation { get; set; } = null!;
}
