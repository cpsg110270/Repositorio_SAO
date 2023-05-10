using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

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