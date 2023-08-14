using SAO.Shared;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;

namespace SAO.TipoEnvases
{
    public interface ITipoEnvasesAppService : IApplicationService
    {
        Task<PagedResultDto<TipoEnvaseDto>> GetListAsync(GetTipoEnvasesInput input);

        Task<TipoEnvaseDto> GetAsync(int id);

        Task DeleteAsync(int id);

        Task<TipoEnvaseDto> CreateAsync(TipoEnvaseCreateDto input);

        Task<TipoEnvaseDto> UpdateAsync(int id, TipoEnvaseUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(TipoEnvaseExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}