using Shouldly;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace SAO.Almacens
{
    public class AlmacensAppServiceTests : SAOApplicationTestBase
    {
        private readonly IAlmacensAppService _almacensAppService;
        private readonly IRepository<Almacen, int> _almacenRepository;

        public AlmacensAppServiceTests()
        {
            _almacensAppService = GetRequiredService<IAlmacensAppService>();
            _almacenRepository = GetRequiredService<IRepository<Almacen, int>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _almacensAppService.GetListAsync(new GetAlmacensInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == 1).ShouldBe(true);
            result.Items.Any(x => x.Id == 2).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _almacensAppService.GetAsync(1);

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(1);
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new AlmacenCreateDto
            {
                NombreAlmacen = "6401b0d36bdd45da8a7f80a651065dda050c73cb4be3437f8c944b5da6305b9d695f57d0ad3a4b979fd7f6ee885ac78370eb2db8076048eaa5d1463df734ec54fd22be5b50cb4a698209f1f0ba385602fffd0f063cb4425490f9deba3a20ee35c9ce64ab",
                SiglaAlmacen = "44009eddc03c4f8c80cb"
            };

            // Act
            var serviceResult = await _almacensAppService.CreateAsync(input);

            // Assert
            var result = await _almacenRepository.FindAsync(c => c.NombreAlmacen == serviceResult.NombreAlmacen);

            result.ShouldNotBe(null);
            result.NombreAlmacen.ShouldBe("6401b0d36bdd45da8a7f80a651065dda050c73cb4be3437f8c944b5da6305b9d695f57d0ad3a4b979fd7f6ee885ac78370eb2db8076048eaa5d1463df734ec54fd22be5b50cb4a698209f1f0ba385602fffd0f063cb4425490f9deba3a20ee35c9ce64ab");
            result.SiglaAlmacen.ShouldBe("44009eddc03c4f8c80cb");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new AlmacenUpdateDto()
            {
                NombreAlmacen = "5a7ab86f8dbd4f6cb097c1409478d8b73b924bb4c788455d8863a0e76d95611e9e1965b7c9f647ae8ae61307e664b31be622d34f3b9d403ea74031b99097d8a8ae5f2a5693424afcbb954c7f523656c8c44c5e4956c6462da5b4ef397a74d9bb32543d5d",
                SiglaAlmacen = "3bb61d2d1cdb411ca424"
            };

            // Act
            var serviceResult = await _almacensAppService.UpdateAsync(1, input);

            // Assert
            var result = await _almacenRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.NombreAlmacen.ShouldBe("5a7ab86f8dbd4f6cb097c1409478d8b73b924bb4c788455d8863a0e76d95611e9e1965b7c9f647ae8ae61307e664b31be622d34f3b9d403ea74031b99097d8a8ae5f2a5693424afcbb954c7f523656c8c44c5e4956c6462da5b4ef397a74d9bb32543d5d");
            result.SiglaAlmacen.ShouldBe("3bb61d2d1cdb411ca424");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _almacensAppService.DeleteAsync(1);

            // Assert
            var result = await _almacenRepository.FindAsync(c => c.Id == 1);

            result.ShouldBeNull();
        }
    }
}