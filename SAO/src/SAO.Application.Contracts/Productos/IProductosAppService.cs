using SAO.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;

namespace SAO.Productos
{
    public interface IProductosAppService : IApplicationService
    {
        Task<PagedResultDto<ProductoWithNavigationPropertiesDto>> GetListAsync(GetProductosInput input);

        Task<ProductoWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<ProductoDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetFabricanteLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<int>>> GetAsraeLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid>>> GetTipoProductoLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid>>> GetSustanciaElementalLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<ProductoDto> CreateAsync(ProductoCreateDto input);

        Task<ProductoDto> UpdateAsync(Guid id, ProductoUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(ProductoExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}