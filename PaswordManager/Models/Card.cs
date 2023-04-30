using System;
using System.Collections.Generic;
using PasswordManager.Models.User;

namespace PaswordManager.Models;

public partial class Card
{
    public Guid Id { get; set; }

    public Guid IdPassword { get; set; }

    public Guid IdLogin { get; set; }

    public Guid IdUser { get; set; }

    public string? LinkResource { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime? EditDate { get; set; }

    public DateTime? DeleteDate { get; set; }

    public virtual ICollection<Folder> Folders { get; } = new List<Folder>();

    public virtual Login IdLoginNavigation { get; set; } = null!;

    public virtual Password IdPasswordNavigation { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;
}
