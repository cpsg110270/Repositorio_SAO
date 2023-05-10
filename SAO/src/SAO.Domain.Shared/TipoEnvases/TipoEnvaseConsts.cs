namespace SAO.TipoEnvases
{
    public static class TipoEnvaseConsts
    {
        private const string DefaultSorting = "{0}DesEnvase asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "TipoEnvase." : string.Empty);
        }

        public const int DesEnvaseMaxLength = 20;
    }
}