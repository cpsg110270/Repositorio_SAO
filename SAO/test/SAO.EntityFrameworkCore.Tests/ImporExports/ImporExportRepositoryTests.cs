using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using SAO.ImporExports;
using SAO.EntityFrameworkCore;
using Xunit;

namespace SAO.ImporExports
{
    public class ImporExportRepositoryTests : SAOEntityFrameworkCoreTestBase
    {
        private readonly IImporExportRepository _imporExportRepository;

        public ImporExportRepositoryTests()
        {
            _imporExportRepository = GetRequiredService<IImporExportRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _imporExportRepository.GetListAsync(
                    noPermiso: "fafd4ec1",
                    noFactura: "e79d2f518ff847f198b4d4c4cf863d",
                    observaciones: "5b76b50ec1514eaa9264890e49aa7b3981055a5789694d7881844278f1a8a742d6c98518f6aa4123808cab7ad6a802614e2000efa8c84c38b5262faa2f893676e366d2bdcb01454497dd28ef5fd2295674dd4297cad94e75ba1d28accd678e3e4cc0dd71a704425c8c47402b8f0866bfbed647653b9f4360a2dd40c187c4b20b219f57e2f0bb474a8be6a4c122e8f4b9a12982064864",
                    esRenovacion: true,
                    estado: true
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("b9680f28-6f29-4b16-aee6-1b5c9e3db1cb"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _imporExportRepository.GetCountAsync(
                    noPermiso: "cc29d75c",
                    noFactura: "e66c525e10934a36b27bdc16467d37",
                    observaciones: "74e3fc89d0284cbcb9b560f3b0fd7c6d5b69fa378f224721a44ceba651dfe81703d2ba663fd140e59f1b277884787a3b751cb073d54248988d5e4e7bd24d3cd9fb02baeefcc94c59a73f03cc255be421a4216226398641218a0755947929513441e7b5a710e24f7aa9f1f8648d4234f0978e65b9e7534a1ab3f5af154e3c6e37a9e2021392ac447a855f408a545b30dcd63682bb01f5",
                    esRenovacion: true,
                    estado: true
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}