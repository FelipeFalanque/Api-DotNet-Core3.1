using System.Threading.Tasks;
using Api.Domain.Entities;

namespace Api.Domain.Interfaces.Services.Login
{
    public interface ILoginService
    {
         Task<UserEntity> SelectByEmail(string email);
    }
}