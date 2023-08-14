using SAO.Shared;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;

namespace SAO.UnidadMedidas
{
    public interface IUnidadMedidasAppService : IApplicationService
    {
        Task<PagedResultDto<UnidadMedidaDto>> GetListAsync(GetUnidadMedidasInput input);

        Task<UnidadMedidaDto> GetAsync(int id);

        Task DeleteAsync(int id);

        Task<UnidadMedidaDto> CreateAsync(UnidadMedidaCreateDto input);

        Task<UnidadMedidaDto> UpdateAsync(int id, UnidadMedidaUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(UnidadMedidaExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}