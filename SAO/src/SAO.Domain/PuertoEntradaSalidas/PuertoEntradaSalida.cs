using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

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