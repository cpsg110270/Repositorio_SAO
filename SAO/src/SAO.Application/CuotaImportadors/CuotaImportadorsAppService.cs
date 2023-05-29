using SAO.Shared;
using SAO.Importadors;
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
using SAO.CuotaImportadors;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using SAO.Shared;

namespace SAO.CuotaImportadors
{

    [Authorize(SAOPermissions.CuotaImportadors.Default)]
    public class CuotaImportadorsAppService : ApplicationService, ICuotaImportadorsAppService
    {
        private readonly IDistributedCache<CuotaImportadorExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly ICuotaImportadorRepository _cuotaImportadorRepository;
        private readonly CuotaImportadorManager _cuotaImportadorManager;
        private readonly IRepository<Importador, Guid> _importadorRepository;

        public CuotaImportadorsAppService(ICuotaImportadorRepository cuotaImportadorRepository, CuotaImportadorManager cuotaImportadorManager, IDistributedCache<CuotaImportadorExcelDownloadTokenCacheItem, string> excelDownloadTokenCache, IRepository<Importador, Guid> importadorRepository)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _cuotaImportadorRepository = cuotaImportadorRepository;
            _cuotaImportadorManager = cuotaImportadorManager; _importadorRepository = importadorRepository;
        }

        public virtual async Task<PagedResultDto<CuotaImportadorWithNavigationPropertiesDto>> GetListAsync(GetCuotaImportadorsInput input)
        {
            var totalCount = await _cuotaImportadorRepository.GetCountAsync(input.FilterText, input.AñoMin, input.AñoMax, input.CuotaMin, input.CuotaMax, input.ImportadorId);
            var items = await _cuotaImportadorRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.AñoMin, input.AñoMax, input.CuotaMin, input.CuotaMax, input.ImportadorId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<CuotaImportadorWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<CuotaImportadorWithNavigationProperties>, List<CuotaImportadorWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<CuotaImportadorWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<CuotaImportadorWithNavigationProperties, CuotaImportadorWithNavigationPropertiesDto>
                (await _cuotaImportadorRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<CuotaImportadorDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<CuotaImportador, CuotaImportadorDto>(await _cuotaImportadorRepository.GetAsync(id));
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetImportadorLookupAsync(LookupRequestDto input)
        {
            var query = (await _importadorRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.NombreImportador != null &&
                         x.NombreImportador.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Importador>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Importador>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        [Authorize(SAOPermissions.CuotaImportadors.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _cuotaImportadorRepository.DeleteAsync(id);
        }

        [Authorize(SAOPermissions.CuotaImportadors.Create)]
        public virtual async Task<CuotaImportadorDto> CreateAsync(CuotaImportadorCreateDto input)
        {
            if (input.ImportadorId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Importador"]]);
            }

            var cuotaImportador = await _cuotaImportadorManager.CreateAsync(
            input.ImportadorId, input.Año, input.Cuota
            );

            return ObjectMapper.Map<CuotaImportador, CuotaImportadorDto>(cuotaImportador);
        }

        [Authorize(SAOPermissions.CuotaImportadors.Edit)]
        public virtual async Task<CuotaImportadorDto> UpdateAsync(Guid id, CuotaImportadorUpdateDto input)
        {
            if (input.ImportadorId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Importador"]]);
            }

            var cuotaImportador = await _cuotaImportadorManager.UpdateAsync(
            id,
            input.ImportadorId, input.Año, input.Cuota
            );

            return ObjectMapper.Map<CuotaImportador, CuotaImportadorDto>(cuotaImportador);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(CuotaImportadorExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var cuotaImportadors = await _cuotaImportadorRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.AñoMin, input.AñoMax, input.CuotaMin, input.CuotaMax);
            var items = cuotaImportadors.Select(item => new
            {
                Año = item.CuotaImportador.Año,
                Cuota = item.CuotaImportador.Cuota,

                Importador = item.Importador?.NombreImportador,

            });

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(items);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "CuotaImportadors.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new CuotaImportadorExcelDownloadTokenCacheItem { Token = token },
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