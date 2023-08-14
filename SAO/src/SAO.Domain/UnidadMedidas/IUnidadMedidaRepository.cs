using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace SAO.UnidadMedidas
{
    public interface IUnidadMedidaRepository : IRepository<UnidadMedida, int>
    {
        Task<List<UnidadMedida>> GetListAsync(
            string filterText = null,
            string abreviatura = null,
            string nombreUnidad = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
            string filterText = null,
            string abreviatura = null,
            string nombreUnidad = null,
            CancellationToken cancellationToken = default);
    }
}