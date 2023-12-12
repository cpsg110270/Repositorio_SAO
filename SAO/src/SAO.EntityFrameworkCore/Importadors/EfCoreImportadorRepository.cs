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

namespace SAO.Importadors
{
    public class EfCoreImportadorRepository : EfCoreRepository<SAODbContext, Importador, Guid>, IImportadorRepository
    {
        public EfCoreImportadorRepository(IDbContextProvider<SAODbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<Importador>> GetListAsync(
            string filterText = null,
            int? noImportadorMin = null,
            int? noImportadorMax = null,
            string noRUC = null,
            string nombreImportador = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, noImportadorMin, noImportadorMax, noRUC, nombreImportador);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ImportadorConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            int? noImportadorMin = null,
            int? noImportadorMax = null,
            string noRUC = null,
            string nombreImportador = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, noImportadorMin, noImportadorMax, noRUC, nombreImportador);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Importador> ApplyFilter(
            IQueryable<Importador> query,
            string filterText,
            int? noImportadorMin = null,
            int? noImportadorMax = null,
            string noRUC = null,
            string nombreImportador = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.NoRUC.Contains(filterText) || e.NombreImportador.Contains(filterText))
                    .WhereIf(noImportadorMin.HasValue, e => e.NoImportador >= noImportadorMin.Value)
                    .WhereIf(noImportadorMax.HasValue, e => e.NoImportador <= noImportadorMax.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(noRUC), e => e.NoRUC.Contains(noRUC))
                    .WhereIf(!string.IsNullOrWhiteSpace(nombreImportador), e => e.NombreImportador.Contains(nombreImportador));
        }
    }
}