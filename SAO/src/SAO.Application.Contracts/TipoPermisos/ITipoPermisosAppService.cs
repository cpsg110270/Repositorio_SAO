using SAO.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;

namespace SAO.TipoPermisos
{
    public interface ITipoPermisosAppService : IApplicationService
    {
        Task<PagedResultDto<TipoPermisoDto>> GetListAsync(GetTipoPermisosInput input);

        Task<TipoPermisoDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<TipoPermisoDto> CreateAsync(TipoPermisoCreateDto input);

        Task<TipoPermisoDto> UpdateAsync(Guid id, TipoPermisoUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(TipoPermisoExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}