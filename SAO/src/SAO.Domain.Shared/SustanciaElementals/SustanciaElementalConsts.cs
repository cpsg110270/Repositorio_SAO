namespace SAO.SustanciaElementals
{
    public static class SustanciaElementalConsts
    {
        private const string DefaultSorting = "{0}CodCas asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "SustanciaElemental." : string.Empty);
        }

        public const int CodCasMinLength = 1;
        public const int CodCasMaxLength = 15;
        public const int DesSustanciaMinLength = 1;
        public const int DesSustanciaMaxLength = 50;
    }
}