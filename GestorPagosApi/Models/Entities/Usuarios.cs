using System;
using System.Collections.Generic;

namespace GestorPagosApi.Models.Entities;

/// <summary>
/// Esta tabla almacena la informacion del responsable de los jugadores.
/// </summary>
public partial class Usuarios
{
    public int IdUsuario { get; set; }

    public string Nombre { get; set; } = null!;

    public string Usuario { get; set; } = null!;

    public string Contrasena { get; set; } = null!;

    public int IdRol { get; set; }

    public virtual Roles IdRolNavigation { get; set; } = null!;

    public virtual ICollection<Jugador> Jugador { get; set; } = new List<Jugador>();
}
