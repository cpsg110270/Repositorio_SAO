using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace SAO.Almacens
{
    public interface IAlmacenRepository : IRepository<Almacen, int>
    {
        Task<List<Almacen>> GetListAsync(
            string filterText = null,
            string nombreAlmacen = null,
            string siglaAlmacen = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
            string filterText = null,
            string nombreAlmacen = null,
            string siglaAlmacen = null,
            CancellationToken cancellationToken = default);
    }
}