using SAO.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;

namespace SAO.TipoProductos
{
    public interface ITipoProductosAppService : IApplicationService
    {
        Task<PagedResultDto<TipoProductoDto>> GetListAsync(GetTipoProductosInput input);

        Task<TipoProductoDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<TipoProductoDto> CreateAsync(TipoProductoCreateDto input);

        Task<TipoProductoDto> UpdateAsync(Guid id, TipoProductoUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(TipoProductoExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}