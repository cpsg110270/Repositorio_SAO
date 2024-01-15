using SAO.TipoProductos;
using SAO.Asraes;
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

namespace SAO.CuotaImportadors
{
    public class EfCoreCuotaImportadorRepository : EfCoreRepository<SAODbContext, CuotaImportador, Guid>, ICuotaImportadorRepository
    {
        public EfCoreCuotaImportadorRepository(IDbContextProvider<SAODbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<CuotaImportadorWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(cuotaImportador => new CuotaImportadorWithNavigationProperties
                {
                    CuotaImportador = cuotaImportador,
                    Importador = dbContext.Set<Importador>().FirstOrDefault(c => c.Id == cuotaImportador.ImportadorId),
                    Asrae = dbContext.Set<Asrae>().FirstOrDefault(c => c.Id == cuotaImportador.AsraeId),
                    TipoProducto = dbContext.Set<TipoProducto>().FirstOrDefault(c => c.Id == cuotaImportador.TipoProductoId)
                }).FirstOrDefault();
        }

        public async Task<List<CuotaImportadorWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            int? añoMin = null,
            int? añoMax = null,
            decimal? cuotaMin = null,
            decimal? cuotaMax = null,
            Guid? importadorId = null,
            int? asraeId = null,
            Guid? tipoProductoId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, añoMin, añoMax, cuotaMin, cuotaMax, importadorId, asraeId, tipoProductoId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? CuotaImportadorConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<CuotaImportadorWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from cuotaImportador in (await GetDbSetAsync())
                   join importador in (await GetDbContextAsync()).Set<Importador>() on cuotaImportador.ImportadorId equals importador.Id into importadors
                   from importador in importadors.DefaultIfEmpty()
                   join asrae in (await GetDbContextAsync()).Set<Asrae>() on cuotaImportador.AsraeId equals asrae.Id into asraes
                   from asrae in asraes.DefaultIfEmpty()
                   join tipoProducto in (await GetDbContextAsync()).Set<TipoProducto>() on cuotaImportador.TipoProductoId equals tipoProducto.Id into tipoProductos
                   from tipoProducto in tipoProductos.DefaultIfEmpty()
                   select new CuotaImportadorWithNavigationProperties
                   {
                       CuotaImportador = cuotaImportador,
                       Importador = importador,
                       Asrae = asrae,
                       TipoProducto = tipoProducto
                   };
        }

        protected virtual IQueryable<CuotaImportadorWithNavigationProperties> ApplyFilter(
            IQueryable<CuotaImportadorWithNavigationProperties> query,
            string filterText,
            int? añoMin = null,
            int? añoMax = null,
            decimal? cuotaMin = null,
            decimal? cuotaMax = null,
            Guid? importadorId = null,
            int? asraeId = null,
            Guid? tipoProductoId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => true)
                    .WhereIf(añoMin.HasValue, e => e.CuotaImportador.Año >= añoMin.Value)
                    .WhereIf(añoMax.HasValue, e => e.CuotaImportador.Año <= añoMax.Value)
                    .WhereIf(cuotaMin.HasValue, e => e.CuotaImportador.Cuota >= cuotaMin.Value)
                    .WhereIf(cuotaMax.HasValue, e => e.CuotaImportador.Cuota <= cuotaMax.Value)
                    .WhereIf(importadorId != null && importadorId != Guid.Empty, e => e.Importador != null && e.Importador.Id == importadorId)
                    .WhereIf(asraeId != null, e => e.Asrae != null && e.Asrae.Id == asraeId)
                    .WhereIf(tipoProductoId != null && tipoProductoId != Guid.Empty, e => e.TipoProducto != null && e.TipoProducto.Id == tipoProductoId);
        }

        public async Task<List<CuotaImportador>> GetListAsync(
            string filterText = null,
            int? añoMin = null,
            int? añoMax = null,
            decimal? cuotaMin = null,
            decimal? cuotaMax = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, añoMin, añoMax, cuotaMin, cuotaMax);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? CuotaImportadorConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            int? añoMin = null,
            int? añoMax = null,
            decimal? cuotaMin = null,
            decimal? cuotaMax = null,
            Guid? importadorId = null,
            int? asraeId = null,
            Guid? tipoProductoId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, añoMin, añoMax, cuotaMin, cuotaMax, importadorId, asraeId, tipoProductoId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<CuotaImportador> ApplyFilter(
            IQueryable<CuotaImportador> query,
            string filterText,
            int? añoMin = null,
            int? añoMax = null,
            decimal? cuotaMin = null,
            decimal? cuotaMax = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => true)
                    .WhereIf(añoMin.HasValue, e => e.Año >= añoMin.Value)
                    .WhereIf(añoMax.HasValue, e => e.Año <= añoMax.Value)
                    .WhereIf(cuotaMin.HasValue, e => e.Cuota >= cuotaMin.Value)
                    .WhereIf(cuotaMax.HasValue, e => e.Cuota <= cuotaMax.Value);
        }
    }
}