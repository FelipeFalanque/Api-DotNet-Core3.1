using System.Threading.Tasks;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Repository.User;
using Api.Domain.Interfaces.Services.Login;

namespace Api.Service.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUserRepository _repository;
        public LoginService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserEntity> SelectByEmail(string email)
        {
            return await _repository.SelectByEmailAsync(email);
        }
    }
}