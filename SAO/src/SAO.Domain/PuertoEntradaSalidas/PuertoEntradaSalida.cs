using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace SAO.PuertoEntradaSalidas
{
    public class PuertoEntradaSalida : Entity<int>
    {
        [NotNull]
        public virtual string NombrePuerto { get; set; }

        public PuertoEntradaSalida()
        {

        }

        public PuertoEntradaSalida(string nombrePuerto)
        {

            Check.NotNull(nombrePuerto, nameof(nombrePuerto));
            Check.Length(nombrePuerto, nameof(nombrePuerto), PuertoEntradaSalidaConsts.NombrePuertoMaxLength, PuertoEntradaSalidaConsts.NombrePuertoMinLength);
            NombrePuerto = nombrePuerto;
        }

    }
}