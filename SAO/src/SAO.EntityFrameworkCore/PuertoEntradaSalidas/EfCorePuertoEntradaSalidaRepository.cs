using Microsoft.EntityFrameworkCore;
using SAO.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace SAO.PuertoEntradaSalidas
{
    public class EfCorePuertoEntradaSalidaRepository : EfCoreRepository<SAODbContext, PuertoEntradaSalida, int>, IPuertoEntradaSalidaRepository
    {
        public EfCorePuertoEntradaSalidaRepository(IDbContextProvider<SAODbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<PuertoEntradaSalida>> GetListAsync(
            string filterText = null,
            string nombrePuerto = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, nombrePuerto);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? PuertoEntradaSalidaConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string nombrePuerto = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, nombrePuerto);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<PuertoEntradaSalida> ApplyFilter(
            IQueryable<PuertoEntradaSalida> query,
            string filterText,
            string nombrePuerto = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.NombrePuerto.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(nombrePuerto), e => e.NombrePuerto.Contains(nombrePuerto));
        }
    }
}