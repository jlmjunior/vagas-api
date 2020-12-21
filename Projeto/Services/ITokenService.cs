using Projeto.Dominio;
using System.Threading.Tasks;

namespace Projeto.Services
{
    public interface ITokenService
    {
        public Task<string> GenerateToken(User user);
    }
}
