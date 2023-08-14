using Microsoft.EntityFrameworkCore;
using SAO.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

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
            string nombreExportador = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, nombreExportador);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ExportadorConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string nombreExportador = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, nombreExportador);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Exportador> ApplyFilter(
            IQueryable<Exportador> query,
            string filterText,
            string nombreExportador = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.NombreExportador.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(nombreExportador), e => e.NombreExportador.Contains(nombreExportador));
        }
    }
}