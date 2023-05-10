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
using SAO.Asraes;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using SAO.Shared;

namespace SAO.Asraes
{

    [Authorize(SAOPermissions.Asraes.Default)]
    public class AsraesAppService : ApplicationService, IAsraesAppService
    {
        private readonly IDistributedCache<AsraeExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly IAsraeRepository _asraeRepository;
        private readonly AsraeManager _asraeManager;

        public AsraesAppService(IAsraeRepository asraeRepository, AsraeManager asraeManager, IDistributedCache<AsraeExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _asraeRepository = asraeRepository;
            _asraeManager = asraeManager;
        }

        public virtual async Task<PagedResultDto<AsraeDto>> GetListAsync(GetAsraesInput input)
        {
            var totalCount = await _asraeRepository.GetCountAsync(input.FilterText, input.Codigo_ASHRAE, input.Descripcion);
            var items = await _asraeRepository.GetListAsync(input.FilterText, input.Codigo_ASHRAE, input.Descripcion, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<AsraeDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Asrae>, List<AsraeDto>>(items)
            };
        }

        public virtual async Task<AsraeDto> GetAsync(int id)
        {
            return ObjectMapper.Map<Asrae, AsraeDto>(await _asraeRepository.GetAsync(id));
        }

        [Authorize(SAOPermissions.Asraes.Delete)]
        public virtual async Task DeleteAsync(int id)
        {
            await _asraeRepository.DeleteAsync(id);
        }

        [Authorize(SAOPermissions.Asraes.Create)]
        public virtual async Task<AsraeDto> CreateAsync(AsraeCreateDto input)
        {

            var asrae = await _asraeManager.CreateAsync(
            input.Codigo_ASHRAE, input.Descripcion
            );

            return ObjectMapper.Map<Asrae, AsraeDto>(asrae);
        }

        [Authorize(SAOPermissions.Asraes.Edit)]
        public virtual async Task<AsraeDto> UpdateAsync(int id, AsraeUpdateDto input)
        {

            var asrae = await _asraeManager.UpdateAsync(
            id,
            input.Codigo_ASHRAE, input.Descripcion
            );

            return ObjectMapper.Map<Asrae, AsraeDto>(asrae);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(AsraeExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _asraeRepository.GetListAsync(input.FilterText, input.Codigo_ASHRAE, input.Descripcion);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<Asrae>, List<AsraeExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "Asraes.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new AsraeExcelDownloadTokenCacheItem { Token = token },
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