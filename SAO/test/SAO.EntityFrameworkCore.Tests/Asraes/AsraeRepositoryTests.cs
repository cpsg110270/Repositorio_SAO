using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using SAO.Asraes;
using SAO.EntityFrameworkCore;
using Xunit;

namespace SAO.Asraes
{
    public class AsraeRepositoryTests : SAOEntityFrameworkCoreTestBase
    {
        private readonly IAsraeRepository _asraeRepository;

        public AsraeRepositoryTests()
        {
            _asraeRepository = GetRequiredService<IAsraeRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _asraeRepository.GetListAsync(
                    codigo_ASHRAE: "a098480ecddc",
                    descripcion: "2546aa9855c246d3bf0633f12dc0d63"
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
                var result = await _asraeRepository.GetCountAsync(
                    codigo_ASHRAE: "94d95c88de5a",
                    descripcion: "ed943729fe55465995fa44a34f4918d6e25cac8f08d7479cbadf1ee3c4d875d0faf93bb48f4443"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}