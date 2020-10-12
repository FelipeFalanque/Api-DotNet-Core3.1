using System.Linq;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Data.Repository;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Repository.Cep;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Implementations
{
    public class CepRepository : BaseRepository<CepEntity>, ICepRepository
    {
        private DbSet<CepEntity> _dataset;

        public CepRepository(MyContext context) : base(context)
        {
            _dataset = context.Set<CepEntity>();
        }

        public async Task<CepEntity> SelectAsync(string cep)
        {
            return await _dataset.Include(c => c.Municipio)
                                 .ThenInclude(m => m.Uf)
                                 .SingleOrDefaultAsync(u => u.Cep.Equals(cep));
        }
    }
}
