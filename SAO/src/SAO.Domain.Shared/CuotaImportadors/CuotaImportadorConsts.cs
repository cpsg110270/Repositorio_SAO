namespace SAO.CuotaImportadors
{
    public static class CuotaImportadorConsts
    {
        private const string DefaultSorting = "{0}Año asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "CuotaImportador." : string.Empty);
        }

    }
}