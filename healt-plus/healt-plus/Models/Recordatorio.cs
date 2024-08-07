using System;
using System.Collections.Generic;

namespace healt_plus.Models;

public partial class Recordatorio
{
    public int IdRecordatorio { get; set; }

    public string? Medicamento { get; set; }

    public string? CantidadMedicamento { get; set; }

    public int IdPaciente { get; set; }

    public DateTime? FechaInicio { get; set; }

    public DateTime? FechaFin { get; set; }

    public bool? Estatus { get; set; }

    public virtual Paciente IdPacienteNavigation { get; set; } = null!;
}
