using SAO.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using SAO.Shared;

namespace SAO.TotalImportacioness
{
    public interface ITotalImportacionessAppService : IApplicationService
    {
        Task<PagedResultDto<TotalImportacionesWithNavigationPropertiesDto>> GetListAsync(GetTotalImportacionessInput input);

        Task<TotalImportacionesWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<TotalImportacionesDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetImportadorLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid>>> GetTipoProductoLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<int>>> GetAsraeLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<TotalImportacionesDto> CreateAsync(TotalImportacionesCreateDto input);

        Task<TotalImportacionesDto> UpdateAsync(Guid id, TotalImportacionesUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(TotalImportacionesExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}