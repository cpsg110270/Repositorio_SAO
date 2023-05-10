namespace SAO.PuertoEntradaSalidas
{
    public static class PuertoEntradaSalidaConsts
    {
        private const string DefaultSorting = "{0}NombrePuerto asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "PuertoEntradaSalida." : string.Empty);
        }

        public const int NombrePuertoMinLength = 1;
        public const int NombrePuertoMaxLength = 50;
    }
}