using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace SAO.PuertoEntradaSalidas
{
    public interface IPuertoEntradaSalidaRepository : IRepository<PuertoEntradaSalida, int>
    {
        Task<List<PuertoEntradaSalida>> GetListAsync(
            string filterText = null,
            string nombrePuerto = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
            string filterText = null,
            string nombrePuerto = null,
            CancellationToken cancellationToken = default);
    }
}