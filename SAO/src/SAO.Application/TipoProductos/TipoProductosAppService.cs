using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using MiniExcelLibs;
using SAO.Permissions;
using SAO.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Volo.Abp.Content;

namespace SAO.TipoProductos
{

    [Authorize(SAOPermissions.TipoProductos.Default)]
    public class TipoProductosAppService : ApplicationService, ITipoProductosAppService
    {
        private readonly IDistributedCache<TipoProductoExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly ITipoProductoRepository _tipoProductoRepository;
        private readonly TipoProductoManager _tipoProductoManager;

        public TipoProductosAppService(ITipoProductoRepository tipoProductoRepository, TipoProductoManager tipoProductoManager, IDistributedCache<TipoProductoExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _tipoProductoRepository = tipoProductoRepository;
            _tipoProductoManager = tipoProductoManager;
        }

        public virtual async Task<PagedResultDto<TipoProductoDto>> GetListAsync(GetTipoProductosInput input)
        {
            var totalCount = await _tipoProductoRepository.GetCountAsync(input.FilterText, input.DesProducto);
            var items = await _tipoProductoRepository.GetListAsync(input.FilterText, input.DesProducto, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<TipoProductoDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<TipoProducto>, List<TipoProductoDto>>(items)
            };
        }

        public virtual async Task<TipoProductoDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<TipoProducto, TipoProductoDto>(await _tipoProductoRepository.GetAsync(id));
        }

        [Authorize(SAOPermissions.TipoProductos.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _tipoProductoRepository.DeleteAsync(id);
        }

        [Authorize(SAOPermissions.TipoProductos.Create)]
        public virtual async Task<TipoProductoDto> CreateAsync(TipoProductoCreateDto input)
        {

            var tipoProducto = await _tipoProductoManager.CreateAsync(
            input.DesProducto
            );

            return ObjectMapper.Map<TipoProducto, TipoProductoDto>(tipoProducto);
        }

        [Authorize(SAOPermissions.TipoProductos.Edit)]
        public virtual async Task<TipoProductoDto> UpdateAsync(Guid id, TipoProductoUpdateDto input)
        {

            var tipoProducto = await _tipoProductoManager.UpdateAsync(
            id,
            input.DesProducto
            );

            return ObjectMapper.Map<TipoProducto, TipoProductoDto>(tipoProducto);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(TipoProductoExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _tipoProductoRepository.GetListAsync(input.FilterText, input.DesProducto);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<TipoProducto>, List<TipoProductoExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "TipoProductos.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new TipoProductoExcelDownloadTokenCacheItem { Token = token },
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