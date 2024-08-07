using System;
using System.Collections.Generic;

namespace healt_plus.Models;

public partial class MonitoreoSalud
{
    public int IdMonitoreo { get; set; }

    public int? IdPaciente { get; set; }

    public DateTime? FechaHora { get; set; }

    public int? RitmoCardiaco { get; set; }

    public string? Tipo { get; set; }

    public virtual Paciente? IdPacienteNavigation { get; set; }
}
