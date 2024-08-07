using System;
using System.Collections.Generic;

namespace healt_plus.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string? Usuario1 { get; set; }

    public string? Contrasenia { get; set; }

    public int IdPersona { get; set; }

    public bool? Estatus { get; set; }

    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();

    public virtual Persona IdPersonaNavigation { get; set; } = null!;
}
