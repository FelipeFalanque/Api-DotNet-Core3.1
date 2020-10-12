using System;
using System.Threading.Tasks;
using Api.Domain.DTOs.Cep;

namespace Api.Domain.Interfaces.Services.Cep
{
    public interface ICepService
    {
        Task<CepDTO> Get(Guid id);
        Task<CepDTO> Get(string cep);
        Task<CepCreateResultDTO> Post(CepCreateDTO cep);
        Task<CepUpdateResultDTO> Put(CepUpdateDTO cep);
        Task<bool> Delete(Guid id);
    }
}
