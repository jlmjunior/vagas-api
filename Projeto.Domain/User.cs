using System;

namespace Projeto.Domain
{
    public class User : IdentityUser
    {
        public string Nome { get; set; }
        public string Role { get; set; }
        public string OrgId { get; set; }
    }
}
