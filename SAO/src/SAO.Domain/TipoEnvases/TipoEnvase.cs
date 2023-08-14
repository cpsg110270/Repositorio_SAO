using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace SAO.TipoEnvases
{
    public class TipoEnvase : Entity<int>
    {
        [NotNull]
        public virtual string DesEnvase { get; set; }

        public TipoEnvase()
        {

        }

        public TipoEnvase(string desEnvase)
        {

            Check.NotNull(desEnvase, nameof(desEnvase));
            Check.Length(desEnvase, nameof(desEnvase), TipoEnvaseConsts.DesEnvaseMaxLength, 0);
            DesEnvase = desEnvase;
        }

    }
}