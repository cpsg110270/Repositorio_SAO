using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace SAO.TotalImportacioness
{
    public interface ITotalImportacionesRepository : IRepository<TotalImportaciones, Guid>
    {
        Task<TotalImportacionesWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

        Task<List<TotalImportacionesWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            int? anioMin = null,
            int? anioMax = null,
            double? cuotaAsignadaMin = null,
            double? cuotaAsignadaMax = null,
            double? cuotaConsumidaMin = null,
            double? cuotaConsumidaMax = null,
            Guid? importadorId = null,
            Guid? tipoProductoId = null,
            int? asraeId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<TotalImportaciones>> GetListAsync(
                    string filterText = null,
                    int? anioMin = null,
                    int? anioMax = null,
                    double? cuotaAsignadaMin = null,
                    double? cuotaAsignadaMax = null,
                    double? cuotaConsumidaMin = null,
                    double? cuotaConsumidaMax = null,
                    string sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string filterText = null,
            int? anioMin = null,
            int? anioMax = null,
            double? cuotaAsignadaMin = null,
            double? cuotaAsignadaMax = null,
            double? cuotaConsumidaMin = null,
            double? cuotaConsumidaMax = null,
            Guid? importadorId = null,
            Guid? tipoProductoId = null,
            int? asraeId = null,
            CancellationToken cancellationToken = default);
    }
}