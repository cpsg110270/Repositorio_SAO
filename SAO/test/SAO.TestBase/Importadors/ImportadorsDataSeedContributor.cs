using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;

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
                id: Guid.Parse("7a33d145-8971-4bbf-aa7f-72ffbf6d0e37"),
                nombreImportador: "feb89470a4784cdfbfeef824b32e07d0a2df7a87497d4f5dbbe4f84d5713ac2ab595505bacde4f9ea6f41aa69c777c70297a6fe148ff48b7a786d4fdfb5aa033b645320ff6e74090a76df7720acf7191327cfbd70f00428f97341236d03769604192b2d40c034d16b090ea38af27fa486f7ee4d543d64d3ab55719aaf2"
            ));

            await _importadorRepository.InsertAsync(new Importador
            (
                id: Guid.Parse("356eac29-4fd5-494f-b442-597bfe47ceb2"),
                nombreImportador: "baa98a605fa345e49bf6b63ea633a8e45fb2781c08944c00aa3fe709f04faaa59f00ad5736164c22a3e5428f8ce2d4ce5e55f6cf2aba469ba0cc9122931e647ddd3fa0fa3a2f411fabb671c9614e135f35aededf705546d58afbddaf9d1363a7a428d66cdaa24000ae7d3e06a81ac2f94aa1f7d2237746c3a19062fd92"
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}