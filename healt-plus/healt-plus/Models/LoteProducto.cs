using System;
using System.Collections.Generic;

namespace healt_plus.Models;

public partial class LoteProducto
{
    public int IdLote { get; set; }

    public double? Precio { get; set; }

    public int? Cantidad { get; set; }

    public string? Modelo { get; set; }

    public double? PrecioLote { get; set; }

    public int IdServicio { get; set; }

    public virtual Servicio IdServicioNavigation { get; set; } = null!;
}
