using SAO.SustanciaElementals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace SAO.Productos
{
    public class ProductoManager : DomainService
    {
        private readonly IProductoRepository _productoRepository;
        private readonly IRepository<SustanciaElemental, Guid> _sustanciaElementalRepository;

        public ProductoManager(IProductoRepository productoRepository,
        IRepository<SustanciaElemental, Guid> sustanciaElementalRepository)
        {
            _productoRepository = productoRepository;
            _sustanciaElementalRepository = sustanciaElementalRepository;
        }

        public async Task<Producto> CreateAsync(
        List<Guid> sustanciaElementalIds,
        Guid fabricanteId, int asraeId, Guid? tipoProductoId, string nombreComercia, string uso)
        {
            Check.NotNull(fabricanteId, nameof(fabricanteId));
            Check.NotNull(asraeId, nameof(asraeId));
            Check.NotNullOrWhiteSpace(nombreComercia, nameof(nombreComercia));
            Check.Length(nombreComercia, nameof(nombreComercia), ProductoConsts.NombreComerciaMaxLength, ProductoConsts.NombreComerciaMinLength);
            Check.Length(uso, nameof(uso), ProductoConsts.UsoMaxLength);

            var producto = new Producto(
             GuidGenerator.Create(),
             fabricanteId, asraeId, tipoProductoId, nombreComercia, uso
             );

            await SetSustanciaElementalsAsync(producto, sustanciaElementalIds);

            return await _productoRepository.InsertAsync(producto);
        }

        public async Task<Producto> UpdateAsync(
            Guid id,
            List<Guid> sustanciaElementalIds,
        Guid fabricanteId, int asraeId, Guid? tipoProductoId, string nombreComercia, string uso
        )
        {
            Check.NotNull(fabricanteId, nameof(fabricanteId));
            Check.NotNull(asraeId, nameof(asraeId));
            Check.NotNullOrWhiteSpace(nombreComercia, nameof(nombreComercia));
            Check.Length(nombreComercia, nameof(nombreComercia), ProductoConsts.NombreComerciaMaxLength, ProductoConsts.NombreComerciaMinLength);
            Check.Length(uso, nameof(uso), ProductoConsts.UsoMaxLength);

            var queryable = await _productoRepository.WithDetailsAsync(x => x.SustanciaElementals);
            var query = queryable.Where(x => x.Id == id);

            var producto = await AsyncExecuter.FirstOrDefaultAsync(query);

            producto.FabricanteId = fabricanteId;
            producto.AsraeId = asraeId;
            producto.TipoProductoId = tipoProductoId;
            producto.NombreComercia = nombreComercia;
            producto.Uso = uso;

            await SetSustanciaElementalsAsync(producto, sustanciaElementalIds);

            return await _productoRepository.UpdateAsync(producto);
        }

        private async Task SetSustanciaElementalsAsync(Producto producto, List<Guid> sustanciaElementalIds)
        {
            if (sustanciaElementalIds == null || !sustanciaElementalIds.Any())
            {
                producto.RemoveAllSustanciaElementals();
                return;
            }

            var query = (await _sustanciaElementalRepository.GetQueryableAsync())
                .Where(x => sustanciaElementalIds.Contains(x.Id))
                .Select(x => x.Id);

            var sustanciaElementalIdsInDb = await AsyncExecuter.ToListAsync(query);
            if (!sustanciaElementalIdsInDb.Any())
            {
                return;
            }

            producto.RemoveAllSustanciaElementalsExceptGivenIds(sustanciaElementalIdsInDb);

            foreach (var sustanciaElementalId in sustanciaElementalIdsInDb)
            {
                producto.AddSustanciaElemental(sustanciaElementalId);
            }
        }

    }
}