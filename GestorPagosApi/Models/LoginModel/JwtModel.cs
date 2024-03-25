using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestorPagosApi.Models.LoginModel
{
    public class JwtModel
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Subject { get; set; }
    }
}