namespace SAO.Permissions;

public static class SAOPermissions
{
    public const string GroupName = "SAO";

    public static class Dashboard
    {
        public const string DashboardGroup = GroupName + ".Dashboard";
        public const string Host = DashboardGroup + ".Host";
        public const string Tenant = DashboardGroup + ".Tenant";
    }

    //Add your own permission names. Example:
    //public const string MyPermission1 = GroupName + ".MyPermission1";

    public const string Reportes = GroupName + ".Reportes";

    public static class Modulos
    {
        public const string ModulosGroup = GroupName + ".Modulos";
        public const string CatalagosGenerales = ModulosGroup + ".CatalagosGenerales";
        public const string CatalogosProductos = ModulosGroup + ".CatalogosProductos";
        public const string CatalogosImportadors = ModulosGroup + ".CatalogosImportadors";
        public const string Reportes = ModulosGroup + ".Reportes";
    }

    public static class Importadors
    {
        public const string Default = GroupName + ".Importadors";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class Exportadors
    {
        public const string Default = GroupName + ".Exportadors";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class TipoProductos
    {
        public const string Default = GroupName + ".TipoProductos";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class SustanciaElementals
    {
        public const string Default = GroupName + ".SustanciaElementals";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class UnidadMedidas
    {
        public const string Default = GroupName + ".UnidadMedidas";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class TipoEnvases
    {
        public const string Default = GroupName + ".TipoEnvases";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class Paiss
    {
        public const string Default = GroupName + ".Paiss";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class PuertoEntradaSalidas
    {
        public const string Default = GroupName + ".PuertoEntradaSalidas";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class Fabricantes
    {
        public const string Default = GroupName + ".Fabricantes";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class Almacens
    {
        public const string Default = GroupName + ".Almacens";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class Asraes
    {
        public const string Default = GroupName + ".Asraes";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class Productos
    {
        public const string Default = GroupName + ".Productos";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class TipoPermisos
    {
        public const string Default = GroupName + ".TipoPermisos";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class ImporExports
    {
        public const string Default = GroupName + ".ImporExports";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class CuotaImportadors
    {
        public const string Default = GroupName + ".CuotaImportadors";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class TotalImportacioness
    {
        public const string Default = GroupName + ".TotalImportacioness";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }
}