using SAO.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;

namespace SAO.SustanciaElementals
{
    public interface ISustanciaElementalsAppService : IApplicationService
    {
        Task<PagedResultDto<SustanciaElementalDto>> GetListAsync(GetSustanciaElementalsInput input);

        Task<SustanciaElementalDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<SustanciaElementalDto> CreateAsync(SustanciaElementalCreateDto input);

        Task<SustanciaElementalDto> UpdateAsync(Guid id, SustanciaElementalUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(SustanciaElementalExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}