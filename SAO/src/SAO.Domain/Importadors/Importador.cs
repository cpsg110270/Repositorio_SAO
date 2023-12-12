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
        public virtual int NoImportador { get; set; }

        [CanBeNull]
        public virtual string? NoRUC { get; set; }

        [NotNull]
        public virtual string NombreImportador { get; set; }

        public Importador()
        {

        }

        public Importador(Guid id, int noImportador, string noRUC, string nombreImportador)
        {

            Id = id;
            Check.Length(noRUC, nameof(noRUC), ImportadorConsts.NoRUCMaxLength, 0);
            Check.NotNull(nombreImportador, nameof(nombreImportador));
            Check.Length(nombreImportador, nameof(nombreImportador), ImportadorConsts.NombreImportadorMaxLength, ImportadorConsts.NombreImportadorMinLength);
            NoImportador = noImportador;
            NoRUC = noRUC;
            NombreImportador = nombreImportador;
        }

    }
}