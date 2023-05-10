using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace SAO.Fabricantes
{
    public interface IFabricanteRepository : IRepository<Fabricante, Guid>
    {
        Task<List<Fabricante>> GetListAsync(
            string filterText = null,
            string nombreFabricante = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
            string filterText = null,
            string nombreFabricante = null,
            CancellationToken cancellationToken = default);
    }
}