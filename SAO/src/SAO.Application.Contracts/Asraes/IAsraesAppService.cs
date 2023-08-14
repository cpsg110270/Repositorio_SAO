using SAO.Shared;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;

namespace SAO.Asraes
{
    public interface IAsraesAppService : IApplicationService
    {
        Task<PagedResultDto<AsraeDto>> GetListAsync(GetAsraesInput input);

        Task<AsraeDto> GetAsync(int id);

        Task DeleteAsync(int id);

        Task<AsraeDto> CreateAsync(AsraeCreateDto input);

        Task<AsraeDto> UpdateAsync(int id, AsraeUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(AsraeExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}