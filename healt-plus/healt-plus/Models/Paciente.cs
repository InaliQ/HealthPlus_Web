using System;
using System.Collections.Generic;

namespace healt_plus.Models;

public partial class Paciente
{
    public int IdPaciente { get; set; }

    public int IdPersona { get; set; }

    public string? NumPaciente { get; set; }

    public string? Altura { get; set; }

    public string? Peso { get; set; }

    public string? TipoSangre { get; set; }

    public string? RitmoMin { get; set; }

    public string? RitmoMax { get; set; }

    public bool? Estatus { get; set; }

    public virtual ICollection<Alertum> Alerta { get; set; } = new List<Alertum>();

    public virtual ICollection<HistoricoPacienteEnfermero> HistoricoPacienteEnfermeros { get; set; } = new List<HistoricoPacienteEnfermero>();

    public virtual Persona IdPersonaNavigation { get; set; } = null!;

    public virtual ICollection<MonitoreoSalud> MonitoreoSaluds { get; set; } = new List<MonitoreoSalud>();

    public virtual ICollection<PacienteEnfermero> PacienteEnfermeros { get; set; } = new List<PacienteEnfermero>();

    public virtual ICollection<PacientePadecimiento> PacientePadecimientos { get; set; } = new List<PacientePadecimiento>();

    public virtual ICollection<Recordatorio> Recordatorios { get; set; } = new List<Recordatorio>();
}
