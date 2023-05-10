using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace SAO.TipoEnvases
{
    public interface ITipoEnvaseRepository : IRepository<TipoEnvase, int>
    {
        Task<List<TipoEnvase>> GetListAsync(
            string filterText = null,
            string desEnvase = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
            string filterText = null,
            string desEnvase = null,
            CancellationToken cancellationToken = default);
    }
}