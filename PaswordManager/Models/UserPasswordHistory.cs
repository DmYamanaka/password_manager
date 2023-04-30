using System;
using System.Collections.Generic;
using PasswordManager.Models.User;

namespace PaswordManager.Models;

public partial class UserPasswordHistory
{
    public Guid Id { get; set; }

    public Guid IdUser { get; set; }

    public DateTime CreateDate { get; set; }

    public virtual User IdUserNavigation { get; set; } = null!;
}
