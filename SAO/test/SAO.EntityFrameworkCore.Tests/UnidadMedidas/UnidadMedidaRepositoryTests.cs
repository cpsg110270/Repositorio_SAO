using SAO.EntityFrameworkCore;
using Shouldly;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SAO.UnidadMedidas
{
    public class UnidadMedidaRepositoryTests : SAOEntityFrameworkCoreTestBase
    {
        private readonly IUnidadMedidaRepository _unidadMedidaRepository;

        public UnidadMedidaRepositoryTests()
        {
            _unidadMedidaRepository = GetRequiredService<IUnidadMedidaRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _unidadMedidaRepository.GetListAsync(
                    abreviatura: "6a70",
                    nombreUnidad: "aca2b1a0e61e460ca807f83d479047794cef37699f4a417db8"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(1);
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _unidadMedidaRepository.GetCountAsync(
                    abreviatura: "d9da",
                    nombreUnidad: "03c564243a6b4a6382a180f774e86d6134cdc0f064194208b9"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}