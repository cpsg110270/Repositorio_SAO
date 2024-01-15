using SAO.Shared;
using SAO.TipoProductos;
using SAO.Asraes;
using SAO.Importadors;
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
using SAO.CuotaImportadors;

namespace SAO.CuotaImportadors
{

    [Authorize(SAOPermissions.CuotaImportadors.Default)]
    public class CuotaImportadorsAppService : ApplicationService, ICuotaImportadorsAppService
    {

        private readonly ICuotaImportadorRepository _cuotaImportadorRepository;
        private readonly CuotaImportadorManager _cuotaImportadorManager;
        private readonly IRepository<Importador, Guid> _importadorRepository;
        private readonly IRepository<Asrae, int> _asraeRepository;
        private readonly IRepository<TipoProducto, Guid> _tipoProductoRepository;

        public CuotaImportadorsAppService(ICuotaImportadorRepository cuotaImportadorRepository, CuotaImportadorManager cuotaImportadorManager, IRepository<Importador, Guid> importadorRepository, IRepository<Asrae, int> asraeRepository, IRepository<TipoProducto, Guid> tipoProductoRepository)
        {

            _cuotaImportadorRepository = cuotaImportadorRepository;
            _cuotaImportadorManager = cuotaImportadorManager; _importadorRepository = importadorRepository;
            _asraeRepository = asraeRepository;
            _tipoProductoRepository = tipoProductoRepository;
        }

        public virtual async Task<PagedResultDto<CuotaImportadorWithNavigationPropertiesDto>> GetListAsync(GetCuotaImportadorsInput input)
        {
            var totalCount = await _cuotaImportadorRepository.GetCountAsync(input.FilterText, input.AñoMin, input.AñoMax, input.CuotaMin, input.CuotaMax, input.ImportadorId, input.AsraeId, input.TipoProductoId);
            var items = await _cuotaImportadorRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.AñoMin, input.AñoMax, input.CuotaMin, input.CuotaMax, input.ImportadorId, input.AsraeId, input.TipoProductoId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<CuotaImportadorWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<CuotaImportadorWithNavigationProperties>, List<CuotaImportadorWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<CuotaImportadorWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<CuotaImportadorWithNavigationProperties, CuotaImportadorWithNavigationPropertiesDto>
                (await _cuotaImportadorRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<CuotaImportadorDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<CuotaImportador, CuotaImportadorDto>(await _cuotaImportadorRepository.GetAsync(id));
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetImportadorLookupAsync(LookupRequestDto input)
        {
            var query = (await _importadorRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.NombreImportador != null &&
                         x.NombreImportador.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Importador>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Importador>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        public virtual async Task<PagedResultDto<LookupDto<int>>> GetAsraeLookupAsync(LookupRequestDto input)
        {
            var query = (await _asraeRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Codigo_ASHRAE != null &&
                         x.Codigo_ASHRAE.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Asrae>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<int>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Asrae>, List<LookupDto<int>>>(lookupData)
            };
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetTipoProductoLookupAsync(LookupRequestDto input)
        {
            var query = (await _tipoProductoRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.DesProducto != null &&
                         x.DesProducto.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<TipoProducto>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<TipoProducto>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        [Authorize(SAOPermissions.CuotaImportadors.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _cuotaImportadorRepository.DeleteAsync(id);
        }

        [Authorize(SAOPermissions.CuotaImportadors.Create)]
        public virtual async Task<CuotaImportadorDto> CreateAsync(CuotaImportadorCreateDto input)
        {
            if (input.ImportadorId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Importador"]]);
            }

            var cuotaImportador = await _cuotaImportadorManager.CreateAsync(
            input.ImportadorId, input.AsraeId, input.TipoProductoId, input.Año, input.Cuota
            );

            return ObjectMapper.Map<CuotaImportador, CuotaImportadorDto>(cuotaImportador);
        }

        [Authorize(SAOPermissions.CuotaImportadors.Edit)]
        public virtual async Task<CuotaImportadorDto> UpdateAsync(Guid id, CuotaImportadorUpdateDto input)
        {
            if (input.ImportadorId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Importador"]]);
            }

            var cuotaImportador = await _cuotaImportadorManager.UpdateAsync(
            id,
            input.ImportadorId, input.AsraeId, input.TipoProductoId, input.Año, input.Cuota
            );

            return ObjectMapper.Map<CuotaImportador, CuotaImportadorDto>(cuotaImportador);
        }
    }
}