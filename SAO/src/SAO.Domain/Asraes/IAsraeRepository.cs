using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace SAO.Asraes
{
    public interface IAsraeRepository : IRepository<Asrae, int>
    {
        Task<List<Asrae>> GetListAsync(
            string filterText = null,
            string codigo_ASHRAE = null,
            string descripcion = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
            string filterText = null,
            string codigo_ASHRAE = null,
            string descripcion = null,
            CancellationToken cancellationToken = default);
    }
}