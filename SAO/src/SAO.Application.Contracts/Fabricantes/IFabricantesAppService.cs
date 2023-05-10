using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using SAO.Shared;

namespace SAO.Fabricantes
{
    public interface IFabricantesAppService : IApplicationService
    {
        Task<PagedResultDto<FabricanteDto>> GetListAsync(GetFabricantesInput input);

        Task<FabricanteDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<FabricanteDto> CreateAsync(FabricanteCreateDto input);

        Task<FabricanteDto> UpdateAsync(Guid id, FabricanteUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(FabricanteExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}