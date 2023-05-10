using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using SAO.Paiss;
using SAO.EntityFrameworkCore;
using Xunit;

namespace SAO.Paiss
{
    public class PaisRepositoryTests : SAOEntityFrameworkCoreTestBase
    {
        private readonly IPaisRepository _paisRepository;

        public PaisRepositoryTests()
        {
            _paisRepository = GetRequiredService<IPaisRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _paisRepository.GetListAsync(
                    nombrePais: "63a276cd0b3148a692b0b239731c44beb9570a3c44f24ea49a"
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
                var result = await _paisRepository.GetCountAsync(
                    nombrePais: "3451766268c24073af5802f915e398fd2f9c6eb67b014b12a3"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}