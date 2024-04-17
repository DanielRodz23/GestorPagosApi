using System;
using System.Collections.Generic;

namespace GestorPagosApi.Models.Entities;

/// <summary>
/// Tabla que contiene la información de los jugadores registrador.
/// </summary>
public partial class Jugador
{
    public int IdJugador { get; set; }

    public string Nombre { get; set; } = null!;

    public DateOnly Dob { get; set; }

    public int IdUsuario { get; set; }

    public int? IdTemporada { get; set; }

    public decimal Deuda { get; set; }

    public ulong Exists { get; set; }

    public virtual Temporada? IdTemporadaNavigation { get; set; }

    public virtual Usuarios IdUsuarioNavigation { get; set; } = null!;

    public virtual ICollection<JugadoresTemporada> JugadoresTemporada { get; set; } = new List<JugadoresTemporada>();

    public virtual ICollection<Pago> Pago { get; set; } = new List<Pago>();
}
