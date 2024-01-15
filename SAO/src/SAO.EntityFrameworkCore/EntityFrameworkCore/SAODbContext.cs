using Microsoft.EntityFrameworkCore;
using SAO.Almacens;
using SAO.Asraes;
using SAO.CuotaImportadors;
using SAO.Exportadors;
using SAO.Fabricantes;
using SAO.ImporExports;
using SAO.Importadors;
using SAO.Paiss;
using SAO.Productos;
using SAO.PuertoEntradaSalidas;
using SAO.SustanciaElementals;
using SAO.TipoEnvases;
using SAO.TipoPermisos;
using SAO.TipoProductos;
using SAO.UnidadMedidas;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.BlobStoring.Database.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Gdpr;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.LanguageManagement.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TextTemplateManagement.EntityFrameworkCore;
using Volo.Saas.Editions;
using Volo.Saas.EntityFrameworkCore;
using Volo.Saas.Tenants;

namespace SAO.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityProDbContext))]
[ReplaceDbContext(typeof(ISaasDbContext))]
[ConnectionStringName("Default")]
public class SAODbContext :
    AbpDbContext<SAODbContext>,
    IIdentityProDbContext,
    ISaasDbContext
{
    public DbSet<CuotaImportador> CuotaImportadors { get; set; }
    public DbSet<ImporExport> ImporExports { get; set; }
    public DbSet<TipoPermiso> TipoPermisos { get; set; }
    public DbSet<Producto> Productos { get; set; }
    public DbSet<Asrae> Asraes { get; set; }
    public DbSet<Almacen> Almacens { get; set; }
    public DbSet<Fabricante> Fabricantes { get; set; }
    public DbSet<PuertoEntradaSalida> PuertoEntradaSalidas { get; set; }
    public DbSet<Pais> Paiss { get; set; }
    public DbSet<TipoEnvase> TipoEnvases { get; set; }
    public DbSet<UnidadMedida> UnidadMedidas { get; set; }
    public DbSet<SustanciaElemental> SustanciaElementals { get; set; }
    public DbSet<TipoProducto> TipoProductos { get; set; }
    public DbSet<Exportador> Exportadors { get; set; }
    public DbSet<Importador> Importadors { get; set; }
    /* Add DbSet properties for your Aggregate Roots / Entities here. */

    #region Entities from the modules

    /* Notice: We only implemented IIdentityProDbContext and ISaasDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityProDbContext and ISaasDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    // Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }

    // SaaS
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<Edition> Editions { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    #endregion

    public SAODbContext(DbContextOptions<SAODbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentityPro();
        builder.ConfigureOpenIddictPro();
        builder.ConfigureFeatureManagement();
        builder.ConfigureLanguageManagement();
        builder.ConfigureSaas();
        builder.ConfigureTextTemplateManagement();
        builder.ConfigureBlobStoring();
        builder.ConfigureGdpr();

        /* Configure your own tables/entities inside here */

        //builder.Entity<YourEntity>(b =>
        //{
        //    b.ToTable(SAOConsts.DbTablePrefix + "YourEntities", SAOConsts.DbSchema);
        //    b.ConfigureByConvention(); //auto configure for the base class props
        //    //...
        //});
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<TipoProducto>(b =>
{
    b.ToTable(SAOConsts.DbTablePrefix + "TipoProductos", SAOConsts.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.DesProducto).HasColumnName(nameof(TipoProducto.DesProducto)).IsRequired().HasMaxLength(TipoProductoConsts.DesProductoMaxLength);
});

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<SustanciaElemental>(b =>
{
    b.ToTable(SAOConsts.DbTablePrefix + "SustanciaElementals", SAOConsts.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.CodCas).HasColumnName(nameof(SustanciaElemental.CodCas)).IsRequired().HasMaxLength(SustanciaElementalConsts.CodCasMaxLength);
    b.HasIndex(x => x.CodCas).IsUnique();
    b.Property(x => x.DesSustancia).HasColumnName(nameof(SustanciaElemental.DesSustancia)).IsRequired().HasMaxLength(SustanciaElementalConsts.DesSustanciaMaxLength);
});

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<UnidadMedida>(b =>
{
    b.ToTable(SAOConsts.DbTablePrefix + "UnidadMedidas", SAOConsts.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.Abreviatura).HasColumnName(nameof(UnidadMedida.Abreviatura)).IsRequired().HasMaxLength(UnidadMedidaConsts.AbreviaturaMaxLength);
    b.Property(x => x.NombreUnidad).HasColumnName(nameof(UnidadMedida.NombreUnidad)).IsRequired().HasMaxLength(UnidadMedidaConsts.NombreUnidadMaxLength);
});

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<TipoEnvase>(b =>
{
    b.ToTable(SAOConsts.DbTablePrefix + "TipoEnvases", SAOConsts.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.DesEnvase).HasColumnName(nameof(TipoEnvase.DesEnvase)).IsRequired().HasMaxLength(TipoEnvaseConsts.DesEnvaseMaxLength);
});

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<Pais>(b =>
{
    b.ToTable(SAOConsts.DbTablePrefix + "Paiss", SAOConsts.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.NombrePais).HasColumnName(nameof(Pais.NombrePais)).IsRequired().HasMaxLength(PaisConsts.NombrePaisMaxLength);
});

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<PuertoEntradaSalida>(b =>
{
    b.ToTable(SAOConsts.DbTablePrefix + "PuertoEntradaSalidas", SAOConsts.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.NombrePuerto).HasColumnName(nameof(PuertoEntradaSalida.NombrePuerto)).IsRequired().HasMaxLength(PuertoEntradaSalidaConsts.NombrePuertoMaxLength);
});

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<Fabricante>(b =>
{
    b.ToTable(SAOConsts.DbTablePrefix + "Fabricantes", SAOConsts.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.NombreFabricante).HasColumnName(nameof(Fabricante.NombreFabricante)).IsRequired().HasMaxLength(FabricanteConsts.NombreFabricanteMaxLength);
});

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<Asrae>(b =>
{
    b.ToTable(SAOConsts.DbTablePrefix + "Asraes", SAOConsts.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.Codigo_ASHRAE).HasColumnName(nameof(Asrae.Codigo_ASHRAE)).IsRequired().HasMaxLength(AsraeConsts.Codigo_ASHRAEMaxLength);
    b.HasIndex(x => x.Codigo_ASHRAE).IsUnique();
    b.Property(x => x.Descripcion).HasColumnName(nameof(Asrae.Descripcion));
});

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<TipoPermiso>(b =>
{
    b.ToTable(SAOConsts.DbTablePrefix + "TipoPermisos", SAOConsts.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.Codigo).HasColumnName(nameof(TipoPermiso.Codigo)).IsRequired().HasMaxLength(TipoPermisoConsts.CodigoMaxLength);
    b.Property(x => x.Desripcion).HasColumnName(nameof(TipoPermiso.Desripcion)).HasMaxLength(TipoPermisoConsts.DesripcionMaxLength);
});

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<Almacen>(b =>
{
    b.ToTable(SAOConsts.DbTablePrefix + "Almacens", SAOConsts.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.NombreAlmacen).HasColumnName(nameof(Almacen.NombreAlmacen)).IsRequired().HasMaxLength(AlmacenConsts.NombreAlmacenMaxLength);
    b.Property(x => x.SiglaAlmacen).HasColumnName(nameof(Almacen.SiglaAlmacen)).HasMaxLength(AlmacenConsts.SiglaAlmacenMaxLength);
});

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<ImporExport>(b =>
{
    b.ToTable(SAOConsts.DbTablePrefix + "ImporExports", SAOConsts.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.NoPermiso).HasColumnName(nameof(ImporExport.NoPermiso)).IsRequired().HasMaxLength(ImporExportConsts.NoPermisoMaxLength);
    b.Property(x => x.FechaEmision).HasColumnName(nameof(ImporExport.FechaEmision));
    b.Property(x => x.FechaSolicitud).HasColumnName(nameof(ImporExport.FechaSolicitud));
    b.Property(x => x.PesoNeto).HasColumnName(nameof(ImporExport.PesoNeto));
    b.Property(x => x.PesoUnitario).HasColumnName(nameof(ImporExport.PesoUnitario));
    b.Property(x => x.CantEnvvase).HasColumnName(nameof(ImporExport.CantEnvvase));
    b.Property(x => x.NoFactura).HasColumnName(nameof(ImporExport.NoFactura)).IsRequired().HasMaxLength(ImporExportConsts.NoFacturaMaxLength);
    b.Property(x => x.Observaciones).HasColumnName(nameof(ImporExport.Observaciones)).HasMaxLength(ImporExportConsts.ObservacionesMaxLength);
    b.Property(x => x.EsRenovacion).HasColumnName(nameof(ImporExport.EsRenovacion));
    b.Property(x => x.Estado).HasColumnName(nameof(ImporExport.Estado));
    b.HasOne<Importador>().WithMany().HasForeignKey(x => x.ImportadorId).OnDelete(DeleteBehavior.NoAction);
    b.HasOne<Exportador>().WithMany().IsRequired().HasForeignKey(x => x.ExportadorId).OnDelete(DeleteBehavior.NoAction);
    b.HasOne<Producto>().WithMany().IsRequired().HasForeignKey(x => x.ProductoId).OnDelete(DeleteBehavior.NoAction);
    b.HasOne<UnidadMedida>().WithMany().IsRequired().HasForeignKey(x => x.UnidadMedidaId).OnDelete(DeleteBehavior.NoAction);
    b.HasOne<TipoEnvase>().WithMany().IsRequired().HasForeignKey(x => x.TipoEnvaseId).OnDelete(DeleteBehavior.NoAction);
    b.HasOne<PuertoEntradaSalida>().WithMany().HasForeignKey(x => x.PuertoEntradaId).OnDelete(DeleteBehavior.NoAction);
    b.HasOne<PuertoEntradaSalida>().WithMany().HasForeignKey(x => x.PuertoSalidaId).OnDelete(DeleteBehavior.NoAction);
    b.HasOne<Pais>().WithMany().HasForeignKey(x => x.PaisProcedenciaId).OnDelete(DeleteBehavior.NoAction);
    b.HasOne<Pais>().WithMany().HasForeignKey(x => x.PaisDestinoId).OnDelete(DeleteBehavior.NoAction);
    b.HasOne<Pais>().WithMany().HasForeignKey(x => x.PaisOrigenId).OnDelete(DeleteBehavior.NoAction);
    b.HasOne<Almacen>().WithMany().HasForeignKey(x => x.AlmacenId).OnDelete(DeleteBehavior.NoAction);
    b.HasOne<ImporExport>().WithMany().HasForeignKey(x => x.PermisoRenov).OnDelete(DeleteBehavior.NoAction);
    b.HasOne<TipoPermiso>().WithMany().IsRequired().HasForeignKey(x => x.PermisoDe).OnDelete(DeleteBehavior.NoAction);
});

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<Exportador>(b =>
{
    b.ToTable(SAOConsts.DbTablePrefix + "Exportadors", SAOConsts.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.NoImportador).HasColumnName(nameof(Exportador.NoImportador));
    b.Property(x => x.NombreExportador).HasColumnName(nameof(Exportador.NombreExportador)).IsRequired().HasMaxLength(ExportadorConsts.NombreExportadorMaxLength);
});

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<Importador>(b =>
{
    b.ToTable(SAOConsts.DbTablePrefix + "Importadors", SAOConsts.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.NoImportador).HasColumnName(nameof(Importador.NoImportador));
    b.Property(x => x.NoRUC).HasColumnName(nameof(Importador.NoRUC)).HasMaxLength(ImportadorConsts.NoRUCMaxLength);
    b.Property(x => x.NombreImportador).HasColumnName(nameof(Importador.NombreImportador)).IsRequired().HasMaxLength(ImportadorConsts.NombreImportadorMaxLength);
});

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<Producto>(b =>
{
    b.ToTable(SAOConsts.DbTablePrefix + "Productos", SAOConsts.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.NoProducto).HasColumnName(nameof(Producto.NoProducto));
    b.Property(x => x.NombreComercia).HasColumnName(nameof(Producto.NombreComercia)).IsRequired().HasMaxLength(ProductoConsts.NombreComerciaMaxLength);
    b.Property(x => x.Uso).HasColumnName(nameof(Producto.Uso)).HasMaxLength(ProductoConsts.UsoMaxLength);
    b.HasOne<Fabricante>().WithMany().IsRequired().HasForeignKey(x => x.FabricanteId).OnDelete(DeleteBehavior.NoAction);
    b.HasOne<Asrae>().WithMany().IsRequired().HasForeignKey(x => x.AsraeId).OnDelete(DeleteBehavior.NoAction);
    b.HasOne<TipoProducto>().WithMany().HasForeignKey(x => x.TipoProductoId).OnDelete(DeleteBehavior.NoAction);
    b.HasMany(x => x.SustanciaElementals).WithOne().HasForeignKey(x => x.ProductoId).IsRequired().OnDelete(DeleteBehavior.NoAction);
});

            builder.Entity<ProductoSustanciaElemental>(b =>
{
    b.ToTable(SAOConsts.DbTablePrefix + "ProductoSustanciaElemental" + SAOConsts.DbSchema);
    b.ConfigureByConvention();

    b.HasKey(
        x => new { x.ProductoId, x.SustanciaElementalId }
    );

    b.HasOne<Producto>().WithMany(x => x.SustanciaElementals).HasForeignKey(x => x.ProductoId).IsRequired().OnDelete(DeleteBehavior.NoAction);
    b.HasOne<SustanciaElemental>().WithMany().HasForeignKey(x => x.SustanciaElementalId).IsRequired().OnDelete(DeleteBehavior.NoAction);

    b.HasIndex(
            x => new { x.ProductoId, x.SustanciaElementalId }
    );
});
        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<CuotaImportador>(b =>
{
    b.ToTable(SAOConsts.DbTablePrefix + "CuotaImportadors", SAOConsts.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.Año).HasColumnName(nameof(CuotaImportador.Año));
    b.Property(x => x.Cuota).HasColumnName(nameof(CuotaImportador.Cuota));
    b.HasOne<Importador>().WithMany().IsRequired().HasForeignKey(x => x.ImportadorId).OnDelete(DeleteBehavior.NoAction);
    b.HasOne<Asrae>().WithMany().HasForeignKey(x => x.AsraeId).OnDelete(DeleteBehavior.NoAction);
});

        }
    }
}