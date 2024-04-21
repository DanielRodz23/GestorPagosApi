using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestorPagosApi.DTOs;

namespace GestorPagosApi.Models.LoginModel
{
    public class UserDTOToken
    {
        public UsuarioDTO Usuario { get; set; }
        public string TokenString { get; set; }
    }
}