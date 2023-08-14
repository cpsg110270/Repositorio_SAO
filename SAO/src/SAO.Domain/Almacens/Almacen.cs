using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace SAO.Almacens
{
    public class Almacen : Entity<int>
    {
        [NotNull]
        public virtual string NombreAlmacen { get; set; }

        [CanBeNull]
        public virtual string? SiglaAlmacen { get; set; }

        public Almacen()
        {

        }

        public Almacen(string nombreAlmacen, string siglaAlmacen)
        {

            Check.NotNull(nombreAlmacen, nameof(nombreAlmacen));
            Check.Length(nombreAlmacen, nameof(nombreAlmacen), AlmacenConsts.NombreAlmacenMaxLength, 0);
            Check.Length(siglaAlmacen, nameof(siglaAlmacen), AlmacenConsts.SiglaAlmacenMaxLength, 0);
            NombreAlmacen = nombreAlmacen;
            SiglaAlmacen = siglaAlmacen;
        }

    }
}