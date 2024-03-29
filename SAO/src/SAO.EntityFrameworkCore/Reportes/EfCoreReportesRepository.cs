﻿using SAO.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore;

namespace SAO.Reportes
{
    public class EfCoreReportesRepository : IReportesRepository
    {
        IDbContextProvider<SAODbContext> dbContextProvider;
        public EfCoreReportesRepository(IDbContextProvider<SAODbContext> dbContextProvider)
        {
            this.dbContextProvider = dbContextProvider;
        }

        [Obsolete("Use GetDbContextAsync() method.")]
        public List<CuotaDataModel> GetCuotaData()
        {
            var dbContext = dbContextProvider.GetDbContext();
            var query = from ci in dbContext.CuotaImportadors
                        join i in dbContext.Importadors on ci.ImportadorId equals i.Id
                        select new CuotaDataModel
                        {
                            Año = ci.Año,
                            Cuota = ci.Cuota,
                            Importador = i.NombreImportador
                        };

            return query.ToList();
        }

        [Obsolete("Use GetDbContextAsync() method.")]
        public List<ImportacionDataModel> GetImportacionData()
        {
            var dbContext = dbContextProvider.GetDbContext();
            var query = from ie in dbContext.ImporExports
                        join p in dbContext.Productos on ie.ProductoId equals p.Id
                        join a in dbContext.Asraes on p.AsraeId equals a.Id
                        join i in dbContext.Importadors on ie.ImportadorId equals i.Id
                        where a.Codigo_ASHRAE == "R-22"
                        group ie by new { Year = ie.FechaSolicitud.Year, i.NombreImportador } into g
                        select new ImportacionDataModel
                        {
                            PesoNeto = (decimal)(g.Sum(ie => ie.PesoNeto) / 1000),
                            Año = g.Key.Year,
                            Importador = g.Key.NombreImportador
                        };

            return query.ToList();
        }

        [Obsolete("Use GetDbContextAsync() method.")]
        // Combine the results of both queries
        public async Task<List<RepCuotasImportadores>> GetCuotasImportadoresData(int? anio)
        {
            if (anio == null)
                anio = DateTime.Now.Year - 1;

            var cuotaData = GetCuotaData();
            var importacionData = GetImportacionData();

            var combinedData = from c in cuotaData
                               join i in importacionData
                               on new { c.Año, c.Importador } equals new { i.Año, i.Importador }
                               into gj
                               from subImportacionData in gj.DefaultIfEmpty()
                               where c.Año == anio
                               select new RepCuotasImportadores()
                               {
                                   Año = c.Año,
                                   Cuota = c.Cuota,
                                   PesoNeto = subImportacionData?.PesoNeto ?? 0,
                                   Importador = c.Importador
                               };

            return combinedData.ToList();
        }

        public async Task<List<RepPesosNetosASHRAE>> GetPesosNetosASHRAE(int? anio)
        {
            var dbContext = await dbContextProvider.GetDbContextAsync();

            if (anio == null)
                anio = DateTime.Now.Year - 1;

            var query = from impexp in dbContext.ImporExports
                        join prod in dbContext.Productos on impexp.ProductoId equals prod.Id
                        join asrae in dbContext.Asraes on prod.AsraeId equals asrae.Id
                        join tipop in dbContext.TipoProductos on prod.TipoProductoId equals tipop.Id
                        where tipop.DesProducto == "HFC" && impexp.FechaEmision.Year == anio
                        group new { asrae, impexp } by new { Codigo_ASHRAE = (asrae.Descripcion == null ? "Ninguno" : asrae.Codigo_ASHRAE), tipop.DesProducto } into g
                        select new RepPesosNetosASHRAE
                        {
                            Codigo_ASHRAE = g.Key.Codigo_ASHRAE,
                            //PesoNeto = Math.Round(g.Sum(x => x.impexp.PesoNeto), 2) // Redondear a dos decimales
                            PesoNeto = Math.Round((g.Sum(x => x.impexp.PesoNeto) / 1000), 4)

                        };

            //query = query.OrderByDescending(x => x.PesoNeto).Take(10);
            query = query.OrderByDescending(x => x.PesoNeto);

            return query.ToList();
        }
    }
}
