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
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using SAO.EntityFrameworkCore;

namespace SAO.ImporExports
{
    public class EfCoreImporExportRepository : EfCoreRepository<SAODbContext, ImporExport, Guid>, IImporExportRepository
    {
        public EfCoreImporExportRepository(IDbContextProvider<SAODbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<ImporExportWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(imporExport => new ImporExportWithNavigationProperties
                {
                    ImporExport = imporExport,
                    Importador = dbContext.Set<Importador>().FirstOrDefault(c => c.Id == imporExport.ImportadorId),
                    Exportador = dbContext.Set<Exportador>().FirstOrDefault(c => c.Id == imporExport.ExportadorId),
                    Producto = dbContext.Set<Producto>().FirstOrDefault(c => c.Id == imporExport.ProductoId),
                    UnidadMedida = dbContext.Set<UnidadMedida>().FirstOrDefault(c => c.Id == imporExport.UnidadMedidaId),
                    TipoEnvase = dbContext.Set<TipoEnvase>().FirstOrDefault(c => c.Id == imporExport.TipoEnvaseId),
                    PuertoEntradaSalida = dbContext.Set<PuertoEntradaSalida>().FirstOrDefault(c => c.Id == imporExport.PuertoEntradaId),
                    PuertoEntradaSalida1 = dbContext.Set<PuertoEntradaSalida>().FirstOrDefault(c => c.Id == imporExport.PuertoSalidaId),
                    Pais = dbContext.Set<Pais>().FirstOrDefault(c => c.Id == imporExport.PaisProcedenciaId),
                    Pais1 = dbContext.Set<Pais>().FirstOrDefault(c => c.Id == imporExport.PaisDestinoId),
                    Pais2 = dbContext.Set<Pais>().FirstOrDefault(c => c.Id == imporExport.PaisOrigenId),
                    Almacen = dbContext.Set<Almacen>().FirstOrDefault(c => c.Id == imporExport.AlmacenId),
                    ImporExport1 = dbContext.Set<ImporExport>().FirstOrDefault(c => c.Id == imporExport.PermisoRenov),
                    TipoPermiso = dbContext.Set<TipoPermiso>().FirstOrDefault(c => c.Id == imporExport.PermisoDe)
                }).FirstOrDefault();
        }

        public async Task<List<ImporExportWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string noPermiso = null,
            DateTime? fechaEmisionMin = null,
            DateTime? fechaEmisionMax = null,
            DateTime? fechaSolicitudMin = null,
            DateTime? fechaSolicitudMax = null,
            double? pesoNetoMin = null,
            double? pesoNetoMax = null,
            double? pesoUnitarioMin = null,
            double? pesoUnitarioMax = null,
            int? cantEnvvaseMin = null,
            int? cantEnvvaseMax = null,
            string noFactura = null,
            string observaciones = null,
            bool? esRenovacion = null,
            bool? estado = null,
            Guid? importadorId = null,
            Guid? exportadorId = null,
            Guid? productoId = null,
            int? unidadMedidaId = null,
            int? tipoEnvaseId = null,
            int? puertoEntradaId = null,
            int? puertoSalidaId = null,
            int? paisProcedenciaId = null,
            int? paisDestinoId = null,
            int? paisOrigenId = null,
            int? almacenId = null,
            Guid? permisoRenov = null,
            Guid? permisoDe = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, noPermiso, fechaEmisionMin, fechaEmisionMax, fechaSolicitudMin, fechaSolicitudMax, pesoNetoMin, pesoNetoMax, pesoUnitarioMin, pesoUnitarioMax, cantEnvvaseMin, cantEnvvaseMax, noFactura, observaciones, esRenovacion, estado, importadorId, exportadorId, productoId, unidadMedidaId, tipoEnvaseId, puertoEntradaId, puertoSalidaId, paisProcedenciaId, paisDestinoId, paisOrigenId, almacenId, permisoRenov, permisoDe);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ImporExportConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<ImporExportWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from imporExport in (await GetDbSetAsync())
                   join importador in (await GetDbContextAsync()).Set<Importador>() on imporExport.ImportadorId equals importador.Id into importadors
                   from importador in importadors.DefaultIfEmpty()
                   join exportador in (await GetDbContextAsync()).Set<Exportador>() on imporExport.ExportadorId equals exportador.Id into exportadors
                   from exportador in exportadors.DefaultIfEmpty()
                   join producto in (await GetDbContextAsync()).Set<Producto>() on imporExport.ProductoId equals producto.Id into productos
                   from producto in productos.DefaultIfEmpty()
                   join unidadMedida in (await GetDbContextAsync()).Set<UnidadMedida>() on imporExport.UnidadMedidaId equals unidadMedida.Id into unidadMedidas
                   from unidadMedida in unidadMedidas.DefaultIfEmpty()
                   join tipoEnvase in (await GetDbContextAsync()).Set<TipoEnvase>() on imporExport.TipoEnvaseId equals tipoEnvase.Id into tipoEnvases
                   from tipoEnvase in tipoEnvases.DefaultIfEmpty()
                   join puertoEntradaSalida in (await GetDbContextAsync()).Set<PuertoEntradaSalida>() on imporExport.PuertoEntradaId equals puertoEntradaSalida.Id into puertoEntradaSalidas
                   from puertoEntradaSalida in puertoEntradaSalidas.DefaultIfEmpty()
                   join puertoEntradaSalida1 in (await GetDbContextAsync()).Set<PuertoEntradaSalida>() on imporExport.PuertoSalidaId equals puertoEntradaSalida1.Id into puertoEntradaSalidas1
                   from puertoEntradaSalida1 in puertoEntradaSalidas1.DefaultIfEmpty()
                   join pais in (await GetDbContextAsync()).Set<Pais>() on imporExport.PaisProcedenciaId equals pais.Id into paiss
                   from pais in paiss.DefaultIfEmpty()
                   join pais1 in (await GetDbContextAsync()).Set<Pais>() on imporExport.PaisDestinoId equals pais1.Id into paiss1
                   from pais1 in paiss1.DefaultIfEmpty()
                   join pais2 in (await GetDbContextAsync()).Set<Pais>() on imporExport.PaisOrigenId equals pais2.Id into paiss2
                   from pais2 in paiss2.DefaultIfEmpty()
                   join almacen in (await GetDbContextAsync()).Set<Almacen>() on imporExport.AlmacenId equals almacen.Id into almacens
                   from almacen in almacens.DefaultIfEmpty()
                   join imporExport1 in (await GetDbContextAsync()).Set<ImporExport>() on imporExport.PermisoRenov equals imporExport1.Id into imporExports1
                   from imporExport1 in imporExports1.DefaultIfEmpty()
                   join tipoPermiso in (await GetDbContextAsync()).Set<TipoPermiso>() on imporExport.PermisoDe equals tipoPermiso.Id into tipoPermisos
                   from tipoPermiso in tipoPermisos.DefaultIfEmpty()
                   select new ImporExportWithNavigationProperties
                   {
                       ImporExport = imporExport,
                       Importador = importador,
                       Exportador = exportador,
                       Producto = producto,
                       UnidadMedida = unidadMedida,
                       TipoEnvase = tipoEnvase,
                       PuertoEntradaSalida = puertoEntradaSalida,
                       PuertoEntradaSalida1 = puertoEntradaSalida1,
                       Pais = pais,
                       Pais1 = pais1,
                       Pais2 = pais2,
                       Almacen = almacen,
                       ImporExport1 = imporExport1,
                       TipoPermiso = tipoPermiso
                   };
        }

        protected virtual IQueryable<ImporExportWithNavigationProperties> ApplyFilter(
            IQueryable<ImporExportWithNavigationProperties> query,
            string filterText,
            string noPermiso = null,
            DateTime? fechaEmisionMin = null,
            DateTime? fechaEmisionMax = null,
            DateTime? fechaSolicitudMin = null,
            DateTime? fechaSolicitudMax = null,
            double? pesoNetoMin = null,
            double? pesoNetoMax = null,
            double? pesoUnitarioMin = null,
            double? pesoUnitarioMax = null,
            int? cantEnvvaseMin = null,
            int? cantEnvvaseMax = null,
            string noFactura = null,
            string observaciones = null,
            bool? esRenovacion = null,
            bool? estado = null,
            Guid? importadorId = null,
            Guid? exportadorId = null,
            Guid? productoId = null,
            int? unidadMedidaId = null,
            int? tipoEnvaseId = null,
            int? puertoEntradaId = null,
            int? puertoSalidaId = null,
            int? paisProcedenciaId = null,
            int? paisDestinoId = null,
            int? paisOrigenId = null,
            int? almacenId = null,
            Guid? permisoRenov = null,
            Guid? permisoDe = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.ImporExport.NoPermiso.Contains(filterText) || e.ImporExport.NoFactura.Contains(filterText) || e.ImporExport.Observaciones.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(noPermiso), e => e.ImporExport.NoPermiso.Contains(noPermiso))
                    .WhereIf(fechaEmisionMin.HasValue, e => e.ImporExport.FechaEmision >= fechaEmisionMin.Value)
                    .WhereIf(fechaEmisionMax.HasValue, e => e.ImporExport.FechaEmision <= fechaEmisionMax.Value)
                    .WhereIf(fechaSolicitudMin.HasValue, e => e.ImporExport.FechaSolicitud >= fechaSolicitudMin.Value)
                    .WhereIf(fechaSolicitudMax.HasValue, e => e.ImporExport.FechaSolicitud <= fechaSolicitudMax.Value)
                    .WhereIf(pesoNetoMin.HasValue, e => e.ImporExport.PesoNeto >= pesoNetoMin.Value)
                    .WhereIf(pesoNetoMax.HasValue, e => e.ImporExport.PesoNeto <= pesoNetoMax.Value)
                    .WhereIf(pesoUnitarioMin.HasValue, e => e.ImporExport.PesoUnitario >= pesoUnitarioMin.Value)
                    .WhereIf(pesoUnitarioMax.HasValue, e => e.ImporExport.PesoUnitario <= pesoUnitarioMax.Value)
                    .WhereIf(cantEnvvaseMin.HasValue, e => e.ImporExport.CantEnvvase >= cantEnvvaseMin.Value)
                    .WhereIf(cantEnvvaseMax.HasValue, e => e.ImporExport.CantEnvvase <= cantEnvvaseMax.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(noFactura), e => e.ImporExport.NoFactura.Contains(noFactura))
                    .WhereIf(!string.IsNullOrWhiteSpace(observaciones), e => e.ImporExport.Observaciones.Contains(observaciones))
                    .WhereIf(esRenovacion.HasValue, e => e.ImporExport.EsRenovacion == esRenovacion)
                    .WhereIf(estado.HasValue, e => e.ImporExport.Estado == estado)
                    .WhereIf(importadorId != null && importadorId != Guid.Empty, e => e.Importador != null && e.Importador.Id == importadorId)
                    .WhereIf(exportadorId != null && exportadorId != Guid.Empty, e => e.Exportador != null && e.Exportador.Id == exportadorId)
                    .WhereIf(productoId != null && productoId != Guid.Empty, e => e.Producto != null && e.Producto.Id == productoId)
                    .WhereIf(unidadMedidaId != null, e => e.UnidadMedida != null && e.UnidadMedida.Id == unidadMedidaId)
                    .WhereIf(tipoEnvaseId != null, e => e.TipoEnvase != null && e.TipoEnvase.Id == tipoEnvaseId)
                    .WhereIf(puertoEntradaId != null, e => e.PuertoEntradaSalida != null && e.PuertoEntradaSalida.Id == puertoEntradaId)
                    .WhereIf(puertoSalidaId != null, e => e.PuertoEntradaSalida1 != null && e.PuertoEntradaSalida1.Id == puertoSalidaId)
                    .WhereIf(paisProcedenciaId != null, e => e.Pais != null && e.Pais.Id == paisProcedenciaId)
                    .WhereIf(paisDestinoId != null, e => e.Pais1 != null && e.Pais1.Id == paisDestinoId)
                    .WhereIf(paisOrigenId != null, e => e.Pais2 != null && e.Pais2.Id == paisOrigenId)
                    .WhereIf(almacenId != null, e => e.Almacen != null && e.Almacen.Id == almacenId)
                    .WhereIf(permisoRenov != null && permisoRenov != Guid.Empty, e => e.ImporExport1 != null && e.ImporExport1.Id == permisoRenov)
                    .WhereIf(permisoDe != null && permisoDe != Guid.Empty, e => e.TipoPermiso != null && e.TipoPermiso.Id == permisoDe);
        }

        public async Task<List<ImporExport>> GetListAsync(
            string filterText = null,
            string noPermiso = null,
            DateTime? fechaEmisionMin = null,
            DateTime? fechaEmisionMax = null,
            DateTime? fechaSolicitudMin = null,
            DateTime? fechaSolicitudMax = null,
            double? pesoNetoMin = null,
            double? pesoNetoMax = null,
            double? pesoUnitarioMin = null,
            double? pesoUnitarioMax = null,
            int? cantEnvvaseMin = null,
            int? cantEnvvaseMax = null,
            string noFactura = null,
            string observaciones = null,
            bool? esRenovacion = null,
            bool? estado = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, noPermiso, fechaEmisionMin, fechaEmisionMax, fechaSolicitudMin, fechaSolicitudMax, pesoNetoMin, pesoNetoMax, pesoUnitarioMin, pesoUnitarioMax, cantEnvvaseMin, cantEnvvaseMax, noFactura, observaciones, esRenovacion, estado);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ImporExportConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string noPermiso = null,
            DateTime? fechaEmisionMin = null,
            DateTime? fechaEmisionMax = null,
            DateTime? fechaSolicitudMin = null,
            DateTime? fechaSolicitudMax = null,
            double? pesoNetoMin = null,
            double? pesoNetoMax = null,
            double? pesoUnitarioMin = null,
            double? pesoUnitarioMax = null,
            int? cantEnvvaseMin = null,
            int? cantEnvvaseMax = null,
            string noFactura = null,
            string observaciones = null,
            bool? esRenovacion = null,
            bool? estado = null,
            Guid? importadorId = null,
            Guid? exportadorId = null,
            Guid? productoId = null,
            int? unidadMedidaId = null,
            int? tipoEnvaseId = null,
            int? puertoEntradaId = null,
            int? puertoSalidaId = null,
            int? paisProcedenciaId = null,
            int? paisDestinoId = null,
            int? paisOrigenId = null,
            int? almacenId = null,
            Guid? permisoRenov = null,
            Guid? permisoDe = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, noPermiso, fechaEmisionMin, fechaEmisionMax, fechaSolicitudMin, fechaSolicitudMax, pesoNetoMin, pesoNetoMax, pesoUnitarioMin, pesoUnitarioMax, cantEnvvaseMin, cantEnvvaseMax, noFactura, observaciones, esRenovacion, estado, importadorId, exportadorId, productoId, unidadMedidaId, tipoEnvaseId, puertoEntradaId, puertoSalidaId, paisProcedenciaId, paisDestinoId, paisOrigenId, almacenId, permisoRenov, permisoDe);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<ImporExport> ApplyFilter(
            IQueryable<ImporExport> query,
            string filterText,
            string noPermiso = null,
            DateTime? fechaEmisionMin = null,
            DateTime? fechaEmisionMax = null,
            DateTime? fechaSolicitudMin = null,
            DateTime? fechaSolicitudMax = null,
            double? pesoNetoMin = null,
            double? pesoNetoMax = null,
            double? pesoUnitarioMin = null,
            double? pesoUnitarioMax = null,
            int? cantEnvvaseMin = null,
            int? cantEnvvaseMax = null,
            string noFactura = null,
            string observaciones = null,
            bool? esRenovacion = null,
            bool? estado = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.NoPermiso.Contains(filterText) || e.NoFactura.Contains(filterText) || e.Observaciones.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(noPermiso), e => e.NoPermiso.Contains(noPermiso))
                    .WhereIf(fechaEmisionMin.HasValue, e => e.FechaEmision >= fechaEmisionMin.Value)
                    .WhereIf(fechaEmisionMax.HasValue, e => e.FechaEmision <= fechaEmisionMax.Value)
                    .WhereIf(fechaSolicitudMin.HasValue, e => e.FechaSolicitud >= fechaSolicitudMin.Value)
                    .WhereIf(fechaSolicitudMax.HasValue, e => e.FechaSolicitud <= fechaSolicitudMax.Value)
                    .WhereIf(pesoNetoMin.HasValue, e => e.PesoNeto >= pesoNetoMin.Value)
                    .WhereIf(pesoNetoMax.HasValue, e => e.PesoNeto <= pesoNetoMax.Value)
                    .WhereIf(pesoUnitarioMin.HasValue, e => e.PesoUnitario >= pesoUnitarioMin.Value)
                    .WhereIf(pesoUnitarioMax.HasValue, e => e.PesoUnitario <= pesoUnitarioMax.Value)
                    .WhereIf(cantEnvvaseMin.HasValue, e => e.CantEnvvase >= cantEnvvaseMin.Value)
                    .WhereIf(cantEnvvaseMax.HasValue, e => e.CantEnvvase <= cantEnvvaseMax.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(noFactura), e => e.NoFactura.Contains(noFactura))
                    .WhereIf(!string.IsNullOrWhiteSpace(observaciones), e => e.Observaciones.Contains(observaciones))
                    .WhereIf(esRenovacion.HasValue, e => e.EsRenovacion == esRenovacion)
                    .WhereIf(estado.HasValue, e => e.Estado == estado);
        }
    }
}