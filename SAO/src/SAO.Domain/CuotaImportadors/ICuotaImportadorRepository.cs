using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace SAO.CuotaImportadors
{
    public interface ICuotaImportadorRepository : IRepository<CuotaImportador, Guid>
    {
        Task<CuotaImportadorWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

        Task<List<CuotaImportadorWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            int? añoMin = null,
            int? añoMax = null,
            decimal? cuotaMin = null,
            decimal? cuotaMax = null,
            Guid? importadorId = null,
            int? asraeId = null,
            Guid? tipoProductoId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<CuotaImportador>> GetListAsync(
                    string filterText = null,
                    int? añoMin = null,
                    int? añoMax = null,
                    decimal? cuotaMin = null,
                    decimal? cuotaMax = null,
                    string sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string filterText = null,
            int? añoMin = null,
            int? añoMax = null,
            decimal? cuotaMin = null,
            decimal? cuotaMax = null,
            Guid? importadorId = null,
            int? asraeId = null,
            Guid? tipoProductoId = null,
            CancellationToken cancellationToken = default);
    }
}