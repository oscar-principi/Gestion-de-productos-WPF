using System;
using System.Collections.Generic;

namespace AMBL_WPF.Domain.Models;

public partial class Producto
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public decimal Precio { get; set; }

    public int Stock { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }
}
