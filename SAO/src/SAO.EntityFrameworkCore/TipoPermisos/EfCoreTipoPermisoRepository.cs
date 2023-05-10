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

namespace SAO.TipoPermisos
{
    public class EfCoreTipoPermisoRepository : EfCoreRepository<SAODbContext, TipoPermiso, Guid>, ITipoPermisoRepository
    {
        public EfCoreTipoPermisoRepository(IDbContextProvider<SAODbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<TipoPermiso>> GetListAsync(
            string filterText = null,
            string codigo = null,
            string desripcion = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, codigo, desripcion);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? TipoPermisoConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string codigo = null,
            string desripcion = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, codigo, desripcion);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<TipoPermiso> ApplyFilter(
            IQueryable<TipoPermiso> query,
            string filterText,
            string codigo = null,
            string desripcion = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Codigo.Contains(filterText) || e.Desripcion.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(codigo), e => e.Codigo.Contains(codigo))
                    .WhereIf(!string.IsNullOrWhiteSpace(desripcion), e => e.Desripcion.Contains(desripcion));
        }
    }
}