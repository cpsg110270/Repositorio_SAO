using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace SAO.Exportadors
{
    public interface IExportadorsAppService : IApplicationService
    {
        Task<PagedResultDto<ExportadorDto>> GetListAsync(GetExportadorsInput input);

        Task<ExportadorDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<ExportadorDto> CreateAsync(ExportadorCreateDto input);

        Task<ExportadorDto> UpdateAsync(Guid id, ExportadorUpdateDto input);
    }
}