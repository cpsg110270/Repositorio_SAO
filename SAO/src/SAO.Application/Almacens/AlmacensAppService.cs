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
using SAO.Almacens;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using SAO.Shared;

namespace SAO.Almacens
{

    [Authorize(SAOPermissions.Almacens.Default)]
    public class AlmacensAppService : ApplicationService, IAlmacensAppService
    {
        private readonly IDistributedCache<AlmacenExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly IAlmacenRepository _almacenRepository;
        private readonly AlmacenManager _almacenManager;

        public AlmacensAppService(IAlmacenRepository almacenRepository, AlmacenManager almacenManager, IDistributedCache<AlmacenExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _almacenRepository = almacenRepository;
            _almacenManager = almacenManager;
        }

        public virtual async Task<PagedResultDto<AlmacenDto>> GetListAsync(GetAlmacensInput input)
        {
            var totalCount = await _almacenRepository.GetCountAsync(input.FilterText, input.NombreAlmacen, input.SiglaAlmacen);
            var items = await _almacenRepository.GetListAsync(input.FilterText, input.NombreAlmacen, input.SiglaAlmacen, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<AlmacenDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Almacen>, List<AlmacenDto>>(items)
            };
        }

        public virtual async Task<AlmacenDto> GetAsync(int id)
        {
            return ObjectMapper.Map<Almacen, AlmacenDto>(await _almacenRepository.GetAsync(id));
        }

        [Authorize(SAOPermissions.Almacens.Delete)]
        public virtual async Task DeleteAsync(int id)
        {
            await _almacenRepository.DeleteAsync(id);
        }

        [Authorize(SAOPermissions.Almacens.Create)]
        public virtual async Task<AlmacenDto> CreateAsync(AlmacenCreateDto input)
        {

            var almacen = await _almacenManager.CreateAsync(
            input.NombreAlmacen, input.SiglaAlmacen
            );

            return ObjectMapper.Map<Almacen, AlmacenDto>(almacen);
        }

        [Authorize(SAOPermissions.Almacens.Edit)]
        public virtual async Task<AlmacenDto> UpdateAsync(int id, AlmacenUpdateDto input)
        {

            var almacen = await _almacenManager.UpdateAsync(
            id,
            input.NombreAlmacen, input.SiglaAlmacen
            );

            return ObjectMapper.Map<Almacen, AlmacenDto>(almacen);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(AlmacenExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _almacenRepository.GetListAsync(input.FilterText, input.NombreAlmacen, input.SiglaAlmacen);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<Almacen>, List<AlmacenExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "Almacens.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new AlmacenExcelDownloadTokenCacheItem { Token = token },
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