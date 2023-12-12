using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using SAO.Importadors;
using SAO.EntityFrameworkCore;
using Xunit;

namespace SAO.Importadors
{
    public class ImportadorRepositoryTests : SAOEntityFrameworkCoreTestBase
    {
        private readonly IImportadorRepository _importadorRepository;

        public ImportadorRepositoryTests()
        {
            _importadorRepository = GetRequiredService<IImportadorRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _importadorRepository.GetListAsync(
                    noRUC: "bb14a057c1564faeb56c",
                    nombreImportador: "dd5037f5d5b44152befe3699ce3590b96bd78158739843d8a2498e17d3df874ba3f403b709874de09047aa4f35e6bce4c7d2ba53116f4ebeab65e2aa4d5002fa6550a30330d14dd2be466c725e83d84aa5c5a1a844fd4e989202387b89c133c7b607f45dc7244ac79eb1da344302bf8532570ef3558842aba327e116a4"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("5ba62d29-2b7d-4f81-b985-290658f73cbf"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _importadorRepository.GetCountAsync(
                    noRUC: "eb7b9d6ce55b4a48be5a",
                    nombreImportador: "18cb1aa854924961a5a35123902fad61934a375b99bf4a95993bfb986afd81c02c6fa9087b124ce3a8a9de6bd2748475192a1d1bf01e43bfb41e989901d4434baeb761a6a3af44a69168b10af65f97047776a7a9883d45bf8212be8d31597dff3149675d5de646aab8611b3117e203fbecb63722ce9c47e09f814d32d5"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}