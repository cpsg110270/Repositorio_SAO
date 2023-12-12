using SAO.Shared;
using SAO.SustanciaElementals;
using SAO.TipoProductos;
using SAO.Asraes;
using SAO.Fabricantes;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using SAO.Permissions;
using SAO.Productos;

namespace SAO.Productos
{

    [Authorize(SAOPermissions.Productos.Default)]
    public class ProductosAppService : ApplicationService, IProductosAppService
    {

        private readonly IProductoRepository _productoRepository;
        private readonly ProductoManager _productoManager;
        private readonly IRepository<Fabricante, Guid> _fabricanteRepository;
        private readonly IRepository<Asrae, int> _asraeRepository;
        private readonly IRepository<TipoProducto, Guid> _tipoProductoRepository;
        private readonly IRepository<SustanciaElemental, Guid> _sustanciaElementalRepository;

        public ProductosAppService(IProductoRepository productoRepository, ProductoManager productoManager, IRepository<Fabricante, Guid> fabricanteRepository, IRepository<Asrae, int> asraeRepository, IRepository<TipoProducto, Guid> tipoProductoRepository, IRepository<SustanciaElemental, Guid> sustanciaElementalRepository)
        {

            _productoRepository = productoRepository;
            _productoManager = productoManager; _fabricanteRepository = fabricanteRepository;
            _asraeRepository = asraeRepository;
            _tipoProductoRepository = tipoProductoRepository;
            _sustanciaElementalRepository = sustanciaElementalRepository;
        }

        public virtual async Task<PagedResultDto<ProductoWithNavigationPropertiesDto>> GetListAsync(GetProductosInput input)
        {
            var totalCount = await _productoRepository.GetCountAsync(input.FilterText, input.NoProductoMin, input.NoProductoMax, input.NombreComercia, input.Uso, input.FabricanteId, input.AsraeId, input.TipoProductoId, input.SustanciaElementalId);
            var items = await _productoRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.NoProductoMin, input.NoProductoMax, input.NombreComercia, input.Uso, input.FabricanteId, input.AsraeId, input.TipoProductoId, input.SustanciaElementalId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<ProductoWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<ProductoWithNavigationProperties>, List<ProductoWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<ProductoWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<ProductoWithNavigationProperties, ProductoWithNavigationPropertiesDto>
                (await _productoRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<ProductoDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Producto, ProductoDto>(await _productoRepository.GetAsync(id));
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetFabricanteLookupAsync(LookupRequestDto input)
        {
            var query = (await _fabricanteRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.NombreFabricante != null &&
                         x.NombreFabricante.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Fabricante>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Fabricante>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        public virtual async Task<PagedResultDto<LookupDto<int>>> GetAsraeLookupAsync(LookupRequestDto input)
        {
            var query = (await _asraeRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Codigo_ASHRAE != null &&
                         x.Codigo_ASHRAE.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Asrae>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<int>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Asrae>, List<LookupDto<int>>>(lookupData)
            };
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetTipoProductoLookupAsync(LookupRequestDto input)
        {
            var query = (await _tipoProductoRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.DesProducto != null &&
                         x.DesProducto.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<TipoProducto>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<TipoProducto>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetSustanciaElementalLookupAsync(LookupRequestDto input)
        {
            var query = (await _sustanciaElementalRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.CodCas != null &&
                         x.CodCas.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<SustanciaElemental>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<SustanciaElemental>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        [Authorize(SAOPermissions.Productos.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _productoRepository.DeleteAsync(id);
        }

        [Authorize(SAOPermissions.Productos.Create)]
        public virtual async Task<ProductoDto> CreateAsync(ProductoCreateDto input)
        {
            if (input.FabricanteId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Fabricante"]]);
            }
            if (input.AsraeId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Asrae"]]);
            }

            var producto = await _productoManager.CreateAsync(
            input.SustanciaElementalIds, input.FabricanteId, input.AsraeId, input.TipoProductoId, input.NoProducto, input.NombreComercia, input.Uso
            );

            return ObjectMapper.Map<Producto, ProductoDto>(producto);
        }

        [Authorize(SAOPermissions.Productos.Edit)]
        public virtual async Task<ProductoDto> UpdateAsync(Guid id, ProductoUpdateDto input)
        {
            if (input.FabricanteId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Fabricante"]]);
            }
            if (input.AsraeId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Asrae"]]);
            }

            var producto = await _productoManager.UpdateAsync(
            id,
            input.SustanciaElementalIds, input.FabricanteId, input.AsraeId, input.TipoProductoId, input.NoProducto, input.NombreComercia, input.Uso
            );

            return ObjectMapper.Map<Producto, ProductoDto>(producto);
        }
    }
}