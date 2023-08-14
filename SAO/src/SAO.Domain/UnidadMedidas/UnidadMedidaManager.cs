using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace SAO.UnidadMedidas
{
    public class UnidadMedidaManager : DomainService
    {
        private readonly IUnidadMedidaRepository _unidadMedidaRepository;

        public UnidadMedidaManager(IUnidadMedidaRepository unidadMedidaRepository)
        {
            _unidadMedidaRepository = unidadMedidaRepository;
        }

        public async Task<UnidadMedida> CreateAsync(
        string abreviatura, string nombreUnidad)
        {
            Check.NotNullOrWhiteSpace(abreviatura, nameof(abreviatura));
            Check.Length(abreviatura, nameof(abreviatura), UnidadMedidaConsts.AbreviaturaMaxLength, UnidadMedidaConsts.AbreviaturaMinLength);
            Check.NotNullOrWhiteSpace(nombreUnidad, nameof(nombreUnidad));
            Check.Length(nombreUnidad, nameof(nombreUnidad), UnidadMedidaConsts.NombreUnidadMaxLength);

            var unidadMedida = new UnidadMedida(

             abreviatura, nombreUnidad
             );

            return await _unidadMedidaRepository.InsertAsync(unidadMedida);
        }

        public async Task<UnidadMedida> UpdateAsync(
            int id,
            string abreviatura, string nombreUnidad
        )
        {
            Check.NotNullOrWhiteSpace(abreviatura, nameof(abreviatura));
            Check.Length(abreviatura, nameof(abreviatura), UnidadMedidaConsts.AbreviaturaMaxLength, UnidadMedidaConsts.AbreviaturaMinLength);
            Check.NotNullOrWhiteSpace(nombreUnidad, nameof(nombreUnidad));
            Check.Length(nombreUnidad, nameof(nombreUnidad), UnidadMedidaConsts.NombreUnidadMaxLength);

            var unidadMedida = await _unidadMedidaRepository.GetAsync(id);

            unidadMedida.Abreviatura = abreviatura;
            unidadMedida.NombreUnidad = nombreUnidad;

            return await _unidadMedidaRepository.UpdateAsync(unidadMedida);
        }

    }
}