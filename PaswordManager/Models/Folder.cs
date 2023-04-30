using System;
using System.Collections.Generic;

namespace PaswordManager.Models;

public partial class Folder
{
    public Guid Id { get; set; }

    public Guid? IdCard { get; set; }

    public string? Name { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime? EditDate { get; set; }

    public DateTime? DeleteDate { get; set; }

    public virtual Card? IdCardNavigation { get; set; }
}
