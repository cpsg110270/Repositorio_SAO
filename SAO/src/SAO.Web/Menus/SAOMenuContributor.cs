using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using SAO.Localization;
using SAO.Permissions;
using Volo.Abp.AuditLogging.Web.Navigation;
using Volo.Abp.Identity.Web.Navigation;
using Volo.Abp.LanguageManagement.Navigation;
using Volo.Abp.SettingManagement.Web.Navigation;
using Volo.Abp.TextTemplateManagement.Web.Navigation;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.OpenIddict.Pro.Web.Menus;
using Volo.Abp.UI.Navigation;
using Volo.Saas.Host.Navigation;

namespace SAO.Web.Menus;

public class SAOMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private static Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var l = context.GetLocalizer<SAOResource>();

        //Home
        context.Menu.AddItem(
            new ApplicationMenuItem(
                SAOMenus.Home,
                l["Menu:Home"],
                "~/",
                icon: "fa fa-home",
                order: 1

            )
        );

        //HostDashboard
        context.Menu.AddItem(
            new ApplicationMenuItem(
                SAOMenus.HostDashboard,
                l["Menu:Dashboard"],
                "~/HostDashboard",
                icon: "fa fa-line-chart",
                order: 2
            ).RequirePermissions(SAOPermissions.Dashboard.Host)
        );

        //TenantDashboard
        context.Menu.AddItem(
            new ApplicationMenuItem(
                SAOMenus.TenantDashboard,
                l["Menu:Dashboard"],
                "~/Dashboard",
                icon: "fa fa-line-chart",
                order: 2
            ).RequirePermissions(SAOPermissions.Dashboard.Tenant)
        );

        context.Menu.SetSubItemOrder(SaasHostMenuNames.GroupName, 3);

        //Administration
        var administration = context.Menu.GetAdministration();
        administration.Order = 5;

        //Administration->Identity
        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 1);

        //Administration->OpenIddict
        administration.SetSubItemOrder(OpenIddictProMenus.GroupName, 2);

        //Administration->Language Management
        administration.SetSubItemOrder(LanguageManagementMenuNames.GroupName, 3);

        //Administration->Text Template Management
        administration.SetSubItemOrder(TextTemplateManagementMainMenuNames.GroupName, 4);

        //Administration->Audit Logs
        administration.SetSubItemOrder(AbpAuditLoggingMainMenuNames.GroupName, 5);

        //Administration->Settings
        administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 6);

        #region Catalogos Generales
        //Catalogos Generales

        var catalogosGenerales = new ApplicationMenuItem(
        SAOMenus.CatalagosGenerales,
        l["Menu:CatalogosGenerales"],
        url: "/CatalogosGenerales",
        icon: "fa fa-folder",
        requiredPermissionName: SAOPermissions.Modulos.CatalagosGenerales);

        context.Menu.AddItem(catalogosGenerales);

        catalogosGenerales.AddItem(
            new ApplicationMenuItem(
                SAOMenus.Importadors,
                l["Menu:Importadors"],
                url: "/Importadors",
                icon: "fa fa-file-alt",
                requiredPermissionName: SAOPermissions.Importadors.Default)
        );

        catalogosGenerales.AddItem(
             new ApplicationMenuItem(
                 SAOMenus.Exportadors,
                 l["Menu:Exportadors"],
                 url: "/Exportadors",
                 icon: "fa fa-file-alt",
                 requiredPermissionName: SAOPermissions.Exportadors.Default)
         );

        catalogosGenerales.AddItem(
            new ApplicationMenuItem(
                SAOMenus.TipoProductos,
                l["Menu:TipoProductos"],
                url: "/TipoProductos",
                icon: "fa fa-file-alt",
                requiredPermissionName: SAOPermissions.TipoProductos.Default)
        );

        catalogosGenerales.AddItem(
            new ApplicationMenuItem(
                SAOMenus.SustanciaElementals,
                l["Menu:SustanciaElementals"],
                url: "/SustanciaElementals",
                icon: "fa fa-file-alt",
                requiredPermissionName: SAOPermissions.SustanciaElementals.Default)
        );

        catalogosGenerales.AddItem(
           new ApplicationMenuItem(
                SAOMenus.UnidadMedidas,
                l["Menu:UnidadMedidas"],
                url: "/UnidadMedidas",
                icon: "fa fa-file-alt",
                requiredPermissionName: SAOPermissions.UnidadMedidas.Default)
        );

        catalogosGenerales.AddItem(
            new ApplicationMenuItem(
                SAOMenus.TipoEnvases,
                l["Menu:TipoEnvases"],
                url: "/TipoEnvases",
                icon: "fa fa-file-alt",
                requiredPermissionName: SAOPermissions.TipoEnvases.Default)
        );

        catalogosGenerales.AddItem(
           new ApplicationMenuItem(
                SAOMenus.Paiss,
                l["Menu:Paiss"],
                url: "/Paiss",
                icon: "fa fa-file-alt",
                requiredPermissionName: SAOPermissions.Paiss.Default)
        );

        catalogosGenerales.AddItem(
            new ApplicationMenuItem(
                SAOMenus.PuertoEntradaSalidas,
                l["Menu:PuertoEntradaSalidas"],
                url: "/PuertoEntradaSalidas",
                icon: "fa fa-file-alt",
                requiredPermissionName: SAOPermissions.PuertoEntradaSalidas.Default)
        );

        catalogosGenerales.AddItem(
            new ApplicationMenuItem(
                SAOMenus.Fabricantes,
                l["Menu:Fabricantes"],
                url: "/Fabricantes",
                icon: "fa fa-file-alt",
                requiredPermissionName: SAOPermissions.Fabricantes.Default)
        );

        catalogosGenerales.AddItem(
           new ApplicationMenuItem(
                SAOMenus.Almacens,
                l["Menu:Almacens"],
                url: "/Almacens",
                icon: "fa fa-file-alt",
                requiredPermissionName: SAOPermissions.Almacens.Default)
        );

        catalogosGenerales.AddItem(
          new ApplicationMenuItem(
               SAOMenus.Asraes,
               l["Menu:Asraes"],
               url: "/Asraes",
               icon: "fa fa-file-alt",
               requiredPermissionName: SAOPermissions.Asraes.Default)
       );

        catalogosGenerales.AddItem(
            new ApplicationMenuItem(
                SAOMenus.TipoPermisos,
                l["Menu:TipoPermisos"],
                url: "/TipoPermisos",
                icon: "fa fa-file-alt",
                requiredPermissionName: SAOPermissions.TipoPermisos.Default)
        );

        #endregion

        #region Producto
        //Catalogos Producto

        var catalogosProductos = new ApplicationMenuItem(
        SAOMenus.CatalogosProductos,
        l["Menu:Productos"],
        url: "/Productos",
        icon: "fa fa-folder",
        requiredPermissionName: SAOPermissions.Modulos.CatalogosProductos);

        context.Menu.AddItem(catalogosProductos);

        catalogosProductos.AddItem(
            new ApplicationMenuItem(
                SAOMenus.Productos,
                l["Menu:Productos"],
                url: "/Productos",
                icon: "fa fa-file-alt",
                requiredPermissionName: SAOPermissions.Productos.Default)
        );
        #endregion

        context.Menu.AddItem(
            new ApplicationMenuItem(
                SAOMenus.ImporExports,
                l["Menu:ImporExports"],
                url: "/ImporExports",
                icon: "fa fa-file-alt",
                requiredPermissionName: SAOPermissions.ImporExports.Default)
        );


        var reportes  = new ApplicationMenuItem(
        SAOMenus.Reportes,
        l["Menu:Reportes"],
        url: "/Reportes",
        icon: "fa fa-bar-chart",
        requiredPermissionName: SAOPermissions.Modulos.Reportes);

        context.Menu.AddItem(reportes);

        reportes.AddItem(
            new ApplicationMenuItem(
                SAOMenus.Reportes,
                l["Menu:Lista Importación/Exportación"],
                url: "/ReportViewer",
                icon: "fa fa-bar-chart",
                requiredPermissionName: SAOPermissions.Reportes)
        ) ;

        //context.Menu.AddItem(
        //    new ApplicationMenuItem(
        //        SAOMenus.CuotaImportadors,
        //        l["Menu:CuotaImportadors"],
        //        url: "/CuotaImportadors",
        //        icon: "fa fa-file-alt",
        //        requiredPermissionName: SAOPermissions.CuotaImportadors.Default)
        //);

        //context.Menu.AddItem(
        //    new ApplicationMenuItem(
        //        SAOMenus.Reportes,
        //        l["Menu:Reportes"],
        //        url: "/ReportViewer",
        //        icon: "fa fa-bar-chart",
        //        requiredPermissionName: SAOPermissions.Reportes)
        //);

        return Task.CompletedTask;
    }
}