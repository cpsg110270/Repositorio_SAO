using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace SAO.Asraes
{
    public class Asrae : Entity<int>
    {
        [NotNull]
        public virtual string Codigo_ASHRAE { get; set; }

        [CanBeNull]
        public virtual string? Descripcion { get; set; }

        public Asrae()
        {

        }

        public Asrae(string codigo_ASHRAE, string descripcion)
        {

            Check.NotNull(codigo_ASHRAE, nameof(codigo_ASHRAE));
            Check.Length(codigo_ASHRAE, nameof(codigo_ASHRAE), AsraeConsts.Codigo_ASHRAEMaxLength, AsraeConsts.Codigo_ASHRAEMinLength);
            Codigo_ASHRAE = codigo_ASHRAE;
            Descripcion = descripcion;
        }

    }
}