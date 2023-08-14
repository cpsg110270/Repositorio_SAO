using Microsoft.EntityFrameworkCore;
using SAO.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace SAO.UnidadMedidas
{
    public class EfCoreUnidadMedidaRepository : EfCoreRepository<SAODbContext, UnidadMedida, int>, IUnidadMedidaRepository
    {
        public EfCoreUnidadMedidaRepository(IDbContextProvider<SAODbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<UnidadMedida>> GetListAsync(
            string filterText = null,
            string abreviatura = null,
            string nombreUnidad = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, abreviatura, nombreUnidad);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? UnidadMedidaConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string abreviatura = null,
            string nombreUnidad = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, abreviatura, nombreUnidad);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<UnidadMedida> ApplyFilter(
            IQueryable<UnidadMedida> query,
            string filterText,
            string abreviatura = null,
            string nombreUnidad = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Abreviatura.Contains(filterText) || e.NombreUnidad.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(abreviatura), e => e.Abreviatura.Contains(abreviatura))
                    .WhereIf(!string.IsNullOrWhiteSpace(nombreUnidad), e => e.NombreUnidad.Contains(nombreUnidad));
        }
    }
}