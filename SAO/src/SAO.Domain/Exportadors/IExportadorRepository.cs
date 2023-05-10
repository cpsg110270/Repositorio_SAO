using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace SAO.Exportadors
{
    public interface IExportadorRepository : IRepository<Exportador, Guid>
    {
        Task<List<Exportador>> GetListAsync(
            string filterText = null,
            string nombreExportador = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
            string filterText = null,
            string nombreExportador = null,
            CancellationToken cancellationToken = default);
    }
}