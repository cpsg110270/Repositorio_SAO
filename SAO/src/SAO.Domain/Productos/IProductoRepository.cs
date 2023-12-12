using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace SAO.Productos
{
    public interface IProductoRepository : IRepository<Producto, Guid>
    {
        Task<ProductoWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

        Task<List<ProductoWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            int? noProductoMin = null,
            int? noProductoMax = null,
            string nombreComercia = null,
            string uso = null,
            Guid? fabricanteId = null,
            int? asraeId = null,
            Guid? tipoProductoId = null,
            Guid? sustanciaElementalId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<Producto>> GetListAsync(
                    string filterText = null,
                    int? noProductoMin = null,
                    int? noProductoMax = null,
                    string nombreComercia = null,
                    string uso = null,
                    string sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string filterText = null,
            int? noProductoMin = null,
            int? noProductoMax = null,
            string nombreComercia = null,
            string uso = null,
            Guid? fabricanteId = null,
            int? asraeId = null,
            Guid? tipoProductoId = null,
            Guid? sustanciaElementalId = null,
            CancellationToken cancellationToken = default);
    }
}