using JetBrains.Annotations;
using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

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