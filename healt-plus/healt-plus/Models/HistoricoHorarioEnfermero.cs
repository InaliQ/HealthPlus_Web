using System;
using System.Collections.Generic;

namespace healt_plus.Models;

public partial class HistoricoHorarioEnfermero
{
    public int IdHistorico { get; set; }

    public int IdEnfermero { get; set; }

    public int IdHorario { get; set; }

    public DateTime? FechaInicio { get; set; }

    public DateTime? FechaFin { get; set; }

    public virtual Enfermero IdEnfermeroNavigation { get; set; } = null!;

    public virtual Horario IdHorarioNavigation { get; set; } = null!;
}
