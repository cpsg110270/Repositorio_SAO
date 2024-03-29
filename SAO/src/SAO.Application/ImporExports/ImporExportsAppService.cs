using SAO.Shared;
using SAO.TipoPermisos;
using SAO.ImporExports;
using SAO.Almacens;
using SAO.Paiss;
using SAO.PuertoEntradaSalidas;
using SAO.TipoEnvases;
using SAO.UnidadMedidas;
using SAO.Productos;
using SAO.Exportadors;
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
//using SAO.ImporExports;

namespace SAO.ImporExports
{

    [Authorize(SAOPermissions.ImporExports.Default)]
    public class ImporExportsAppService : ApplicationService, IImporExportsAppService
    {

        private readonly IImporExportRepository _imporExportRepository;
        private readonly ImporExportManager _imporExportManager;
        private readonly IRepository<Importador, Guid> _importadorRepository;
        private readonly IRepository<Exportador, Guid> _exportadorRepository;
        private readonly IRepository<Producto, Guid> _productoRepository;
        private readonly IRepository<UnidadMedida, int> _unidadMedidaRepository;
        private readonly IRepository<TipoEnvase, int> _tipoEnvaseRepository;
        private readonly IRepository<PuertoEntradaSalida, int> _puertoEntradaSalidaRepository;
        private readonly IRepository<Pais, int> _paisRepository;
        private readonly IRepository<Almacen, int> _almacenRepository;
        private readonly IRepository<TipoPermiso, Guid> _tipoPermisoRepository;

        public ImporExportsAppService(IImporExportRepository imporExportRepository, ImporExportManager imporExportManager, IRepository<Importador, Guid> importadorRepository, IRepository<Exportador, Guid> exportadorRepository, IRepository<Producto, Guid> productoRepository, IRepository<UnidadMedida, int> unidadMedidaRepository, IRepository<TipoEnvase, int> tipoEnvaseRepository, IRepository<PuertoEntradaSalida, int> puertoEntradaSalidaRepository, IRepository<Pais, int> paisRepository, IRepository<Almacen, int> almacenRepository, IRepository<TipoPermiso, Guid> tipoPermisoRepository)
        {

            _imporExportRepository = imporExportRepository;
            _imporExportManager = imporExportManager; _importadorRepository = importadorRepository;
            _exportadorRepository = exportadorRepository;
            _productoRepository = productoRepository;
            _unidadMedidaRepository = unidadMedidaRepository;
            _tipoEnvaseRepository = tipoEnvaseRepository;
            _puertoEntradaSalidaRepository = puertoEntradaSalidaRepository;
            _paisRepository = paisRepository;
            _almacenRepository = almacenRepository;
            _tipoPermisoRepository = tipoPermisoRepository;
        }

        public virtual async Task<PagedResultDto<ImporExportWithNavigationPropertiesDto>> GetListAsync(GetImporExportsInput input)
        {
            var totalCount = await _imporExportRepository.GetCountAsync(input.FilterText, input.NoPermiso, input.FechaEmisionMin, input.FechaEmisionMax, input.FechaSolicitudMin, input.FechaSolicitudMax, input.PesoNetoMin, input.PesoNetoMax, input.PesoUnitarioMin, input.PesoUnitarioMax, input.CantEnvvaseMin, input.CantEnvvaseMax, input.NoFactura, input.Observaciones, input.EsRenovacion, input.Estado, input.ImportadorId, input.ExportadorId, input.ProductoId, input.UnidadMedidaId, input.TipoEnvaseId, input.PuertoEntradaId, input.PuertoSalidaId, input.PaisProcedenciaId, input.PaisDestinoId, input.PaisOrigenId, input.AlmacenId, input.PermisoRenov, input.PermisoDe);
            var items = await _imporExportRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.NoPermiso, input.FechaEmisionMin, input.FechaEmisionMax, input.FechaSolicitudMin, input.FechaSolicitudMax, input.PesoNetoMin, input.PesoNetoMax, input.PesoUnitarioMin, input.PesoUnitarioMax, input.CantEnvvaseMin, input.CantEnvvaseMax, input.NoFactura, input.Observaciones, input.EsRenovacion, input.Estado, input.ImportadorId, input.ExportadorId, input.ProductoId, input.UnidadMedidaId, input.TipoEnvaseId, input.PuertoEntradaId, input.PuertoSalidaId, input.PaisProcedenciaId, input.PaisDestinoId, input.PaisOrigenId, input.AlmacenId, input.PermisoRenov, input.PermisoDe, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<ImporExportWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<ImporExportWithNavigationProperties>, List<ImporExportWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<ImporExportWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<ImporExportWithNavigationProperties, ImporExportWithNavigationPropertiesDto>
                (await _imporExportRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<ImporExportDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<ImporExport, ImporExportDto>(await _imporExportRepository.GetAsync(id));
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

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetExportadorLookupAsync(LookupRequestDto input)
        {
            var query = (await _exportadorRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.NombreExportador != null &&
                         x.NombreExportador.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Exportador>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Exportador>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetProductoLookupAsync(LookupRequestDto input)
        {
            var query = (await _productoRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.NombreComercia != null &&
                         x.NombreComercia.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Producto>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Producto>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        public virtual async Task<PagedResultDto<LookupDto<int>>> GetUnidadMedidaLookupAsync(LookupRequestDto input)
        {
            var query = (await _unidadMedidaRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Abreviatura != null &&
                         x.Abreviatura.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<UnidadMedida>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<int>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<UnidadMedida>, List<LookupDto<int>>>(lookupData)
            };
        }

        public virtual async Task<PagedResultDto<LookupDto<int>>> GetTipoEnvaseLookupAsync(LookupRequestDto input)
        {
            var query = (await _tipoEnvaseRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.DesEnvase != null &&
                         x.DesEnvase.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<TipoEnvase>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<int>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<TipoEnvase>, List<LookupDto<int>>>(lookupData)
            };
        }

        public virtual async Task<PagedResultDto<LookupDto<int>>> GetPuertoEntradaSalidaLookupAsync(LookupRequestDto input)
        {
            var query = (await _puertoEntradaSalidaRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.NombrePuerto != null &&
                         x.NombrePuerto.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<PuertoEntradaSalida>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<int>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<PuertoEntradaSalida>, List<LookupDto<int>>>(lookupData)
            };
        }

        public virtual async Task<PagedResultDto<LookupDto<int>>> GetPaisLookupAsync(LookupRequestDto input)
        {
            var query = (await _paisRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.NombrePais != null &&
                         x.NombrePais.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Pais>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<int>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Pais>, List<LookupDto<int>>>(lookupData)
            };
        }

        public virtual async Task<PagedResultDto<LookupDto<int>>> GetAlmacenLookupAsync(LookupRequestDto input)
        {
            var query = (await _almacenRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.NombreAlmacen != null &&
                         x.NombreAlmacen.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Almacen>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<int>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Almacen>, List<LookupDto<int>>>(lookupData)
            };
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetImporExportLookupAsync(LookupRequestDto input)
        {
            var query = (await _imporExportRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.NoPermiso != null &&
                         x.NoPermiso.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<ImporExport>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<ImporExport>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetTipoPermisoLookupAsync(LookupRequestDto input)
        {
            var query = (await _tipoPermisoRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Desripcion != null &&
                         x.Desripcion.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<TipoPermiso>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<TipoPermiso>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        [Authorize(SAOPermissions.ImporExports.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _imporExportRepository.DeleteAsync(id);
        }

        [Authorize(SAOPermissions.ImporExports.Create)]
        public virtual async Task<ImporExportDto> CreateAsync(ImporExportCreateDto input)
        {
            if (input.ExportadorId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Exportador"]]);
            }
            if (input.ProductoId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Producto"]]);
            }
            if (input.UnidadMedidaId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["UnidadMedida"]]);
            }
            if (input.TipoEnvaseId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["TipoEnvase"]]);
            }
            if (input.PermisoDe == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["TipoPermiso"]]);
            }

            if (input.PaisDestinoId == 0)
            { input.PaisDestinoId = null; }

            if (input.PaisProcedenciaId == 0)
            { input.PaisDestinoId = null; }

            if (input.PuertoEntradaId == 0)
            { input.PuertoEntradaId = null; }

            if (input.PuertoSalidaId == 0)
            { input.PuertoSalidaId = null; }

             if (input.PermisoRenov == Guid.Empty)
            { input.PermisoRenov = null; }

         

            var ImporExport = await _imporExportManager.CreateAsync(
            input.ImportadorId, input.ExportadorId, input.ProductoId, input.UnidadMedidaId, input.TipoEnvaseId, input.PuertoEntradaId, input.PuertoSalidaId, input.PaisProcedenciaId, input.PaisDestinoId, input.PaisOrigenId, input.AlmacenId, input.PermisoRenov, input.PermisoDe, input.NoPermiso, input.FechaEmision, input.FechaSolicitud, input.PesoNeto, input.PesoUnitario, input.CantEnvvase, input.NoFactura, input.Observaciones, input.EsRenovacion, input.Estado
            );

            return ObjectMapper.Map<ImporExport, ImporExportDto>(ImporExport);
        }

        [Authorize(SAOPermissions.ImporExports.Edit)]
        public virtual async Task<ImporExportDto> UpdateAsync(Guid id, ImporExportUpdateDto input)
        {
            if (input.ExportadorId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Exportador"]]);
            }
            if (input.ProductoId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Producto"]]);
            }
            if (input.UnidadMedidaId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["UnidadMedida"]]);
            }
            if (input.TipoEnvaseId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["TipoEnvase"]]);
            }
            if (input.PermisoDe == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["TipoPermiso"]]);
            }

            var imporExport = await _imporExportManager.UpdateAsync(
            id,
            input.ImportadorId, input.ExportadorId, input.ProductoId, input.UnidadMedidaId, input.TipoEnvaseId, input.PuertoEntradaId, input.PuertoSalidaId, input.PaisProcedenciaId, input.PaisDestinoId, input.PaisOrigenId, input.AlmacenId, input.PermisoRenov, input.PermisoDe, input.NoPermiso, input.FechaEmision, input.FechaSolicitud, input.PesoNeto, input.PesoUnitario, input.CantEnvvase, input.NoFactura, input.Observaciones, input.EsRenovacion, input.Estado
            );

            return ObjectMapper.Map<ImporExport, ImporExportDto>(imporExport);
        }
    }
}