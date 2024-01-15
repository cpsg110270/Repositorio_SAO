namespace SAO.TotalImportacioness
{
    public static class TotalImportacionesConsts
    {
        private const string DefaultSorting = "{0}Anio asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "TotalImportaciones." : string.Empty);
        }

    }
}