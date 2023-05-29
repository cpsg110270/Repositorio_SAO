using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using SAO.CuotaImportadors;
using SAO.EntityFrameworkCore;
using Xunit;

namespace SAO.CuotaImportadors
{
    public class CuotaImportadorRepositoryTests : SAOEntityFrameworkCoreTestBase
    {
        private readonly ICuotaImportadorRepository _cuotaImportadorRepository;

        public CuotaImportadorRepositoryTests()
        {
            _cuotaImportadorRepository = GetRequiredService<ICuotaImportadorRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _cuotaImportadorRepository.GetListAsync(

                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("c4967a32-1e44-4e3d-9798-af87dd9e5c23"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _cuotaImportadorRepository.GetCountAsync(

                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}