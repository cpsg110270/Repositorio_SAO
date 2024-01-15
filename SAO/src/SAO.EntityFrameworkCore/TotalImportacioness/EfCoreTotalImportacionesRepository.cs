using SAO.Asraes;
using SAO.TipoProductos;
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

namespace SAO.TotalImportacioness
{
    public class EfCoreTotalImportacionesRepository : EfCoreRepository<SAODbContext, TotalImportaciones, Guid>, ITotalImportacionesRepository
    {
        public EfCoreTotalImportacionesRepository(IDbContextProvider<SAODbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<TotalImportacionesWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(totalImportaciones => new TotalImportacionesWithNavigationProperties
                {
                    TotalImportaciones = totalImportaciones,
                    Importador = dbContext.Set<Importador>().FirstOrDefault(c => c.Id == totalImportaciones.ImportadorId),
                    TipoProducto = dbContext.Set<TipoProducto>().FirstOrDefault(c => c.Id == totalImportaciones.TipoProductoId),
                    Asrae = dbContext.Set<Asrae>().FirstOrDefault(c => c.Id == totalImportaciones.AsraeId)
                }).FirstOrDefault();
        }

        public async Task<List<TotalImportacionesWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            int? anioMin = null,
            int? anioMax = null,
            double? cuotaAsignadaMin = null,
            double? cuotaAsignadaMax = null,
            double? cuotaConsumidaMin = null,
            double? cuotaConsumidaMax = null,
            Guid? importadorId = null,
            Guid? tipoProductoId = null,
            int? asraeId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, anioMin, anioMax, cuotaAsignadaMin, cuotaAsignadaMax, cuotaConsumidaMin, cuotaConsumidaMax, importadorId, tipoProductoId, asraeId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? TotalImportacionesConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<TotalImportacionesWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from totalImportaciones in (await GetDbSetAsync())
                   join importador in (await GetDbContextAsync()).Set<Importador>() on totalImportaciones.ImportadorId equals importador.Id into importadors
                   from importador in importadors.DefaultIfEmpty()
                   join tipoProducto in (await GetDbContextAsync()).Set<TipoProducto>() on totalImportaciones.TipoProductoId equals tipoProducto.Id into tipoProductos
                   from tipoProducto in tipoProductos.DefaultIfEmpty()
                   join asrae in (await GetDbContextAsync()).Set<Asrae>() on totalImportaciones.AsraeId equals asrae.Id into asraes
                   from asrae in asraes.DefaultIfEmpty()
                   select new TotalImportacionesWithNavigationProperties
                   {
                       TotalImportaciones = totalImportaciones,
                       Importador = importador,
                       TipoProducto = tipoProducto,
                       Asrae = asrae
                   };
        }

        protected virtual IQueryable<TotalImportacionesWithNavigationProperties> ApplyFilter(
            IQueryable<TotalImportacionesWithNavigationProperties> query,
            string filterText,
            int? anioMin = null,
            int? anioMax = null,
            double? cuotaAsignadaMin = null,
            double? cuotaAsignadaMax = null,
            double? cuotaConsumidaMin = null,
            double? cuotaConsumidaMax = null,
            Guid? importadorId = null,
            Guid? tipoProductoId = null,
            int? asraeId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => true)
                    .WhereIf(anioMin.HasValue, e => e.TotalImportaciones.Anio >= anioMin.Value)
                    .WhereIf(anioMax.HasValue, e => e.TotalImportaciones.Anio <= anioMax.Value)
                    .WhereIf(cuotaAsignadaMin.HasValue, e => e.TotalImportaciones.CuotaAsignada >= cuotaAsignadaMin.Value)
                    .WhereIf(cuotaAsignadaMax.HasValue, e => e.TotalImportaciones.CuotaAsignada <= cuotaAsignadaMax.Value)
                    .WhereIf(cuotaConsumidaMin.HasValue, e => e.TotalImportaciones.CuotaConsumida >= cuotaConsumidaMin.Value)
                    .WhereIf(cuotaConsumidaMax.HasValue, e => e.TotalImportaciones.CuotaConsumida <= cuotaConsumidaMax.Value)
                    .WhereIf(importadorId != null && importadorId != Guid.Empty, e => e.Importador != null && e.Importador.Id == importadorId)
                    .WhereIf(tipoProductoId != null && tipoProductoId != Guid.Empty, e => e.TipoProducto != null && e.TipoProducto.Id == tipoProductoId)
                    .WhereIf(asraeId != null, e => e.Asrae != null && e.Asrae.Id == asraeId);
        }

        public async Task<List<TotalImportaciones>> GetListAsync(
            string filterText = null,
            int? anioMin = null,
            int? anioMax = null,
            double? cuotaAsignadaMin = null,
            double? cuotaAsignadaMax = null,
            double? cuotaConsumidaMin = null,
            double? cuotaConsumidaMax = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, anioMin, anioMax, cuotaAsignadaMin, cuotaAsignadaMax, cuotaConsumidaMin, cuotaConsumidaMax);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? TotalImportacionesConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            int? anioMin = null,
            int? anioMax = null,
            double? cuotaAsignadaMin = null,
            double? cuotaAsignadaMax = null,
            double? cuotaConsumidaMin = null,
            double? cuotaConsumidaMax = null,
            Guid? importadorId = null,
            Guid? tipoProductoId = null,
            int? asraeId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, anioMin, anioMax, cuotaAsignadaMin, cuotaAsignadaMax, cuotaConsumidaMin, cuotaConsumidaMax, importadorId, tipoProductoId, asraeId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<TotalImportaciones> ApplyFilter(
            IQueryable<TotalImportaciones> query,
            string filterText,
            int? anioMin = null,
            int? anioMax = null,
            double? cuotaAsignadaMin = null,
            double? cuotaAsignadaMax = null,
            double? cuotaConsumidaMin = null,
            double? cuotaConsumidaMax = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => true)
                    .WhereIf(anioMin.HasValue, e => e.Anio >= anioMin.Value)
                    .WhereIf(anioMax.HasValue, e => e.Anio <= anioMax.Value)
                    .WhereIf(cuotaAsignadaMin.HasValue, e => e.CuotaAsignada >= cuotaAsignadaMin.Value)
                    .WhereIf(cuotaAsignadaMax.HasValue, e => e.CuotaAsignada <= cuotaAsignadaMax.Value)
                    .WhereIf(cuotaConsumidaMin.HasValue, e => e.CuotaConsumida >= cuotaConsumidaMin.Value)
                    .WhereIf(cuotaConsumidaMax.HasValue, e => e.CuotaConsumida <= cuotaConsumidaMax.Value);
        }
    }
}