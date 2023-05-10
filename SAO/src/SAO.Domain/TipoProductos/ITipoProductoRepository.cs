using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace SAO.TipoProductos
{
    public interface ITipoProductoRepository : IRepository<TipoProducto, Guid>
    {
        Task<List<TipoProducto>> GetListAsync(
            string filterText = null,
            string desProducto = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
            string filterText = null,
            string desProducto = null,
            CancellationToken cancellationToken = default);
    }
}