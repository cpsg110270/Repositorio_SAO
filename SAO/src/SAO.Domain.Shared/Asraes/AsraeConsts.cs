namespace SAO.Asraes
{
    public static class AsraeConsts
    {
        private const string DefaultSorting = "{0}Codigo_ASHRAE asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Asrae." : string.Empty);
        }

        public const int Codigo_ASHRAEMinLength = 1;
        public const int Codigo_ASHRAEMaxLength = 12;
    }
}