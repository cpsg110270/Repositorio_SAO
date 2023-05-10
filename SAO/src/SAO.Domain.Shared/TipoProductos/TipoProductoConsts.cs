namespace SAO.TipoProductos
{
    public static class TipoProductoConsts
    {
        private const string DefaultSorting = "{0}DesProducto asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "TipoProducto." : string.Empty);
        }

        public const int DesProductoMinLength = 1;
        public const int DesProductoMaxLength = 20;
    }
}