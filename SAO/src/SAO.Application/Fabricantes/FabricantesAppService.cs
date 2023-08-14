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

namespace SAO.Fabricantes
{

    [Authorize(SAOPermissions.Fabricantes.Default)]
    public class FabricantesAppService : ApplicationService, IFabricantesAppService
    {
        private readonly IDistributedCache<FabricanteExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly IFabricanteRepository _fabricanteRepository;
        private readonly FabricanteManager _fabricanteManager;

        public FabricantesAppService(IFabricanteRepository fabricanteRepository, FabricanteManager fabricanteManager, IDistributedCache<FabricanteExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _fabricanteRepository = fabricanteRepository;
            _fabricanteManager = fabricanteManager;
        }

        public virtual async Task<PagedResultDto<FabricanteDto>> GetListAsync(GetFabricantesInput input)
        {
            var totalCount = await _fabricanteRepository.GetCountAsync(input.FilterText, input.NombreFabricante);
            var items = await _fabricanteRepository.GetListAsync(input.FilterText, input.NombreFabricante, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<FabricanteDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Fabricante>, List<FabricanteDto>>(items)
            };
        }

        public virtual async Task<FabricanteDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Fabricante, FabricanteDto>(await _fabricanteRepository.GetAsync(id));
        }

        [Authorize(SAOPermissions.Fabricantes.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _fabricanteRepository.DeleteAsync(id);
        }

        [Authorize(SAOPermissions.Fabricantes.Create)]
        public virtual async Task<FabricanteDto> CreateAsync(FabricanteCreateDto input)
        {

            var fabricante = await _fabricanteManager.CreateAsync(
            input.NombreFabricante
            );

            return ObjectMapper.Map<Fabricante, FabricanteDto>(fabricante);
        }

        [Authorize(SAOPermissions.Fabricantes.Edit)]
        public virtual async Task<FabricanteDto> UpdateAsync(Guid id, FabricanteUpdateDto input)
        {

            var fabricante = await _fabricanteManager.UpdateAsync(
            id,
            input.NombreFabricante
            );

            return ObjectMapper.Map<Fabricante, FabricanteDto>(fabricante);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(FabricanteExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _fabricanteRepository.GetListAsync(input.FilterText, input.NombreFabricante);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<Fabricante>, List<FabricanteExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "Fabricantes.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new FabricanteExcelDownloadTokenCacheItem { Token = token },
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