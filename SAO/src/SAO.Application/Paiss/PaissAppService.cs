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
using SAO.Paiss;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using SAO.Shared;

namespace SAO.Paiss
{

    [Authorize(SAOPermissions.Paiss.Default)]
    public class PaissAppService : ApplicationService, IPaissAppService
    {
        private readonly IDistributedCache<PaisExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly IPaisRepository _paisRepository;
        private readonly PaisManager _paisManager;

        public PaissAppService(IPaisRepository paisRepository, PaisManager paisManager, IDistributedCache<PaisExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _paisRepository = paisRepository;
            _paisManager = paisManager;
        }

        public virtual async Task<PagedResultDto<PaisDto>> GetListAsync(GetPaissInput input)
        {
            var totalCount = await _paisRepository.GetCountAsync(input.FilterText, input.NombrePais);
            var items = await _paisRepository.GetListAsync(input.FilterText, input.NombrePais, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<PaisDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Pais>, List<PaisDto>>(items)
            };
        }

        public virtual async Task<PaisDto> GetAsync(int id)
        {
            return ObjectMapper.Map<Pais, PaisDto>(await _paisRepository.GetAsync(id));
        }

        [Authorize(SAOPermissions.Paiss.Delete)]
        public virtual async Task DeleteAsync(int id)
        {
            await _paisRepository.DeleteAsync(id);
        }

        [Authorize(SAOPermissions.Paiss.Create)]
        public virtual async Task<PaisDto> CreateAsync(PaisCreateDto input)
        {

            var pais = await _paisManager.CreateAsync(
            input.NombrePais
            );

            return ObjectMapper.Map<Pais, PaisDto>(pais);
        }

        [Authorize(SAOPermissions.Paiss.Edit)]
        public virtual async Task<PaisDto> UpdateAsync(int id, PaisUpdateDto input)
        {

            var pais = await _paisManager.UpdateAsync(
            id,
            input.NombrePais
            );

            return ObjectMapper.Map<Pais, PaisDto>(pais);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(PaisExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _paisRepository.GetListAsync(input.FilterText, input.NombrePais);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<Pais>, List<PaisExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "Paiss.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new PaisExcelDownloadTokenCacheItem { Token = token },
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