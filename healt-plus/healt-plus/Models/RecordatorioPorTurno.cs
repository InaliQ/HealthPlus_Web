using System;
using System.Collections.Generic;

namespace healt_plus.Models;

public partial class RecordatorioPorTurno
{
    public int IdRecordatorio { get; set; }

    public int IdEnfermero { get; set; }

    public virtual Enfermero IdEnfermeroNavigation { get; set; } = null!;

    public virtual Recordatorio IdRecordatorioNavigation { get; set; } = null!;
}
