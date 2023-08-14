using SAO.EntityFrameworkCore;
using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SAO.Fabricantes
{
    public class FabricanteRepositoryTests : SAOEntityFrameworkCoreTestBase
    {
        private readonly IFabricanteRepository _fabricanteRepository;

        public FabricanteRepositoryTests()
        {
            _fabricanteRepository = GetRequiredService<IFabricanteRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _fabricanteRepository.GetListAsync(
                    nombreFabricante: "3ab80e7fa9184a89bacbf94deea86287a5456483798b4e27ba495a67e9c72637779f872aabaf45fcac21a1c3607908959991"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("d4944455-6f12-40c8-b177-99ce52c6ac28"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _fabricanteRepository.GetCountAsync(
                    nombreFabricante: "eb6863cedc7348868b080664f404d9d6b4727a32b6a3408ca4a2117e048c8d67487fa08d86354ac3b4dc952dc4812c19e7e3"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}