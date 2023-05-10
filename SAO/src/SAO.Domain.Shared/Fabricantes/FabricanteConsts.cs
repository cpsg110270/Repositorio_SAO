namespace SAO.Fabricantes
{
    public static class FabricanteConsts
    {
        private const string DefaultSorting = "{0}NombreFabricante asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Fabricante." : string.Empty);
        }

        public const int NombreFabricanteMinLength = 1;
        public const int NombreFabricanteMaxLength = 100;
    }
}