using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace SAO.Importadors
{
    public interface IImportadorsAppService : IApplicationService
    {
        Task<PagedResultDto<ImportadorDto>> GetListAsync(GetImportadorsInput input);

        Task<ImportadorDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<ImportadorDto> CreateAsync(ImportadorCreateDto input);

        Task<ImportadorDto> UpdateAsync(Guid id, ImportadorUpdateDto input);
    }
}