namespace SAO.Importadors
{
    public static class ImportadorConsts
    {
        private const string DefaultSorting = "{0}NombreImportador asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Importador." : string.Empty);
        }

        public const int NombreImportadorMinLength = 1;
        public const int NombreImportadorMaxLength = 250;
    }
}