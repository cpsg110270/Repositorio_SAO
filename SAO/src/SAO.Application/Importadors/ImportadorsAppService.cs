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
using SAO.Importadors;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using SAO.Shared;

namespace SAO.Importadors
{

    [Authorize(SAOPermissions.Importadors.Default)]
    public class ImportadorsAppService : ApplicationService, IImportadorsAppService
    {
        private readonly IDistributedCache<ImportadorExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly IImportadorRepository _importadorRepository;
        private readonly ImportadorManager _importadorManager;

        public ImportadorsAppService(IImportadorRepository importadorRepository, ImportadorManager importadorManager, IDistributedCache<ImportadorExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _importadorRepository = importadorRepository;
            _importadorManager = importadorManager;
        }

        public virtual async Task<PagedResultDto<ImportadorDto>> GetListAsync(GetImportadorsInput input)
        {
            var totalCount = await _importadorRepository.GetCountAsync(input.FilterText, input.NombreImportador);
            var items = await _importadorRepository.GetListAsync(input.FilterText, input.NombreImportador, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<ImportadorDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Importador>, List<ImportadorDto>>(items)
            };
        }

        public virtual async Task<ImportadorDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Importador, ImportadorDto>(await _importadorRepository.GetAsync(id));
        }

        [Authorize(SAOPermissions.Importadors.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _importadorRepository.DeleteAsync(id);
        }

        [Authorize(SAOPermissions.Importadors.Create)]
        public virtual async Task<ImportadorDto> CreateAsync(ImportadorCreateDto input)
        {

            var importador = await _importadorManager.CreateAsync(
            input.NombreImportador
            );

            return ObjectMapper.Map<Importador, ImportadorDto>(importador);
        }

        [Authorize(SAOPermissions.Importadors.Edit)]
        public virtual async Task<ImportadorDto> UpdateAsync(Guid id, ImportadorUpdateDto input)
        {

            var importador = await _importadorManager.UpdateAsync(
            id,
            input.NombreImportador
            );

            return ObjectMapper.Map<Importador, ImportadorDto>(importador);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(ImportadorExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _importadorRepository.GetListAsync(input.FilterText, input.NombreImportador);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<Importador>, List<ImportadorExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "Importadors.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new ImportadorExcelDownloadTokenCacheItem { Token = token },
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