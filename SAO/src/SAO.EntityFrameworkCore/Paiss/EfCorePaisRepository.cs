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

namespace SAO.Paiss
{
    public class EfCorePaisRepository : EfCoreRepository<SAODbContext, Pais, int>, IPaisRepository
    {
        public EfCorePaisRepository(IDbContextProvider<SAODbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<Pais>> GetListAsync(
            string filterText = null,
            string nombrePais = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, nombrePais);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? PaisConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string nombrePais = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, nombrePais);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Pais> ApplyFilter(
            IQueryable<Pais> query,
            string filterText,
            string nombrePais = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.NombrePais.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(nombrePais), e => e.NombrePais.Contains(nombrePais));
        }
    }
}