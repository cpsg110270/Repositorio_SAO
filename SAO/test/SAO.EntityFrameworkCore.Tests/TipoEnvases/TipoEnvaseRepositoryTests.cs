using SAO.EntityFrameworkCore;
using Shouldly;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SAO.TipoEnvases
{
    public class TipoEnvaseRepositoryTests : SAOEntityFrameworkCoreTestBase
    {
        private readonly ITipoEnvaseRepository _tipoEnvaseRepository;

        public TipoEnvaseRepositoryTests()
        {
            _tipoEnvaseRepository = GetRequiredService<ITipoEnvaseRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _tipoEnvaseRepository.GetListAsync(
                    desEnvase: "5cde84e5796f44f98a83"
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
                var result = await _tipoEnvaseRepository.GetCountAsync(
                    desEnvase: "da10ddf1795b46b6a3c0"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}