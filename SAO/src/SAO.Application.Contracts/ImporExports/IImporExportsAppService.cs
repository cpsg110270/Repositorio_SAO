using SAO.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace SAO.ImporExports
{
    public interface IImporExportsAppService : IApplicationService
    {
        Task<PagedResultDto<ImporExportWithNavigationPropertiesDto>> GetListAsync(GetImporExportsInput input);

        Task<ImporExportWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<ImporExportDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetImportadorLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid>>> GetExportadorLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid>>> GetProductoLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<int>>> GetUnidadMedidaLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<int>>> GetTipoEnvaseLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<int>>> GetPuertoEntradaSalidaLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<int>>> GetPaisLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<int>>> GetAlmacenLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid>>> GetImporExportLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid>>> GetTipoPermisoLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<ImporExportDto> CreateAsync(ImporExportCreateDto input);

        Task<ImporExportDto> UpdateAsync(Guid id, ImporExportUpdateDto input);
    }
}