using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace SAO.Fabricantes
{
    public class Fabricante : Entity<Guid>
    {
        [NotNull]
        public virtual string NombreFabricante { get; set; }

        public Fabricante()
        {

        }

        public Fabricante(Guid id, string nombreFabricante)
        {

            Id = id;
            Check.NotNull(nombreFabricante, nameof(nombreFabricante));
            Check.Length(nombreFabricante, nameof(nombreFabricante), FabricanteConsts.NombreFabricanteMaxLength, FabricanteConsts.NombreFabricanteMinLength);
            NombreFabricante = nombreFabricante;
        }

    }
}