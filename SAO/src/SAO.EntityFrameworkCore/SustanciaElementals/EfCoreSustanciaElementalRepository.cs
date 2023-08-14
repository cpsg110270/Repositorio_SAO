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

namespace SAO.SustanciaElementals
{
    public class EfCoreSustanciaElementalRepository : EfCoreRepository<SAODbContext, SustanciaElemental, Guid>, ISustanciaElementalRepository
    {
        public EfCoreSustanciaElementalRepository(IDbContextProvider<SAODbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<SustanciaElemental>> GetListAsync(
            string filterText = null,
            string codCas = null,
            string desSustancia = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, codCas, desSustancia);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? SustanciaElementalConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string codCas = null,
            string desSustancia = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, codCas, desSustancia);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<SustanciaElemental> ApplyFilter(
            IQueryable<SustanciaElemental> query,
            string filterText,
            string codCas = null,
            string desSustancia = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.CodCas.Contains(filterText) || e.DesSustancia.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(codCas), e => e.CodCas.Contains(codCas))
                    .WhereIf(!string.IsNullOrWhiteSpace(desSustancia), e => e.DesSustancia.Contains(desSustancia));
        }
    }
}