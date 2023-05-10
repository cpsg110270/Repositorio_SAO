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
                    noPermiso: "6919c449",
                    noFactura: "379a278d08de",
                    observaciones: "c702a1fdd4bb44aeb6554727d879a01f75d2741e8cd24136abc4c0e00e775e566c0e2b9c2f4e4232ae9c34853108aa44eaceec067885402a8b4fe5084bbf59fa6e35f32f8b35469faf901705698f97467f9000fe68644cdaab38197321d9020305f3bd1061684605b03f56ad1054632d741ff67d9df0487fab7811d85d1f4ec53dd822593bef423c9ded06f281ab6f1ce8cb89acff5a",
                    esRenovacion: true,
                    estado: true
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("a1bdbd90-a5ae-42f6-842f-395a52b70947"));
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
                    noPermiso: "d948eafb",
                    noFactura: "cebd4eedf3f0",
                    observaciones: "7ac236c14bac4c3baf18cf5ec541105cc42428c0927b42eb9200dc57f596a99c1cbfa30516f3409f871367546ffd41213bf563d4820f4fba976e8f88e4cdea91ec5c93a3cdc1447b84d80e58120f41490d74aeacca76494986a03b13111ecca5c5f6991015334ba4b5fbd3fbadca05ef8584ecf0b85c4c61bbe611f91afe852f8be26d3bd5184b63a554f48b0c9213513169d3bf81be",
                    esRenovacion: true,
                    estado: true
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}