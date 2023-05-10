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

namespace SAO.Fabricantes
{
    public class EfCoreFabricanteRepository : EfCoreRepository<SAODbContext, Fabricante, Guid>, IFabricanteRepository
    {
        public EfCoreFabricanteRepository(IDbContextProvider<SAODbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<Fabricante>> GetListAsync(
            string filterText = null,
            string nombreFabricante = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, nombreFabricante);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? FabricanteConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string nombreFabricante = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, nombreFabricante);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Fabricante> ApplyFilter(
            IQueryable<Fabricante> query,
            string filterText,
            string nombreFabricante = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.NombreFabricante.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(nombreFabricante), e => e.NombreFabricante.Contains(nombreFabricante));
        }
    }
}