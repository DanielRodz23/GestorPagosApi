using System;
using System.Collections.Generic;

namespace GestorPagosApi.Models.Entities;

public partial class Categoria
{
    public int IdCategoria { get; set; }

    public string NombreCategoria { get; set; } = null!;

    public int? EdadInicial { get; set; }

    public int? EdadTermino { get; set; }

    public virtual ICollection<Temporada> Temporada { get; set; } = new List<Temporada>();
}
