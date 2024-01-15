using SAO.Shared;
using SAO.Asraes;
using SAO.TipoProductos;
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
using SAO.TotalImportacioness;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using SAO.Shared;

namespace SAO.TotalImportacioness
{

    [Authorize(SAOPermissions.TotalImportacioness.Default)]
    public class TotalImportacionessAppService : ApplicationService, ITotalImportacionessAppService
    {
        private readonly IDistributedCache<TotalImportacionesExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly ITotalImportacionesRepository _totalImportacionesRepository;
        private readonly TotalImportacionesManager _totalImportacionesManager;
        private readonly IRepository<Importador, Guid> _importadorRepository;
        private readonly IRepository<TipoProducto, Guid> _tipoProductoRepository;
        private readonly IRepository<Asrae, int> _asraeRepository;

        public TotalImportacionessAppService(ITotalImportacionesRepository totalImportacionesRepository, TotalImportacionesManager totalImportacionesManager, IDistributedCache<TotalImportacionesExcelDownloadTokenCacheItem, string> excelDownloadTokenCache, IRepository<Importador, Guid> importadorRepository, IRepository<TipoProducto, Guid> tipoProductoRepository, IRepository<Asrae, int> asraeRepository)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _totalImportacionesRepository = totalImportacionesRepository;
            _totalImportacionesManager = totalImportacionesManager; _importadorRepository = importadorRepository;
            _tipoProductoRepository = tipoProductoRepository;
            _asraeRepository = asraeRepository;
        }

        public virtual async Task<PagedResultDto<TotalImportacionesWithNavigationPropertiesDto>> GetListAsync(GetTotalImportacionessInput input)
        {
            var totalCount = await _totalImportacionesRepository.GetCountAsync(input.FilterText, input.AnioMin, input.AnioMax, input.CuotaAsignadaMin, input.CuotaAsignadaMax, input.CuotaConsumidaMin, input.CuotaConsumidaMax, input.ImportadorId, input.TipoProductoId, input.AsraeId);
            var items = await _totalImportacionesRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.AnioMin, input.AnioMax, input.CuotaAsignadaMin, input.CuotaAsignadaMax, input.CuotaConsumidaMin, input.CuotaConsumidaMax, input.ImportadorId, input.TipoProductoId, input.AsraeId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<TotalImportacionesWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<TotalImportacionesWithNavigationProperties>, List<TotalImportacionesWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<TotalImportacionesWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<TotalImportacionesWithNavigationProperties, TotalImportacionesWithNavigationPropertiesDto>
                (await _totalImportacionesRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<TotalImportacionesDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<TotalImportaciones, TotalImportacionesDto>(await _totalImportacionesRepository.GetAsync(id));
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

        public virtual async Task<PagedResultDto<LookupDto<int>>> GetAsraeLookupAsync(LookupRequestDto input)
        {
            var query = (await _asraeRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Codigo_ASHRAE != null &&
                         x.Codigo_ASHRAE.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Asrae>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<int>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Asrae>, List<LookupDto<int>>>(lookupData)
            };
        }

        [Authorize(SAOPermissions.TotalImportacioness.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _totalImportacionesRepository.DeleteAsync(id);
        }

        [Authorize(SAOPermissions.TotalImportacioness.Create)]
        public virtual async Task<TotalImportacionesDto> CreateAsync(TotalImportacionesCreateDto input)
        {
            if (input.ImportadorId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Importador"]]);
            }
            if (input.TipoProductoId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["TipoProducto"]]);
            }
            if (input.AsraeId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Asrae"]]);
            }

            var totalImportaciones = await _totalImportacionesManager.CreateAsync(
            input.ImportadorId, input.TipoProductoId, input.AsraeId, input.Anio, input.CuotaAsignada, input.CuotaConsumida
            );

            return ObjectMapper.Map<TotalImportaciones, TotalImportacionesDto>(totalImportaciones);
        }

        [Authorize(SAOPermissions.TotalImportacioness.Edit)]
        public virtual async Task<TotalImportacionesDto> UpdateAsync(Guid id, TotalImportacionesUpdateDto input)
        {
            if (input.ImportadorId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Importador"]]);
            }
            if (input.TipoProductoId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["TipoProducto"]]);
            }
            if (input.AsraeId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Asrae"]]);
            }

            var totalImportaciones = await _totalImportacionesManager.UpdateAsync(
            id,
            input.ImportadorId, input.TipoProductoId, input.AsraeId, input.Anio, input.CuotaAsignada, input.CuotaConsumida
            );

            return ObjectMapper.Map<TotalImportaciones, TotalImportacionesDto>(totalImportaciones);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(TotalImportacionesExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var totalImportacioness = await _totalImportacionesRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.AnioMin, input.AnioMax, input.CuotaAsignadaMin, input.CuotaAsignadaMax, input.CuotaConsumidaMin, input.CuotaConsumidaMax);
            var items = totalImportacioness.Select(item => new
            {
                Anio = item.TotalImportaciones.Anio,
                CuotaAsignada = item.TotalImportaciones.CuotaAsignada,
                CuotaConsumida = item.TotalImportaciones.CuotaConsumida,

                Importador = item.Importador?.NombreImportador,
                TipoProducto = item.TipoProducto?.DesProducto,
                Asrae = item.Asrae?.Codigo_ASHRAE,

            });

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(items);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "TotalImportacioness.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new TotalImportacionesExcelDownloadTokenCacheItem { Token = token },
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