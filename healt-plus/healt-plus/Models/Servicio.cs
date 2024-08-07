using System;
using System.Collections.Generic;

namespace healt_plus.Models;

public partial class Servicio
{
    public int IdServicio { get; set; }

    public DateTime? FechaInicio { get; set; }

    public DateTime? FechaFin { get; set; }

    public double? Precio { get; set; }

    public DateTime? FechaPago { get; set; }

    public bool? Estatus { get; set; }

    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();

    public virtual ICollection<LoteProducto> LoteProductos { get; set; } = new List<LoteProducto>();
}
