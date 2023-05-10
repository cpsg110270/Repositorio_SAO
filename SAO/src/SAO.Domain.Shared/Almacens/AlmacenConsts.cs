namespace SAO.Almacens
{
    public static class AlmacenConsts
    {
        private const string DefaultSorting = "{0}NombreAlmacen asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Almacen." : string.Empty);
        }

        public const int NombreAlmacenMaxLength = 200;
        public const int SiglaAlmacenMaxLength = 20;
    }
}