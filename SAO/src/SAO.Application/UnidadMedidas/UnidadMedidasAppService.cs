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

namespace SAO.UnidadMedidas
{

    [Authorize(SAOPermissions.UnidadMedidas.Default)]
    public class UnidadMedidasAppService : ApplicationService, IUnidadMedidasAppService
    {
        private readonly IDistributedCache<UnidadMedidaExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly IUnidadMedidaRepository _unidadMedidaRepository;
        private readonly UnidadMedidaManager _unidadMedidaManager;

        public UnidadMedidasAppService(IUnidadMedidaRepository unidadMedidaRepository, UnidadMedidaManager unidadMedidaManager, IDistributedCache<UnidadMedidaExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _unidadMedidaRepository = unidadMedidaRepository;
            _unidadMedidaManager = unidadMedidaManager;
        }

        public virtual async Task<PagedResultDto<UnidadMedidaDto>> GetListAsync(GetUnidadMedidasInput input)
        {
            var totalCount = await _unidadMedidaRepository.GetCountAsync(input.FilterText, input.Abreviatura, input.NombreUnidad);
            var items = await _unidadMedidaRepository.GetListAsync(input.FilterText, input.Abreviatura, input.NombreUnidad, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<UnidadMedidaDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<UnidadMedida>, List<UnidadMedidaDto>>(items)
            };
        }

        public virtual async Task<UnidadMedidaDto> GetAsync(int id)
        {
            return ObjectMapper.Map<UnidadMedida, UnidadMedidaDto>(await _unidadMedidaRepository.GetAsync(id));
        }

        [Authorize(SAOPermissions.UnidadMedidas.Delete)]
        public virtual async Task DeleteAsync(int id)
        {
            await _unidadMedidaRepository.DeleteAsync(id);
        }

        [Authorize(SAOPermissions.UnidadMedidas.Create)]
        public virtual async Task<UnidadMedidaDto> CreateAsync(UnidadMedidaCreateDto input)
        {

            var unidadMedida = await _unidadMedidaManager.CreateAsync(
            input.Abreviatura, input.NombreUnidad
            );

            return ObjectMapper.Map<UnidadMedida, UnidadMedidaDto>(unidadMedida);
        }

        [Authorize(SAOPermissions.UnidadMedidas.Edit)]
        public virtual async Task<UnidadMedidaDto> UpdateAsync(int id, UnidadMedidaUpdateDto input)
        {

            var unidadMedida = await _unidadMedidaManager.UpdateAsync(
            id,
            input.Abreviatura, input.NombreUnidad
            );

            return ObjectMapper.Map<UnidadMedida, UnidadMedidaDto>(unidadMedida);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(UnidadMedidaExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _unidadMedidaRepository.GetListAsync(input.FilterText, input.Abreviatura, input.NombreUnidad);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<UnidadMedida>, List<UnidadMedidaExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "UnidadMedidas.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new UnidadMedidaExcelDownloadTokenCacheItem { Token = token },
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