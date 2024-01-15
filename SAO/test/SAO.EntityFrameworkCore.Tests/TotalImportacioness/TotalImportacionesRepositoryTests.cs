using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using SAO.TotalImportacioness;
using SAO.EntityFrameworkCore;
using Xunit;

namespace SAO.TotalImportacioness
{
    public class TotalImportacionesRepositoryTests : SAOEntityFrameworkCoreTestBase
    {
        private readonly ITotalImportacionesRepository _totalImportacionesRepository;

        public TotalImportacionesRepositoryTests()
        {
            _totalImportacionesRepository = GetRequiredService<ITotalImportacionesRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _totalImportacionesRepository.GetListAsync(

                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("a72937f3-827f-4ced-acb3-40dabbb13047"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _totalImportacionesRepository.GetCountAsync(

                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}