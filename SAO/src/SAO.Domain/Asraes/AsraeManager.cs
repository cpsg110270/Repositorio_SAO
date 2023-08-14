using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace SAO.Asraes
{
    public class AsraeManager : DomainService
    {
        private readonly IAsraeRepository _asraeRepository;

        public AsraeManager(IAsraeRepository asraeRepository)
        {
            _asraeRepository = asraeRepository;
        }

        public async Task<Asrae> CreateAsync(
        string codigo_ASHRAE, string descripcion)
        {
            Check.NotNullOrWhiteSpace(codigo_ASHRAE, nameof(codigo_ASHRAE));
            Check.Length(codigo_ASHRAE, nameof(codigo_ASHRAE), AsraeConsts.Codigo_ASHRAEMaxLength, AsraeConsts.Codigo_ASHRAEMinLength);

            var asrae = new Asrae(

             codigo_ASHRAE, descripcion
             );

            return await _asraeRepository.InsertAsync(asrae);
        }

        public async Task<Asrae> UpdateAsync(
            int id,
            string codigo_ASHRAE, string descripcion
        )
        {
            Check.NotNullOrWhiteSpace(codigo_ASHRAE, nameof(codigo_ASHRAE));
            Check.Length(codigo_ASHRAE, nameof(codigo_ASHRAE), AsraeConsts.Codigo_ASHRAEMaxLength, AsraeConsts.Codigo_ASHRAEMinLength);

            var asrae = await _asraeRepository.GetAsync(id);

            asrae.Codigo_ASHRAE = codigo_ASHRAE;
            asrae.Descripcion = descripcion;

            return await _asraeRepository.UpdateAsync(asrae);
        }

    }
}