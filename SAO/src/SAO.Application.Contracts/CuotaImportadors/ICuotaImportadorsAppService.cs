using SAO.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using SAO.Shared;

namespace SAO.CuotaImportadors
{
    public interface ICuotaImportadorsAppService : IApplicationService
    {
        Task<PagedResultDto<CuotaImportadorWithNavigationPropertiesDto>> GetListAsync(GetCuotaImportadorsInput input);

        Task<CuotaImportadorWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<CuotaImportadorDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetImportadorLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<CuotaImportadorDto> CreateAsync(CuotaImportadorCreateDto input);

        Task<CuotaImportadorDto> UpdateAsync(Guid id, CuotaImportadorUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(CuotaImportadorExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}