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
using SAO.Exportadors;

namespace SAO.Exportadors
{

    [Authorize(SAOPermissions.Exportadors.Default)]
    public class ExportadorsAppService : ApplicationService, IExportadorsAppService
    {

        private readonly IExportadorRepository _exportadorRepository;
        private readonly ExportadorManager _exportadorManager;

        public ExportadorsAppService(IExportadorRepository exportadorRepository, ExportadorManager exportadorManager)
        {

            _exportadorRepository = exportadorRepository;
            _exportadorManager = exportadorManager;
        }

        public virtual async Task<PagedResultDto<ExportadorDto>> GetListAsync(GetExportadorsInput input)
        {
            var totalCount = await _exportadorRepository.GetCountAsync(input.FilterText, input.NoImportadorMin, input.NoImportadorMax, input.NombreExportador);
            var items = await _exportadorRepository.GetListAsync(input.FilterText, input.NoImportadorMin, input.NoImportadorMax, input.NombreExportador, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<ExportadorDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Exportador>, List<ExportadorDto>>(items)
            };
        }

        public virtual async Task<ExportadorDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Exportador, ExportadorDto>(await _exportadorRepository.GetAsync(id));
        }

        [Authorize(SAOPermissions.Exportadors.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _exportadorRepository.DeleteAsync(id);
        }

        [Authorize(SAOPermissions.Exportadors.Create)]
        public virtual async Task<ExportadorDto> CreateAsync(ExportadorCreateDto input)
        {

            var exportador = await _exportadorManager.CreateAsync(
            input.NoImportador, input.NombreExportador
            );

            return ObjectMapper.Map<Exportador, ExportadorDto>(exportador);
        }

        [Authorize(SAOPermissions.Exportadors.Edit)]
        public virtual async Task<ExportadorDto> UpdateAsync(Guid id, ExportadorUpdateDto input)
        {

            var exportador = await _exportadorManager.UpdateAsync(
            id,
            input.NoImportador, input.NombreExportador
            );

            return ObjectMapper.Map<Exportador, ExportadorDto>(exportador);
        }
    }
}