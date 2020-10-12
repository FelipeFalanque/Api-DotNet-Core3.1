using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.DTOs.Municipio;

namespace Api.Domain.Interfaces.Services.Municipio
{
    public interface IMunicipioService
    {
        Task<MunicipioDTO> Get(Guid id);
        Task<MunicipioCompletoDTO> GetCompleteById(Guid id);
        Task<MunicipioCompletoDTO> GetCompleteByIBGE(int codIBGE);
        Task<IEnumerable<MunicipioDTO>> GetAll();
        Task<MunicipioCreateResultDTO> Post(MunicipioCreateDTO municipio);
        Task<MunicipioUpdateResultDTO> Put(MunicipioUpdateDTO municipio);
        Task<bool> Delete(Guid id);
    }
}
