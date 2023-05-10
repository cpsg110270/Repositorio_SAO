using SAO.ImporExports;
using SAO.TipoPermisos;
using SAO.Productos;
using SAO.Asraes;
using SAO.Almacens;
using SAO.Fabricantes;
using SAO.PuertoEntradaSalidas;
using SAO.Paiss;
using SAO.TipoEnvases;
using SAO.UnidadMedidas;
using SAO.SustanciaElementals;
using SAO.TipoProductos;
using SAO.Exportadors;
using SAO.Importadors;
using System;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Uow;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.LanguageManagement.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TextTemplateManagement.EntityFrameworkCore;
using Volo.Saas.EntityFrameworkCore;
using Volo.Abp.BlobStoring.Database.EntityFrameworkCore;
using Volo.Abp.Gdpr;
using Volo.Abp.OpenIddict.EntityFrameworkCore;

namespace SAO.EntityFrameworkCore;

[DependsOn(
    typeof(SAODomainModule),
    typeof(AbpIdentityProEntityFrameworkCoreModule),
    typeof(AbpOpenIddictProEntityFrameworkCoreModule),
    typeof(AbpPermissionManagementEntityFrameworkCoreModule),
    typeof(AbpSettingManagementEntityFrameworkCoreModule),
    typeof(AbpEntityFrameworkCoreSqlServerModule),
    typeof(AbpBackgroundJobsEntityFrameworkCoreModule),
    typeof(AbpAuditLoggingEntityFrameworkCoreModule),
    typeof(AbpFeatureManagementEntityFrameworkCoreModule),
    typeof(LanguageManagementEntityFrameworkCoreModule),
    typeof(SaasEntityFrameworkCoreModule),
    typeof(TextTemplateManagementEntityFrameworkCoreModule),
    typeof(AbpGdprEntityFrameworkCoreModule),
    typeof(BlobStoringDatabaseEntityFrameworkCoreModule)
    )]
public class SAOEntityFrameworkCoreModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        SAOEfCoreEntityExtensionMappings.Configure();
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<SAODbContext>(options =>
        {
            /* Remove "includeAllEntities: true" to create
             * default repositories only for aggregate roots */
            options.AddDefaultRepositories(includeAllEntities: true);
            options.AddRepository<Importador, Importadors.EfCoreImportadorRepository>();

            options.AddRepository<Exportador, Exportadors.EfCoreExportadorRepository>();

            options.AddRepository<TipoProducto, TipoProductos.EfCoreTipoProductoRepository>();

            options.AddRepository<SustanciaElemental, SustanciaElementals.EfCoreSustanciaElementalRepository>();

            options.AddRepository<UnidadMedida, UnidadMedidas.EfCoreUnidadMedidaRepository>();

            options.AddRepository<TipoEnvase, TipoEnvases.EfCoreTipoEnvaseRepository>();

            options.AddRepository<Pais, Paiss.EfCorePaisRepository>();

            options.AddRepository<PuertoEntradaSalida, PuertoEntradaSalidas.EfCorePuertoEntradaSalidaRepository>();

            options.AddRepository<Fabricante, Fabricantes.EfCoreFabricanteRepository>();

            options.AddRepository<Almacen, Almacens.EfCoreAlmacenRepository>();

            options.AddRepository<Asrae, Asraes.EfCoreAsraeRepository>();

            options.AddRepository<Producto, Productos.EfCoreProductoRepository>();

            options.AddRepository<TipoPermiso, TipoPermisos.EfCoreTipoPermisoRepository>();

            options.AddRepository<ImporExport, ImporExports.EfCoreImporExportRepository>();

        });

        Configure<AbpDbContextOptions>(options =>
        {
            /* The main point to change your DBMS.
             * See also SAODbContextFactory for EF Core tooling. */
            options.UseSqlServer();
        });

    }
}