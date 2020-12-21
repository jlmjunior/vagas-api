using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Projeto.Dominio
{
    public class User : IdentityUser<int>
    {
        public string Nome { get; set; }
        public string Cargo { get; set; } = "Member";
        public string OrgId { get; set; }
        public List<UserRole> UserRoles { get; set; }
    }
}
