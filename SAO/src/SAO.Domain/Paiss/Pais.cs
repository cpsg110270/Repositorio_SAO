using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

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