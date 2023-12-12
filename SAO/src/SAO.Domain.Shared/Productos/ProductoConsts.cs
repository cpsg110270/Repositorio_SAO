namespace SAO.Productos
{
    public static class ProductoConsts
    {
        private const string DefaultSorting = "{0}NoProducto asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Producto." : string.Empty);
        }

        public const int NombreComerciaMinLength = 1;
        public const int NombreComerciaMaxLength = 90;
        public const int UsoMaxLength = 200;
    }
}