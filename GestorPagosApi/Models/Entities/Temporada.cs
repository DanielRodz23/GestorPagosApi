using System;
using System.Collections.Generic;

namespace GestorPagosApi.Models.Entities;

public partial class Temporada
{
    public int IdTemporada { get; set; }

    public string Nombre { get; set; } = null!;

    public DateTime FechaInicio { get; set; }

    public DateTime FechaFinal { get; set; }

    public int? IdCategoria { get; set; }

    public decimal Costo { get; set; }

    public ulong TempActual { get; set; }

    public virtual Categoria? IdCategoriaNavigation { get; set; }

    public virtual ICollection<Jugador> Jugador { get; set; } = new List<Jugador>();
}
