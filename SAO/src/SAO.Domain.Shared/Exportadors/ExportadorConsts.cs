namespace SAO.Exportadors
{
    public static class ExportadorConsts
    {
        private const string DefaultSorting = "{0}NoImportador asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Exportador." : string.Empty);
        }

        public const int NombreExportadorMinLength = 1;
        public const int NombreExportadorMaxLength = 250;
    }
}