using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace SAO.TipoEnvases
{
    public class TipoEnvaseManager : DomainService
    {
        private readonly ITipoEnvaseRepository _tipoEnvaseRepository;

        public TipoEnvaseManager(ITipoEnvaseRepository tipoEnvaseRepository)
        {
            _tipoEnvaseRepository = tipoEnvaseRepository;
        }

        public async Task<TipoEnvase> CreateAsync(
        string desEnvase)
        {
            Check.NotNullOrWhiteSpace(desEnvase, nameof(desEnvase));
            Check.Length(desEnvase, nameof(desEnvase), TipoEnvaseConsts.DesEnvaseMaxLength);

            var tipoEnvase = new TipoEnvase(

             desEnvase
             );

            return await _tipoEnvaseRepository.InsertAsync(tipoEnvase);
        }

        public async Task<TipoEnvase> UpdateAsync(
            int id,
            string desEnvase
        )
        {
            Check.NotNullOrWhiteSpace(desEnvase, nameof(desEnvase));
            Check.Length(desEnvase, nameof(desEnvase), TipoEnvaseConsts.DesEnvaseMaxLength);

            var tipoEnvase = await _tipoEnvaseRepository.GetAsync(id);

            tipoEnvase.DesEnvase = desEnvase;

            return await _tipoEnvaseRepository.UpdateAsync(tipoEnvase);
        }

    }
}