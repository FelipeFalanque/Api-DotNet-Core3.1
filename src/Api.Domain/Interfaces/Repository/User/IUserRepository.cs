using System.Threading.Tasks;
using Api.Domain.Entities;

namespace Api.Domain.Interfaces.Repository.User
{
    public interface IUserRepository: IRepository<UserEntity>
    {
        Task<UserEntity> SelectByEmailAsync(string email);
    }
}