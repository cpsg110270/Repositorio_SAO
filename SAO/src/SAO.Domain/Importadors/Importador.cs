using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace SAO.Importadors
{
    public class Importador : Entity<Guid>
    {
        [NotNull]
        public virtual string NombreImportador { get; set; }

        public Importador()
        {

        }

        public Importador(Guid id, string nombreImportador)
        {

            Id = id;
            Check.NotNull(nombreImportador, nameof(nombreImportador));
            Check.Length(nombreImportador, nameof(nombreImportador), ImportadorConsts.NombreImportadorMaxLength, ImportadorConsts.NombreImportadorMinLength);
            NombreImportador = nombreImportador;
        }

    }
}