using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace SAO.Paiss
{
    public class Pais : Entity<int>
    {
        [NotNull]
        public virtual string NombrePais { get; set; }

        public Pais()
        {

        }

        public Pais(string nombrePais)
        {

            Check.NotNull(nombrePais, nameof(nombrePais));
            Check.Length(nombrePais, nameof(nombrePais), PaisConsts.NombrePaisMaxLength, PaisConsts.NombrePaisMinLength);
            NombrePais = nombrePais;
        }

    }
}