using System;
using System.Collections.Generic;

namespace GestorPagosApi.Models.Entities;

public partial class JugadoresTemporada
{
    public int Id { get; set; }

    public int IdJugador { get; set; }

    public int IdTemporada { get; set; }

    public virtual Jugador IdJugadorNavigation { get; set; } = null!;

    public virtual Temporada IdTemporadaNavigation { get; set; } = null!;
}
