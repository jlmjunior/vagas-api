using Projeto.Dominio;
using Projeto.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto.Data
{
    public class UsersData : IUsersData
    {
        private readonly Contexto _contexto;

        public UsersData(Contexto contexto)
        {
            _contexto = contexto;
        }

        public User GetAllUsers()
        {
            return _contexto.Users.Where(x => x.Id == 63).FirstOrDefault();
        }
    }
}
