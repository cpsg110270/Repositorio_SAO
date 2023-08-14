using Microsoft.EntityFrameworkCore;
using SAO.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace SAO.Asraes
{
    public class EfCoreAsraeRepository : EfCoreRepository<SAODbContext, Asrae, int>, IAsraeRepository
    {
        public EfCoreAsraeRepository(IDbContextProvider<SAODbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<Asrae>> GetListAsync(
            string filterText = null,
            string codigo_ASHRAE = null,
            string descripcion = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, codigo_ASHRAE, descripcion);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? AsraeConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string codigo_ASHRAE = null,
            string descripcion = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, codigo_ASHRAE, descripcion);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Asrae> ApplyFilter(
            IQueryable<Asrae> query,
            string filterText,
            string codigo_ASHRAE = null,
            string descripcion = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Codigo_ASHRAE.Contains(filterText) || e.Descripcion.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(codigo_ASHRAE), e => e.Codigo_ASHRAE.Contains(codigo_ASHRAE))
                    .WhereIf(!string.IsNullOrWhiteSpace(descripcion), e => e.Descripcion.Contains(descripcion));
        }
    }
}