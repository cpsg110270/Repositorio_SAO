using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace SAO.SustanciaElementals
{
    public interface ISustanciaElementalRepository : IRepository<SustanciaElemental, Guid>
    {
        Task<List<SustanciaElemental>> GetListAsync(
            string filterText = null,
            string codCas = null,
            string desSustancia = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
            string filterText = null,
            string codCas = null,
            string desSustancia = null,
            CancellationToken cancellationToken = default);
    }
}