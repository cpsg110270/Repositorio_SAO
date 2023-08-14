using Microsoft.EntityFrameworkCore;
using SAO.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace SAO.Almacens
{
    public class EfCoreAlmacenRepository : EfCoreRepository<SAODbContext, Almacen, int>, IAlmacenRepository
    {
        public EfCoreAlmacenRepository(IDbContextProvider<SAODbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<Almacen>> GetListAsync(
            string filterText = null,
            string nombreAlmacen = null,
            string siglaAlmacen = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, nombreAlmacen, siglaAlmacen);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? AlmacenConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string nombreAlmacen = null,
            string siglaAlmacen = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, nombreAlmacen, siglaAlmacen);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Almacen> ApplyFilter(
            IQueryable<Almacen> query,
            string filterText,
            string nombreAlmacen = null,
            string siglaAlmacen = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.NombreAlmacen.Contains(filterText) || e.SiglaAlmacen.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(nombreAlmacen), e => e.NombreAlmacen.Contains(nombreAlmacen))
                    .WhereIf(!string.IsNullOrWhiteSpace(siglaAlmacen), e => e.SiglaAlmacen.Contains(siglaAlmacen));
        }
    }
}