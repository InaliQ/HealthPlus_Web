using System;
using System.Collections.Generic;

namespace healt_plus.Models;

public partial class Alertum
{
    public int IdAlerta { get; set; }

    public int? IdPaciente { get; set; }

    public DateTime? FechaHora { get; set; }

    public string? Descripcion { get; set; }

    public virtual Paciente? IdPacienteNavigation { get; set; }
}
