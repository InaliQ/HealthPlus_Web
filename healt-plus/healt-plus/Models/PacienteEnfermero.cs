using System;
using System.Collections.Generic;

namespace healt_plus.Models;

public partial class PacienteEnfermero
{
    public int IdPacienteEnfermero { get; set; }

    public int IdPaciente { get; set; }

    public int IdEnfermero { get; set; }

    public virtual Enfermero IdEnfermeroNavigation { get; set; } = null!;

    public virtual Paciente IdPacienteNavigation { get; set; } = null!;
}
