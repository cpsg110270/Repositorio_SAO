using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace SAO.Paiss
{
    public interface IPaisRepository : IRepository<Pais, int>
    {
        Task<List<Pais>> GetListAsync(
            string filterText = null,
            string nombrePais = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
            string filterText = null,
            string nombrePais = null,
            CancellationToken cancellationToken = default);
    }
}