using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using SAO.Permissions;
using SAO.Importadors;

namespace SAO.Importadors
{

    [Authorize(SAOPermissions.Importadors.Default)]
    public class ImportadorsAppService : ApplicationService, IImportadorsAppService
    {

        private readonly IImportadorRepository _importadorRepository;
        private readonly ImportadorManager _importadorManager;

        public ImportadorsAppService(IImportadorRepository importadorRepository, ImportadorManager importadorManager)
        {

            _importadorRepository = importadorRepository;
            _importadorManager = importadorManager;
        }

        public virtual async Task<PagedResultDto<ImportadorDto>> GetListAsync(GetImportadorsInput input)
        {
            var totalCount = await _importadorRepository.GetCountAsync(input.FilterText, input.NoImportadorMin, input.NoImportadorMax, input.NoRUC, input.NombreImportador);
            var items = await _importadorRepository.GetListAsync(input.FilterText, input.NoImportadorMin, input.NoImportadorMax, input.NoRUC, input.NombreImportador, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<ImportadorDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Importador>, List<ImportadorDto>>(items)
            };
        }

        public virtual async Task<ImportadorDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Importador, ImportadorDto>(await _importadorRepository.GetAsync(id));
        }

        [Authorize(SAOPermissions.Importadors.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _importadorRepository.DeleteAsync(id);
        }

        [Authorize(SAOPermissions.Importadors.Create)]
        public virtual async Task<ImportadorDto> CreateAsync(ImportadorCreateDto input)
        {

            var importador = await _importadorManager.CreateAsync(
            input.NoImportador, input.NoRUC, input.NombreImportador
            );

            return ObjectMapper.Map<Importador, ImportadorDto>(importador);
        }

        [Authorize(SAOPermissions.Importadors.Edit)]
        public virtual async Task<ImportadorDto> UpdateAsync(Guid id, ImportadorUpdateDto input)
        {

            var importador = await _importadorManager.UpdateAsync(
            id,
            input.NoImportador, input.NoRUC, input.NombreImportador
            );

            return ObjectMapper.Map<Importador, ImportadorDto>(importador);
        }
    }
}