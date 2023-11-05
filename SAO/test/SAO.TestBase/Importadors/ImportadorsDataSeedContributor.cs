using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using SAO.Importadors;

namespace SAO.Importadors
{
    public class ImportadorsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IImportadorRepository _importadorRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public ImportadorsDataSeedContributor(IImportadorRepository importadorRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _importadorRepository = importadorRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _importadorRepository.InsertAsync(new Importador
            (
                id: Guid.Parse("7f4bc753-28f7-474a-a2d3-90ce35f5dbe0"),
                nombreImportador: "6dd81afdab9b45df857c12bbf9b016daf6c8c107c1044325a362a431b21db14e394680b87a974594971a8607072b7a853dcceeb95f3c4d19a6e7bd51a73fe65322135b4557d14f598f1f65b3228be0178bb15ba9aba64d76a9ef9dca0f54f2edbb0c6c82ef5242049033f752052061dbcfa26767a5cf46c69234f8a2e8",
                noRUC: "37522ccc58e74f8e9361"
            ));

            await _importadorRepository.InsertAsync(new Importador
            (
                id: Guid.Parse("dc15f0ac-c05a-4df1-a379-2c730f920eb2"),
                nombreImportador: "265aaff039a14983a08dbde1747da1b5d3098c9197624b2783084f4d4f18e585f83f1afb0565488b984aae0f6ea7365452993e18a8c14592a6d6834066202e70f9cbd4372702445393d3e201838d78c90f24c42d4eac49b7b9750b82d08e2e1b17fafbed46aa45bdac494ba20bf14f226be2ef40894f46c1945f46c7b0",
                noRUC: "acde0330bb5644949178"
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}