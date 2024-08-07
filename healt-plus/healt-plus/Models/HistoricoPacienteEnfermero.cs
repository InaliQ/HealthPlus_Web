using System;
using System.Collections.Generic;

namespace healt_plus.Models;

public partial class HistoricoPacienteEnfermero
{
    public int IdHistorico { get; set; }

    public int IdPaciente { get; set; }

    public int IdEnfermero { get; set; }

    public DateTime FechaInicio { get; set; }

    public DateTime? FechaFin { get; set; }

    public virtual Enfermero IdEnfermeroNavigation { get; set; } = null!;

    public virtual Paciente IdPacienteNavigation { get; set; } = null!;
}
