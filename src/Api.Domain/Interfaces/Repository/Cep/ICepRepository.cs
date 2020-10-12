using System.Threading.Tasks;
using Api.Domain.Entities;

namespace Api.Domain.Interfaces.Repository.Cep
{
    public interface ICepRepository : IRepository<CepEntity>
    {
         Task<CepEntity> SelectAsync(string cep);
    }
}