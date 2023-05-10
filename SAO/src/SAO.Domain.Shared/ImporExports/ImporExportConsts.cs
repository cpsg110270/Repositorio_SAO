namespace SAO.ImporExports
{
    public static class ImporExportConsts
    {
        private const string DefaultSorting = "{0}NoPermiso asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "ImporExport." : string.Empty);
        }

        public const int NoPermisoMaxLength = 8;
        public const int NoFacturaMaxLength = 12;
        public const int ObservacionesMaxLength = 300;
    }
}