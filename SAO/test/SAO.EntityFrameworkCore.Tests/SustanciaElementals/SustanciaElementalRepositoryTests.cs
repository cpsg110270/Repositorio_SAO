using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using SAO.SustanciaElementals;
using SAO.EntityFrameworkCore;
using Xunit;

namespace SAO.SustanciaElementals
{
    public class SustanciaElementalRepositoryTests : SAOEntityFrameworkCoreTestBase
    {
        private readonly ISustanciaElementalRepository _sustanciaElementalRepository;

        public SustanciaElementalRepositoryTests()
        {
            _sustanciaElementalRepository = GetRequiredService<ISustanciaElementalRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _sustanciaElementalRepository.GetListAsync(
                    codCas: "e2456395d55640d",
                    desSustancia: "8f03f7799d1a46baa7b4a8b466615d24e72465d9969e481191"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("abb9f38f-02b3-4c25-9703-9a928f83c185"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _sustanciaElementalRepository.GetCountAsync(
                    codCas: "e597dcd8b8d041b",
                    desSustancia: "6e9cab24f0774173894ab1c4c442b7d0f090912a1ba54bd595"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}