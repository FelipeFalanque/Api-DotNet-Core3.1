using System.Threading.Tasks;
using Api.Domain.Entities;

namespace Api.Domain.Interfaces.Helpers.User
{
    public interface IUserHelper
    {
        void AdicionarPrefixoNome(ref UserEntity userEntity);
    }
}