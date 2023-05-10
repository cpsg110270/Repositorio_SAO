using SAO.TipoProductos;
using SAO.Asraes;
using SAO.Fabricantes;
using SAO.SustanciaElementals;
using SAO.SustanciaElementals;
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

namespace SAO.Productos
{
    public class EfCoreProductoRepository : EfCoreRepository<SAODbContext, Producto, Guid>, IProductoRepository
    {
        public EfCoreProductoRepository(IDbContextProvider<SAODbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<ProductoWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id).Include(x => x.SustanciaElementals)
                .Select(producto => new ProductoWithNavigationProperties
                {
                    Producto = producto,
                    Fabricante = dbContext.Set<Fabricante>().FirstOrDefault(c => c.Id == producto.FabricanteId),
                    Asrae = dbContext.Set<Asrae>().FirstOrDefault(c => c.Id == producto.AsraeId),
                    TipoProducto = dbContext.Set<TipoProducto>().FirstOrDefault(c => c.Id == producto.TipoProductoId),
                    SustanciaElementals = (from productoSustanciaElementals in producto.SustanciaElementals
                                           join _sustanciaElemental in dbContext.Set<SustanciaElemental>() on productoSustanciaElementals.SustanciaElementalId equals _sustanciaElemental.Id
                                           select _sustanciaElemental).ToList()
                }).FirstOrDefault();
        }

        public async Task<List<ProductoWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string nombreComercia = null,
            string uso = null,
            Guid? fabricanteId = null,
            int? asraeId = null,
            Guid? tipoProductoId = null,
            Guid? sustanciaElementalId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, nombreComercia, uso, fabricanteId, asraeId, tipoProductoId, sustanciaElementalId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ProductoConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<ProductoWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from producto in (await GetDbSetAsync())
                   join fabricante in (await GetDbContextAsync()).Set<Fabricante>() on producto.FabricanteId equals fabricante.Id into fabricantes
                   from fabricante in fabricantes.DefaultIfEmpty()
                   join asrae in (await GetDbContextAsync()).Set<Asrae>() on producto.AsraeId equals asrae.Id into asraes
                   from asrae in asraes.DefaultIfEmpty()
                   join tipoProducto in (await GetDbContextAsync()).Set<TipoProducto>() on producto.TipoProductoId equals tipoProducto.Id into tipoProductos
                   from tipoProducto in tipoProductos.DefaultIfEmpty()
                   select new ProductoWithNavigationProperties
                   {
                       Producto = producto,
                       Fabricante = fabricante,
                       Asrae = asrae,
                       TipoProducto = tipoProducto,
                       SustanciaElementals = new List<SustanciaElemental>()
                   };
        }

        protected virtual IQueryable<ProductoWithNavigationProperties> ApplyFilter(
            IQueryable<ProductoWithNavigationProperties> query,
            string filterText,
            string nombreComercia = null,
            string uso = null,
            Guid? fabricanteId = null,
            int? asraeId = null,
            Guid? tipoProductoId = null,
            Guid? sustanciaElementalId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Producto.NombreComercia.Contains(filterText) || e.Producto.Uso.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(nombreComercia), e => e.Producto.NombreComercia.Contains(nombreComercia))
                    .WhereIf(!string.IsNullOrWhiteSpace(uso), e => e.Producto.Uso.Contains(uso))
                    .WhereIf(fabricanteId != null && fabricanteId != Guid.Empty, e => e.Fabricante != null && e.Fabricante.Id == fabricanteId)
                    .WhereIf(asraeId != null, e => e.Asrae != null && e.Asrae.Id == asraeId)
                    .WhereIf(tipoProductoId != null && tipoProductoId != Guid.Empty, e => e.TipoProducto != null && e.TipoProducto.Id == tipoProductoId)
                    .WhereIf(sustanciaElementalId != null && sustanciaElementalId != Guid.Empty, e => e.Producto.SustanciaElementals.Any(x => x.SustanciaElementalId == sustanciaElementalId));
        }

        public async Task<List<Producto>> GetListAsync(
            string filterText = null,
            string nombreComercia = null,
            string uso = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, nombreComercia, uso);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ProductoConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string nombreComercia = null,
            string uso = null,
            Guid? fabricanteId = null,
            int? asraeId = null,
            Guid? tipoProductoId = null,
            Guid? sustanciaElementalId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, nombreComercia, uso, fabricanteId, asraeId, tipoProductoId, sustanciaElementalId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Producto> ApplyFilter(
            IQueryable<Producto> query,
            string filterText,
            string nombreComercia = null,
            string uso = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.NombreComercia.Contains(filterText) || e.Uso.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(nombreComercia), e => e.NombreComercia.Contains(nombreComercia))
                    .WhereIf(!string.IsNullOrWhiteSpace(uso), e => e.Uso.Contains(uso));
        }
    }
}