using Microsoft.AspNetCore.Authorization;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Caching.Distributed;
using MiniExcelLibs;
using SAO.Permissions;
using SAO.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Volo.Abp.Content;

namespace SAO.SustanciaElementals
{

    [Authorize(SAOPermissions.SustanciaElementals.Default)]
    public class SustanciaElementalsAppService : ApplicationService, ISustanciaElementalsAppService
    {
        private readonly IDistributedCache<SustanciaElementalExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly ISustanciaElementalRepository _sustanciaElementalRepository;
        private readonly SustanciaElementalManager _sustanciaElementalManager;

        public SustanciaElementalsAppService(ISustanciaElementalRepository sustanciaElementalRepository, SustanciaElementalManager sustanciaElementalManager, IDistributedCache<SustanciaElementalExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _sustanciaElementalRepository = sustanciaElementalRepository;
            _sustanciaElementalManager = sustanciaElementalManager;
        }

        public virtual async Task<PagedResultDto<SustanciaElementalDto>> GetListAsync(GetSustanciaElementalsInput input)
        {
            var totalCount = await _sustanciaElementalRepository.GetCountAsync(input.FilterText, input.CodCas, input.DesSustancia);
            var items = await _sustanciaElementalRepository.GetListAsync(input.FilterText, input.CodCas, input.DesSustancia, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<SustanciaElementalDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<SustanciaElemental>, List<SustanciaElementalDto>>(items)
            };
        }

        public virtual async Task<SustanciaElementalDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<SustanciaElemental, SustanciaElementalDto>(await _sustanciaElementalRepository.GetAsync(id));
        }

        [Authorize(SAOPermissions.SustanciaElementals.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _sustanciaElementalRepository.DeleteAsync(id);
        }

        [Authorize(SAOPermissions.SustanciaElementals.Create)]
        public virtual async Task<SustanciaElementalDto> CreateAsync(SustanciaElementalCreateDto input)
        {

            var sustanciaElemental = await _sustanciaElementalManager.CreateAsync(
            input.CodCas, input.DesSustancia
            );

            return ObjectMapper.Map<SustanciaElemental, SustanciaElementalDto>(sustanciaElemental);
        }

        [Authorize(SAOPermissions.SustanciaElementals.Edit)]
        public virtual async Task<SustanciaElementalDto> UpdateAsync(Guid id, SustanciaElementalUpdateDto input)
        {
            try
            {
                var sustanciaElemental = await _sustanciaElementalManager.UpdateAsync(
           id,
           input.CodCas, input.DesSustancia
           );
                return ObjectMapper.Map<SustanciaElemental, SustanciaElementalDto>(sustanciaElemental);

            }
            catch (SqlException e)
            {

                throw new UserFriendlyException("Ha ocurrido un error: " + e.Message);
            }



        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(SustanciaElementalExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _sustanciaElementalRepository.GetListAsync(input.FilterText, input.CodCas, input.DesSustancia);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<SustanciaElemental>, List<SustanciaElementalExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "SustanciaElementals.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new SustanciaElementalExcelDownloadTokenCacheItem { Token = token },
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