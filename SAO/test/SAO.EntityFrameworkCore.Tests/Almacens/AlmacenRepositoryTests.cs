using SAO.EntityFrameworkCore;
using Shouldly;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SAO.Almacens
{
    public class AlmacenRepositoryTests : SAOEntityFrameworkCoreTestBase
    {
        private readonly IAlmacenRepository _almacenRepository;

        public AlmacenRepositoryTests()
        {
            _almacenRepository = GetRequiredService<IAlmacenRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _almacenRepository.GetListAsync(
                    nombreAlmacen: "7cd8e1b5c6904e87896d7f08ec6631286530b2e7dc4e46ed89d7d5433cbf04724e879a4ad5a04e2c996a4be2944d219163d8db8464d4469fb87b578d4f0545e29303be99fa3b4f0b955b5c4b6297d2951eadc142b4704bc8b4de0be732c75a676d44e59b",
                    siglaAlmacen: "d58adcbd6d084a999b56"
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
                var result = await _almacenRepository.GetCountAsync(
                    nombreAlmacen: "58f90229e2ac4f529f9df8646a011f6c9e613e8522ed41a68f6b559b311f30ced51b7594057c4d47a32267849e9167cb4d0e86c915154b55b15034b9ad3e71399608906623034c228e56630d0d357d8520170bee22634997bae67a244b077d46b87ccf64",
                    siglaAlmacen: "3e2a5abf68384d678b99"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}