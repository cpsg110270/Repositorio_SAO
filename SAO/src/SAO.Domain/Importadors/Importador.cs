using JetBrains.Annotations;
using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

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