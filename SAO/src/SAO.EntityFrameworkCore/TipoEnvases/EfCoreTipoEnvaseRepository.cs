using Microsoft.EntityFrameworkCore;
using SAO.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace SAO.TipoEnvases
{
    public class EfCoreTipoEnvaseRepository : EfCoreRepository<SAODbContext, TipoEnvase, int>, ITipoEnvaseRepository
    {
        public EfCoreTipoEnvaseRepository(IDbContextProvider<SAODbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<TipoEnvase>> GetListAsync(
            string filterText = null,
            string desEnvase = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, desEnvase);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? TipoEnvaseConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string desEnvase = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, desEnvase);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<TipoEnvase> ApplyFilter(
            IQueryable<TipoEnvase> query,
            string filterText,
            string desEnvase = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.DesEnvase.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(desEnvase), e => e.DesEnvase.Contains(desEnvase));
        }
    }
}