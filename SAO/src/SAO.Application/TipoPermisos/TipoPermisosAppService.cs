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
using SAO.TipoPermisos;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using SAO.Shared;

namespace SAO.TipoPermisos
{

    [Authorize(SAOPermissions.TipoPermisos.Default)]
    public class TipoPermisosAppService : ApplicationService, ITipoPermisosAppService
    {
        private readonly IDistributedCache<TipoPermisoExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly ITipoPermisoRepository _tipoPermisoRepository;
        private readonly TipoPermisoManager _tipoPermisoManager;

        public TipoPermisosAppService(ITipoPermisoRepository tipoPermisoRepository, TipoPermisoManager tipoPermisoManager, IDistributedCache<TipoPermisoExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _tipoPermisoRepository = tipoPermisoRepository;
            _tipoPermisoManager = tipoPermisoManager;
        }

        public virtual async Task<PagedResultDto<TipoPermisoDto>> GetListAsync(GetTipoPermisosInput input)
        {
            var totalCount = await _tipoPermisoRepository.GetCountAsync(input.FilterText, input.Codigo, input.Desripcion);
            var items = await _tipoPermisoRepository.GetListAsync(input.FilterText, input.Codigo, input.Desripcion, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<TipoPermisoDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<TipoPermiso>, List<TipoPermisoDto>>(items)
            };
        }

        public virtual async Task<TipoPermisoDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<TipoPermiso, TipoPermisoDto>(await _tipoPermisoRepository.GetAsync(id));
        }

        [Authorize(SAOPermissions.TipoPermisos.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _tipoPermisoRepository.DeleteAsync(id);
        }

        [Authorize(SAOPermissions.TipoPermisos.Create)]
        public virtual async Task<TipoPermisoDto> CreateAsync(TipoPermisoCreateDto input)
        {

            var tipoPermiso = await _tipoPermisoManager.CreateAsync(
            input.Codigo, input.Desripcion
            );

            return ObjectMapper.Map<TipoPermiso, TipoPermisoDto>(tipoPermiso);
        }

        [Authorize(SAOPermissions.TipoPermisos.Edit)]
        public virtual async Task<TipoPermisoDto> UpdateAsync(Guid id, TipoPermisoUpdateDto input)
        {

            var tipoPermiso = await _tipoPermisoManager.UpdateAsync(
            id,
            input.Codigo, input.Desripcion
            );

            return ObjectMapper.Map<TipoPermiso, TipoPermisoDto>(tipoPermiso);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(TipoPermisoExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _tipoPermisoRepository.GetListAsync(input.FilterText, input.Codigo, input.Desripcion);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<TipoPermiso>, List<TipoPermisoExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "TipoPermisos.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new TipoPermisoExcelDownloadTokenCacheItem { Token = token },
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