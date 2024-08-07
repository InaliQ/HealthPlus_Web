using System;
using System.Collections.Generic;

namespace healt_plus.Models;

public partial class Cliente
{
    public int IdCliente { get; set; }

    public int IdUsuario { get; set; }

    public int IdServicio { get; set; }

    public bool? Estatus { get; set; }

    public virtual Servicio IdServicioNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
