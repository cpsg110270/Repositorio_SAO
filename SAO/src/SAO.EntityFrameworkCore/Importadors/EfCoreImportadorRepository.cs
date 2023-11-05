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
            string nombreImportador = null,
            string noRUC = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, nombreImportador, noRUC);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ImportadorConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string nombreImportador = null,
            string noRUC = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, nombreImportador, noRUC);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Importador> ApplyFilter(
            IQueryable<Importador> query,
            string filterText,
            string nombreImportador = null,
            string noRUC = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.NombreImportador.Contains(filterText) || e.NoRUC.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(nombreImportador), e => e.NombreImportador.Contains(nombreImportador))
                    .WhereIf(!string.IsNullOrWhiteSpace(noRUC), e => e.NoRUC.Contains(noRUC));
        }
    }
}