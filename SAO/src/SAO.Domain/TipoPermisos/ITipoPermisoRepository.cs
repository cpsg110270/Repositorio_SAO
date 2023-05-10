using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace SAO.TipoPermisos
{
    public interface ITipoPermisoRepository : IRepository<TipoPermiso, Guid>
    {
        Task<List<TipoPermiso>> GetListAsync(
            string filterText = null,
            string codigo = null,
            string desripcion = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
            string filterText = null,
            string codigo = null,
            string desripcion = null,
            CancellationToken cancellationToken = default);
    }
}