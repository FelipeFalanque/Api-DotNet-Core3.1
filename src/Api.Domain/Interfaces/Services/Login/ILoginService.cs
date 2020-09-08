using System.Threading.Tasks;

namespace Api.Domain.Interfaces.Services.Login
{
    public interface ILoginService
    {
         Task<object> SelectByEmail(string email);
    }
}