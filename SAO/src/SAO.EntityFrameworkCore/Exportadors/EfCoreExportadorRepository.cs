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

namespace SAO.Exportadors
{
    public class EfCoreExportadorRepository : EfCoreRepository<SAODbContext, Exportador, Guid>, IExportadorRepository
    {
        public EfCoreExportadorRepository(IDbContextProvider<SAODbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<Exportador>> GetListAsync(
            string filterText = null,
            int? noImportadorMin = null,
            int? noImportadorMax = null,
            string nombreExportador = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, noImportadorMin, noImportadorMax, nombreExportador);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ExportadorConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            int? noImportadorMin = null,
            int? noImportadorMax = null,
            string nombreExportador = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, noImportadorMin, noImportadorMax, nombreExportador);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Exportador> ApplyFilter(
            IQueryable<Exportador> query,
            string filterText,
            int? noImportadorMin = null,
            int? noImportadorMax = null,
            string nombreExportador = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.NombreExportador.Contains(filterText))
                    .WhereIf(noImportadorMin.HasValue, e => e.NoImportador >= noImportadorMin.Value)
                    .WhereIf(noImportadorMax.HasValue, e => e.NoImportador <= noImportadorMax.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(nombreExportador), e => e.NombreExportador.Contains(nombreExportador));
        }
    }
}