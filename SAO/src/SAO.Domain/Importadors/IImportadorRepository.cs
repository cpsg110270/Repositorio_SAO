using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace SAO.Importadors
{
    public interface IImportadorRepository : IRepository<Importador, Guid>
    {
        Task<List<Importador>> GetListAsync(
            string filterText = null,
            int? noImportadorMin = null,
            int? noImportadorMax = null,
            string noRUC = null,
            string nombreImportador = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
            string filterText = null,
            int? noImportadorMin = null,
            int? noImportadorMax = null,
            string noRUC = null,
            string nombreImportador = null,
            CancellationToken cancellationToken = default);
    }
}