using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Dominio
{
    public class Vaga : DbContext
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public float Salario { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
    }
}
