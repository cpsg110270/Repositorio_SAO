using SAO.EntityFrameworkCore;
using Shouldly;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SAO.PuertoEntradaSalidas
{
    public class PuertoEntradaSalidaRepositoryTests : SAOEntityFrameworkCoreTestBase
    {
        private readonly IPuertoEntradaSalidaRepository _puertoEntradaSalidaRepository;

        public PuertoEntradaSalidaRepositoryTests()
        {
            _puertoEntradaSalidaRepository = GetRequiredService<IPuertoEntradaSalidaRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _puertoEntradaSalidaRepository.GetListAsync(
                    nombrePuerto: "96d2471158d141e6952ade9d64c8e1fae6d92e97a9254fb58e"
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
                var result = await _puertoEntradaSalidaRepository.GetCountAsync(
                    nombrePuerto: "8a7eb6b486384d4c9db845e8a6010853e6fac99ceb074dcfad"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}