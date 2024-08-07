using System;
using System.Collections.Generic;

namespace healt_plus.Models;

public partial class Padecimiento
{
    public int IdPadecimiento { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<PacientePadecimiento> PacientePadecimientos { get; set; } = new List<PacientePadecimiento>();
}
