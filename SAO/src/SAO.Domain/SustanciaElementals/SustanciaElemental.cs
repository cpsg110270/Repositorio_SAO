using JetBrains.Annotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

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