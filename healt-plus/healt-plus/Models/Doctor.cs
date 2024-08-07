using System;
using System.Collections.Generic;

namespace healt_plus.Models;

public partial class Doctor
{
    public int IdDoctor { get; set; }

    public string? Cedula { get; set; }

    public string? NumDoctor { get; set; }

    public int IdPersona { get; set; }

    public int IdHorario { get; set; }

    public virtual Horario IdHorarioNavigation { get; set; } = null!;

    public virtual Persona IdPersonaNavigation { get; set; } = null!;
}
