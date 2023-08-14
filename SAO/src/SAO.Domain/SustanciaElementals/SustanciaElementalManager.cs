using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace SAO.SustanciaElementals
{
    public class SustanciaElementalManager : DomainService
    {
        private readonly ISustanciaElementalRepository _sustanciaElementalRepository;

        public SustanciaElementalManager(ISustanciaElementalRepository sustanciaElementalRepository)
        {
            _sustanciaElementalRepository = sustanciaElementalRepository;
        }

        public async Task<SustanciaElemental> CreateAsync(
        string codCas, string desSustancia)
        {
            Check.NotNullOrWhiteSpace(codCas, nameof(codCas));
            Check.Length(codCas, nameof(codCas), SustanciaElementalConsts.CodCasMaxLength, SustanciaElementalConsts.CodCasMinLength);
            Check.NotNullOrWhiteSpace(desSustancia, nameof(desSustancia));
            Check.Length(desSustancia, nameof(desSustancia), SustanciaElementalConsts.DesSustanciaMaxLength, SustanciaElementalConsts.DesSustanciaMinLength);

            var sustanciaElemental = new SustanciaElemental(
             GuidGenerator.Create(),
             codCas, desSustancia
             );

            return await _sustanciaElementalRepository.InsertAsync(sustanciaElemental);
        }

        public async Task<SustanciaElemental> UpdateAsync(
            Guid id,
            string codCas, string desSustancia
        )
        {
            Check.NotNullOrWhiteSpace(codCas, nameof(codCas));
            Check.Length(codCas, nameof(codCas), SustanciaElementalConsts.CodCasMaxLength, SustanciaElementalConsts.CodCasMinLength);
            Check.NotNullOrWhiteSpace(desSustancia, nameof(desSustancia));
            Check.Length(desSustancia, nameof(desSustancia), SustanciaElementalConsts.DesSustanciaMaxLength, SustanciaElementalConsts.DesSustanciaMinLength);

            var sustanciaElemental = await _sustanciaElementalRepository.GetAsync(id);

            sustanciaElemental.CodCas = codCas;
            sustanciaElemental.DesSustancia = desSustancia;

            return await _sustanciaElementalRepository.UpdateAsync(sustanciaElemental);
        }

    }
}