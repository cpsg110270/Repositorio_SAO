namespace SAO.UnidadMedidas
{
    public static class UnidadMedidaConsts
    {
        private const string DefaultSorting = "{0}Abreviatura asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "UnidadMedida." : string.Empty);
        }

        public const int AbreviaturaMinLength = 1;
        public const int AbreviaturaMaxLength = 4;
        public const int NombreUnidadMaxLength = 50;
    }
}