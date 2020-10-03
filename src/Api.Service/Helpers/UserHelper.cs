using System.Threading.Tasks;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Helpers.User;

namespace Api.Service.Helpers
{
    public class UserHelper : IUserHelper
    {
        public void AdicionarPrefixoNome(ref UserEntity userEntity)
        {
            userEntity.Name = $"sr(a). {userEntity.Name}";
        }
    }
}