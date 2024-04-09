using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestorPagosApi.Identity
{
    public class IdentityData
    {
        public const string AdminUserClaimName = "Administrador";
        public const string AdminUserPolicyName = "AdminPolicy";
        public const string TesoreroUserClaimName = "Tesorero";
        public const string TesoreroUserPolicyName = "TesoreroPolicy";
        public const string ResponsableUserClaimName = "Responsable";
        public const string ResponsableUserPolicyName = "ResponsablePolicy";
    }
}