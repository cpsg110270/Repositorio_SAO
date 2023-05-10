using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using SAO.Shared;

namespace SAO.Almacens
{
    public interface IAlmacensAppService : IApplicationService
    {
        Task<PagedResultDto<AlmacenDto>> GetListAsync(GetAlmacensInput input);

        Task<AlmacenDto> GetAsync(int id);

        Task DeleteAsync(int id);

        Task<AlmacenDto> CreateAsync(AlmacenCreateDto input);

        Task<AlmacenDto> UpdateAsync(int id, AlmacenUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(AlmacenExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}