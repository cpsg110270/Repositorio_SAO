using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using SAO.Shared;

namespace SAO.Paiss
{
    public interface IPaissAppService : IApplicationService
    {
        Task<PagedResultDto<PaisDto>> GetListAsync(GetPaissInput input);

        Task<PaisDto> GetAsync(int id);

        Task DeleteAsync(int id);

        Task<PaisDto> CreateAsync(PaisCreateDto input);

        Task<PaisDto> UpdateAsync(int id, PaisUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(PaisExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}