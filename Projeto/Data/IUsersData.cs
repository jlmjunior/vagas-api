using Projeto.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto.Data
{
    public interface IUsersData
    {
        public User GetAllUsers();
    }
}
