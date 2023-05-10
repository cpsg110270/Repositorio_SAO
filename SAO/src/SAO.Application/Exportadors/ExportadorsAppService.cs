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
using SAO.Exportadors;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using SAO.Shared;

namespace SAO.Exportadors
{

    [Authorize(SAOPermissions.Exportadors.Default)]
    public class ExportadorsAppService : ApplicationService, IExportadorsAppService
    {
        private readonly IDistributedCache<ExportadorExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly IExportadorRepository _exportadorRepository;
        private readonly ExportadorManager _exportadorManager;

        public ExportadorsAppService(IExportadorRepository exportadorRepository, ExportadorManager exportadorManager, IDistributedCache<ExportadorExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _exportadorRepository = exportadorRepository;
            _exportadorManager = exportadorManager;
        }

        public virtual async Task<PagedResultDto<ExportadorDto>> GetListAsync(GetExportadorsInput input)
        {
            var totalCount = await _exportadorRepository.GetCountAsync(input.FilterText, input.NombreExportador);
            var items = await _exportadorRepository.GetListAsync(input.FilterText, input.NombreExportador, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<ExportadorDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Exportador>, List<ExportadorDto>>(items)
            };
        }

        public virtual async Task<ExportadorDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Exportador, ExportadorDto>(await _exportadorRepository.GetAsync(id));
        }

        [Authorize(SAOPermissions.Exportadors.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _exportadorRepository.DeleteAsync(id);
        }

        [Authorize(SAOPermissions.Exportadors.Create)]
        public virtual async Task<ExportadorDto> CreateAsync(ExportadorCreateDto input)
        {

            var exportador = await _exportadorManager.CreateAsync(
            input.NombreExportador
            );

            return ObjectMapper.Map<Exportador, ExportadorDto>(exportador);
        }

        [Authorize(SAOPermissions.Exportadors.Edit)]
        public virtual async Task<ExportadorDto> UpdateAsync(Guid id, ExportadorUpdateDto input)
        {

            var exportador = await _exportadorManager.UpdateAsync(
            id,
            input.NombreExportador
            );

            return ObjectMapper.Map<Exportador, ExportadorDto>(exportador);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(ExportadorExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _exportadorRepository.GetListAsync(input.FilterText, input.NombreExportador);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<Exportador>, List<ExportadorExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "Exportadors.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new ExportadorExcelDownloadTokenCacheItem { Token = token },
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