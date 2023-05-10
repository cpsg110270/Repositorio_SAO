namespace SAO.TipoPermisos
{
    public static class TipoPermisoConsts
    {
        private const string DefaultSorting = "{0}Codigo asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "TipoPermiso." : string.Empty);
        }

        public const int CodigoMinLength = 1;
        public const int CodigoMaxLength = 3;
        public const int DesripcionMaxLength = 20;
    }
}