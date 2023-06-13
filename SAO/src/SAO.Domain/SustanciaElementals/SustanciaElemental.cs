using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAO.SustanciaElementals
{
    public class SustanciaElemental : Entity<Guid>
    {
        [NotNull]
        public virtual string CodCas { get; set; }

        [NotNull]
        public virtual string DesSustancia { get; set; }

        [NotMapped]
        public string Completo { get { return CodCas + " - " + DesSustancia; } }

        public SustanciaElemental()
        {

        }

        public SustanciaElemental(Guid id, string codCas, string desSustancia)
        {

            Id = id;
            Check.NotNull(codCas, nameof(codCas));
            Check.Length(codCas, nameof(codCas), SustanciaElementalConsts.CodCasMaxLength, SustanciaElementalConsts.CodCasMinLength);
            Check.NotNull(desSustancia, nameof(desSustancia));
            Check.Length(desSustancia, nameof(desSustancia), SustanciaElementalConsts.DesSustanciaMaxLength, SustanciaElementalConsts.DesSustanciaMinLength);
            CodCas = codCas;
            DesSustancia = desSustancia;
        }

    }
}