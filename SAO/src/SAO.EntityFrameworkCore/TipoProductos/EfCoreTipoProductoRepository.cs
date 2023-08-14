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

namespace SAO.TipoProductos
{
    public class EfCoreTipoProductoRepository : EfCoreRepository<SAODbContext, TipoProducto, Guid>, ITipoProductoRepository
    {
        public EfCoreTipoProductoRepository(IDbContextProvider<SAODbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<TipoProducto>> GetListAsync(
            string filterText = null,
            string desProducto = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, desProducto);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? TipoProductoConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string desProducto = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, desProducto);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<TipoProducto> ApplyFilter(
            IQueryable<TipoProducto> query,
            string filterText,
            string desProducto = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.DesProducto.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(desProducto), e => e.DesProducto.Contains(desProducto));
        }
    }
}