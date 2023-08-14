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

namespace SAO.TipoEnvases
{

    [Authorize(SAOPermissions.TipoEnvases.Default)]
    public class TipoEnvasesAppService : ApplicationService, ITipoEnvasesAppService
    {
        private readonly IDistributedCache<TipoEnvaseExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly ITipoEnvaseRepository _tipoEnvaseRepository;
        private readonly TipoEnvaseManager _tipoEnvaseManager;

        public TipoEnvasesAppService(ITipoEnvaseRepository tipoEnvaseRepository, TipoEnvaseManager tipoEnvaseManager, IDistributedCache<TipoEnvaseExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _tipoEnvaseRepository = tipoEnvaseRepository;
            _tipoEnvaseManager = tipoEnvaseManager;
        }

        public virtual async Task<PagedResultDto<TipoEnvaseDto>> GetListAsync(GetTipoEnvasesInput input)
        {
            var totalCount = await _tipoEnvaseRepository.GetCountAsync(input.FilterText, input.DesEnvase);
            var items = await _tipoEnvaseRepository.GetListAsync(input.FilterText, input.DesEnvase, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<TipoEnvaseDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<TipoEnvase>, List<TipoEnvaseDto>>(items)
            };
        }

        public virtual async Task<TipoEnvaseDto> GetAsync(int id)
        {
            return ObjectMapper.Map<TipoEnvase, TipoEnvaseDto>(await _tipoEnvaseRepository.GetAsync(id));
        }

        [Authorize(SAOPermissions.TipoEnvases.Delete)]
        public virtual async Task DeleteAsync(int id)
        {
            await _tipoEnvaseRepository.DeleteAsync(id);
        }

        [Authorize(SAOPermissions.TipoEnvases.Create)]
        public virtual async Task<TipoEnvaseDto> CreateAsync(TipoEnvaseCreateDto input)
        {

            var tipoEnvase = await _tipoEnvaseManager.CreateAsync(
            input.DesEnvase
            );

            return ObjectMapper.Map<TipoEnvase, TipoEnvaseDto>(tipoEnvase);
        }

        [Authorize(SAOPermissions.TipoEnvases.Edit)]
        public virtual async Task<TipoEnvaseDto> UpdateAsync(int id, TipoEnvaseUpdateDto input)
        {

            var tipoEnvase = await _tipoEnvaseManager.UpdateAsync(
            id,
            input.DesEnvase
            );

            return ObjectMapper.Map<TipoEnvase, TipoEnvaseDto>(tipoEnvase);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(TipoEnvaseExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _tipoEnvaseRepository.GetListAsync(input.FilterText, input.DesEnvase);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<TipoEnvase>, List<TipoEnvaseExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "TipoEnvases.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new TipoEnvaseExcelDownloadTokenCacheItem { Token = token },
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