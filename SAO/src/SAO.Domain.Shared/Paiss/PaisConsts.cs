namespace SAO.Paiss
{
    public static class PaisConsts
    {
        private const string DefaultSorting = "{0}NombrePais asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Pais." : string.Empty);
        }

        public const int NombrePaisMinLength = 1;
        public const int NombrePaisMaxLength = 50;
    }
}