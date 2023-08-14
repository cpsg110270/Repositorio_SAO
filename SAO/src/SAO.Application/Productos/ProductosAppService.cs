using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using MiniExcelLibs;
using SAO.Asraes;
using SAO.Fabricantes;
using SAO.Permissions;
using SAO.Shared;
using SAO.SustanciaElementals;
using SAO.TipoProductos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Volo.Abp.Content;
using Volo.Abp.Domain.Repositories;

namespace SAO.Productos
{

    [Authorize(SAOPermissions.Productos.Default)]
    public class ProductosAppService : ApplicationService, IProductosAppService
    {
        private readonly IDistributedCache<ProductoExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly IProductoRepository _productoRepository;
        private readonly ProductoManager _productoManager;
        private readonly IRepository<Fabricante, Guid> _fabricanteRepository;
        private readonly IRepository<Asrae, int> _asraeRepository;
        private readonly IRepository<TipoProducto, Guid> _tipoProductoRepository;
        private readonly IRepository<SustanciaElemental, Guid> _sustanciaElementalRepository;

        public ProductosAppService(IProductoRepository productoRepository, ProductoManager productoManager, IDistributedCache<ProductoExcelDownloadTokenCacheItem, string> excelDownloadTokenCache, IRepository<Fabricante, Guid> fabricanteRepository, IRepository<Asrae, int> asraeRepository, IRepository<TipoProducto, Guid> tipoProductoRepository, IRepository<SustanciaElemental, Guid> sustanciaElementalRepository)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _productoRepository = productoRepository;
            _productoManager = productoManager; _fabricanteRepository = fabricanteRepository;
            _asraeRepository = asraeRepository;
            _tipoProductoRepository = tipoProductoRepository;
            _sustanciaElementalRepository = sustanciaElementalRepository;
        }

        public virtual async Task<PagedResultDto<ProductoWithNavigationPropertiesDto>> GetListAsync(GetProductosInput input)
        {
            var totalCount = await _productoRepository.GetCountAsync(input.FilterText, input.NombreComercia, input.Uso, input.FabricanteId, input.AsraeId, input.TipoProductoId, input.SustanciaElementalId);
            var items = await _productoRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.NombreComercia, input.Uso, input.FabricanteId, input.AsraeId, input.TipoProductoId, input.SustanciaElementalId, input.Sorting, input.MaxResultCount, input.SkipCount);

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
                         (x.Codigo_ASHRAE.Contains(input.Filter) || x.Descripcion.Contains(input.Filter)));



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
                         (x.CodCas.Contains(input.Filter) || x.DesSustancia.Contains(input.Filter)));

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
            input.SustanciaElementalIds, input.FabricanteId, input.AsraeId, input.TipoProductoId, input.NombreComercia, input.Uso
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
            input.SustanciaElementalIds, input.FabricanteId, input.AsraeId, input.TipoProductoId, input.NombreComercia, input.Uso
            );

            return ObjectMapper.Map<Producto, ProductoDto>(producto);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(ProductoExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var productos = await _productoRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.NombreComercia, input.Uso);
            var items = productos.Select(item => new
            {
                NombreComercia = item.Producto.NombreComercia,
                Uso = item.Producto.Uso,

                Fabricante = item.Fabricante?.NombreFabricante,
                Asrae = item.Asrae?.Codigo_ASHRAE,
                TipoProducto = item.TipoProducto?.DesProducto,

            });

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(items);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "Productos.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new ProductoExcelDownloadTokenCacheItem { Token = token },
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
                });

            return new DownloadTokenResultDto
            {
                Token = token
            };
        }
    }
}