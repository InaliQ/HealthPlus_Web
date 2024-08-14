using System;
using System.Collections.Generic;

namespace healt_plus.Models;

public partial class Horario
{
    public int IdHorario { get; set; }

    public string? HoraInicio { get; set; }

    public string? HoraFin { get; set; }

    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();

    public virtual ICollection<Enfermero> Enfermeros { get; set; } = new List<Enfermero>();

    public virtual ICollection<HistoricoHorarioEnfermero> HistoricoHorarioEnfermeros { get; set; } = new List<HistoricoHorarioEnfermero>();
}
