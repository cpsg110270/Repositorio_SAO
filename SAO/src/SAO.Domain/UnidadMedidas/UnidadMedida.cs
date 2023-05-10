using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace SAO.UnidadMedidas
{
    public class UnidadMedida : Entity<int>
    {
        [NotNull]
        public virtual string Abreviatura { get; set; }

        [NotNull]
        public virtual string NombreUnidad { get; set; }

        public UnidadMedida()
        {

        }

        public UnidadMedida(string abreviatura, string nombreUnidad)
        {

            Check.NotNull(abreviatura, nameof(abreviatura));
            Check.Length(abreviatura, nameof(abreviatura), UnidadMedidaConsts.AbreviaturaMaxLength, UnidadMedidaConsts.AbreviaturaMinLength);
            Check.NotNull(nombreUnidad, nameof(nombreUnidad));
            Check.Length(nombreUnidad, nameof(nombreUnidad), UnidadMedidaConsts.NombreUnidadMaxLength, 0);
            Abreviatura = abreviatura;
            NombreUnidad = nombreUnidad;
        }

    }
}