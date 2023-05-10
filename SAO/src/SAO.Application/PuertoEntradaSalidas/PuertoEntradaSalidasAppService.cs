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
using SAO.PuertoEntradaSalidas;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using SAO.Shared;

namespace SAO.PuertoEntradaSalidas
{

    [Authorize(SAOPermissions.PuertoEntradaSalidas.Default)]
    public class PuertoEntradaSalidasAppService : ApplicationService, IPuertoEntradaSalidasAppService
    {
        private readonly IDistributedCache<PuertoEntradaSalidaExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly IPuertoEntradaSalidaRepository _puertoEntradaSalidaRepository;
        private readonly PuertoEntradaSalidaManager _puertoEntradaSalidaManager;

        public PuertoEntradaSalidasAppService(IPuertoEntradaSalidaRepository puertoEntradaSalidaRepository, PuertoEntradaSalidaManager puertoEntradaSalidaManager, IDistributedCache<PuertoEntradaSalidaExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _puertoEntradaSalidaRepository = puertoEntradaSalidaRepository;
            _puertoEntradaSalidaManager = puertoEntradaSalidaManager;
        }

        public virtual async Task<PagedResultDto<PuertoEntradaSalidaDto>> GetListAsync(GetPuertoEntradaSalidasInput input)
        {
            var totalCount = await _puertoEntradaSalidaRepository.GetCountAsync(input.FilterText, input.NombrePuerto);
            var items = await _puertoEntradaSalidaRepository.GetListAsync(input.FilterText, input.NombrePuerto, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<PuertoEntradaSalidaDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<PuertoEntradaSalida>, List<PuertoEntradaSalidaDto>>(items)
            };
        }

        public virtual async Task<PuertoEntradaSalidaDto> GetAsync(int id)
        {
            return ObjectMapper.Map<PuertoEntradaSalida, PuertoEntradaSalidaDto>(await _puertoEntradaSalidaRepository.GetAsync(id));
        }

        [Authorize(SAOPermissions.PuertoEntradaSalidas.Delete)]
        public virtual async Task DeleteAsync(int id)
        {
            await _puertoEntradaSalidaRepository.DeleteAsync(id);
        }

        [Authorize(SAOPermissions.PuertoEntradaSalidas.Create)]
        public virtual async Task<PuertoEntradaSalidaDto> CreateAsync(PuertoEntradaSalidaCreateDto input)
        {

            var puertoEntradaSalida = await _puertoEntradaSalidaManager.CreateAsync(
            input.NombrePuerto
            );

            return ObjectMapper.Map<PuertoEntradaSalida, PuertoEntradaSalidaDto>(puertoEntradaSalida);
        }

        [Authorize(SAOPermissions.PuertoEntradaSalidas.Edit)]
        public virtual async Task<PuertoEntradaSalidaDto> UpdateAsync(int id, PuertoEntradaSalidaUpdateDto input)
        {

            var puertoEntradaSalida = await _puertoEntradaSalidaManager.UpdateAsync(
            id,
            input.NombrePuerto
            );

            return ObjectMapper.Map<PuertoEntradaSalida, PuertoEntradaSalidaDto>(puertoEntradaSalida);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(PuertoEntradaSalidaExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _puertoEntradaSalidaRepository.GetListAsync(input.FilterText, input.NombrePuerto);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<PuertoEntradaSalida>, List<PuertoEntradaSalidaExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "PuertoEntradaSalidas.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new PuertoEntradaSalidaExcelDownloadTokenCacheItem { Token = token },
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