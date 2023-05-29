using SAO.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace SAO.Permissions;

public class SAOPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(SAOPermissions.GroupName);

        myGroup.AddPermission(SAOPermissions.Dashboard.Host, L("Permission:Dashboard"), MultiTenancySides.Host);
        myGroup.AddPermission(SAOPermissions.Dashboard.Tenant, L("Permission:Dashboard"), MultiTenancySides.Tenant);

        //Define your own permissions here. Example:
        //myGroup.AddPermission(SAOPermissions.MyPermission1, L("Permission:MyPermission1"));

        myGroup.AddPermission(SAOPermissions.Reportes, L("Permission:Reportes"));

        var modulosPermission = myGroup.AddPermission(SAOPermissions.Modulos.ModulosGroup, L("Permission:Modulos"));
        modulosPermission.AddChild(SAOPermissions.Modulos.CatalagosGenerales, L("Permission:CatalagosGenerales"));
        modulosPermission.AddChild(SAOPermissions.Modulos.CatalogosProductos, L("Permission:CatalogosProductos"));

        var importadorPermission = myGroup.AddPermission(SAOPermissions.Importadors.Default, L("Permission:Importadors"));
        importadorPermission.AddChild(SAOPermissions.Importadors.Create, L("Permission:Create"));
        importadorPermission.AddChild(SAOPermissions.Importadors.Edit, L("Permission:Edit"));
        importadorPermission.AddChild(SAOPermissions.Importadors.Delete, L("Permission:Delete"));

        var exportadorPermission = myGroup.AddPermission(SAOPermissions.Exportadors.Default, L("Permission:Exportadors"));
        exportadorPermission.AddChild(SAOPermissions.Exportadors.Create, L("Permission:Create"));
        exportadorPermission.AddChild(SAOPermissions.Exportadors.Edit, L("Permission:Edit"));
        exportadorPermission.AddChild(SAOPermissions.Exportadors.Delete, L("Permission:Delete"));

        var tipoProductoPermission = myGroup.AddPermission(SAOPermissions.TipoProductos.Default, L("Permission:TipoProductos"));
        tipoProductoPermission.AddChild(SAOPermissions.TipoProductos.Create, L("Permission:Create"));
        tipoProductoPermission.AddChild(SAOPermissions.TipoProductos.Edit, L("Permission:Edit"));
        tipoProductoPermission.AddChild(SAOPermissions.TipoProductos.Delete, L("Permission:Delete"));

        var sustanciaElementalPermission = myGroup.AddPermission(SAOPermissions.SustanciaElementals.Default, L("Permission:SustanciaElementals"));
        sustanciaElementalPermission.AddChild(SAOPermissions.SustanciaElementals.Create, L("Permission:Create"));
        sustanciaElementalPermission.AddChild(SAOPermissions.SustanciaElementals.Edit, L("Permission:Edit"));
        sustanciaElementalPermission.AddChild(SAOPermissions.SustanciaElementals.Delete, L("Permission:Delete"));

        var unidadMedidaPermission = myGroup.AddPermission(SAOPermissions.UnidadMedidas.Default, L("Permission:UnidadMedidas"));
        unidadMedidaPermission.AddChild(SAOPermissions.UnidadMedidas.Create, L("Permission:Create"));
        unidadMedidaPermission.AddChild(SAOPermissions.UnidadMedidas.Edit, L("Permission:Edit"));
        unidadMedidaPermission.AddChild(SAOPermissions.UnidadMedidas.Delete, L("Permission:Delete"));

        var tipoEnvasePermission = myGroup.AddPermission(SAOPermissions.TipoEnvases.Default, L("Permission:TipoEnvases"));
        tipoEnvasePermission.AddChild(SAOPermissions.TipoEnvases.Create, L("Permission:Create"));
        tipoEnvasePermission.AddChild(SAOPermissions.TipoEnvases.Edit, L("Permission:Edit"));
        tipoEnvasePermission.AddChild(SAOPermissions.TipoEnvases.Delete, L("Permission:Delete"));

        var paisPermission = myGroup.AddPermission(SAOPermissions.Paiss.Default, L("Permission:Paiss"));
        paisPermission.AddChild(SAOPermissions.Paiss.Create, L("Permission:Create"));
        paisPermission.AddChild(SAOPermissions.Paiss.Edit, L("Permission:Edit"));
        paisPermission.AddChild(SAOPermissions.Paiss.Delete, L("Permission:Delete"));

        var puertoEntradaSalidaPermission = myGroup.AddPermission(SAOPermissions.PuertoEntradaSalidas.Default, L("Permission:PuertoEntradaSalidas"));
        puertoEntradaSalidaPermission.AddChild(SAOPermissions.PuertoEntradaSalidas.Create, L("Permission:Create"));
        puertoEntradaSalidaPermission.AddChild(SAOPermissions.PuertoEntradaSalidas.Edit, L("Permission:Edit"));
        puertoEntradaSalidaPermission.AddChild(SAOPermissions.PuertoEntradaSalidas.Delete, L("Permission:Delete"));

        var fabricantePermission = myGroup.AddPermission(SAOPermissions.Fabricantes.Default, L("Permission:Fabricantes"));
        fabricantePermission.AddChild(SAOPermissions.Fabricantes.Create, L("Permission:Create"));
        fabricantePermission.AddChild(SAOPermissions.Fabricantes.Edit, L("Permission:Edit"));
        fabricantePermission.AddChild(SAOPermissions.Fabricantes.Delete, L("Permission:Delete"));

        var almacenPermission = myGroup.AddPermission(SAOPermissions.Almacens.Default, L("Permission:Almacens"));
        almacenPermission.AddChild(SAOPermissions.Almacens.Create, L("Permission:Create"));
        almacenPermission.AddChild(SAOPermissions.Almacens.Edit, L("Permission:Edit"));
        almacenPermission.AddChild(SAOPermissions.Almacens.Delete, L("Permission:Delete"));

        var asraePermission = myGroup.AddPermission(SAOPermissions.Asraes.Default, L("Permission:Asraes"));
        asraePermission.AddChild(SAOPermissions.Asraes.Create, L("Permission:Create"));
        asraePermission.AddChild(SAOPermissions.Asraes.Edit, L("Permission:Edit"));
        asraePermission.AddChild(SAOPermissions.Asraes.Delete, L("Permission:Delete"));

        var productoPermission = myGroup.AddPermission(SAOPermissions.Productos.Default, L("Permission:Productos"));
        productoPermission.AddChild(SAOPermissions.Productos.Create, L("Permission:Create"));
        productoPermission.AddChild(SAOPermissions.Productos.Edit, L("Permission:Edit"));
        productoPermission.AddChild(SAOPermissions.Productos.Delete, L("Permission:Delete"));

        var tipoPermisoPermission = myGroup.AddPermission(SAOPermissions.TipoPermisos.Default, L("Permission:TipoPermisos"));
        tipoPermisoPermission.AddChild(SAOPermissions.TipoPermisos.Create, L("Permission:Create"));
        tipoPermisoPermission.AddChild(SAOPermissions.TipoPermisos.Edit, L("Permission:Edit"));
        tipoPermisoPermission.AddChild(SAOPermissions.TipoPermisos.Delete, L("Permission:Delete"));

        var imporExportPermission = myGroup.AddPermission(SAOPermissions.ImporExports.Default, L("Permission:ImporExports"));
        imporExportPermission.AddChild(SAOPermissions.ImporExports.Create, L("Permission:Create"));
        imporExportPermission.AddChild(SAOPermissions.ImporExports.Edit, L("Permission:Edit"));
        imporExportPermission.AddChild(SAOPermissions.ImporExports.Delete, L("Permission:Delete"));

        var cuotaImportadorPermission = myGroup.AddPermission(SAOPermissions.CuotaImportadors.Default, L("Permission:CuotaImportadors"));
        cuotaImportadorPermission.AddChild(SAOPermissions.CuotaImportadors.Create, L("Permission:Create"));
        cuotaImportadorPermission.AddChild(SAOPermissions.CuotaImportadors.Edit, L("Permission:Edit"));
        cuotaImportadorPermission.AddChild(SAOPermissions.CuotaImportadors.Delete, L("Permission:Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<SAOResource>(name);
    }
}