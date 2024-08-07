using System;
using System.Collections.Generic;

namespace healt_plus.Models;

public partial class Enfermero
{
    public int IdEnfermero { get; set; }

    public string? Titulo { get; set; }

    public string? NumEnfermero { get; set; }

    public int IdPersona { get; set; }

    public int IdHorario { get; set; }

    public virtual ICollection<HistoricoHorarioEnfermero> HistoricoHorarioEnfermeros { get; set; } = new List<HistoricoHorarioEnfermero>();

    public virtual ICollection<HistoricoPacienteEnfermero> HistoricoPacienteEnfermeros { get; set; } = new List<HistoricoPacienteEnfermero>();

    public virtual Horario IdHorarioNavigation { get; set; } = null!;

    public virtual Persona IdPersonaNavigation { get; set; } = null!;

    public virtual ICollection<PacienteEnfermero> PacienteEnfermeros { get; set; } = new List<PacienteEnfermero>();
}
