using System;
using System.Collections.Generic;

namespace GestorPagosApi.Models.Entities;

public partial class Pago
{
    public int IdPago { get; set; }

    public DateTime FechaPago { get; set; }

    public int IdJugador { get; set; }

    public decimal CantidadPago { get; set; }

    public virtual Jugador IdJugadorNavigation { get; set; } = null!;
}
