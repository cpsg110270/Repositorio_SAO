using SAO.Shared;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;

namespace SAO.PuertoEntradaSalidas
{
    public interface IPuertoEntradaSalidasAppService : IApplicationService
    {
        Task<PagedResultDto<PuertoEntradaSalidaDto>> GetListAsync(GetPuertoEntradaSalidasInput input);

        Task<PuertoEntradaSalidaDto> GetAsync(int id);

        Task DeleteAsync(int id);

        Task<PuertoEntradaSalidaDto> CreateAsync(PuertoEntradaSalidaCreateDto input);

        Task<PuertoEntradaSalidaDto> UpdateAsync(int id, PuertoEntradaSalidaUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(PuertoEntradaSalidaExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}