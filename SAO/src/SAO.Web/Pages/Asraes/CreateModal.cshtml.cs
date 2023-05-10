using SAO.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SAO.Asraes;

namespace SAO.Web.Pages.Asraes
{
    public class CreateModalModel : SAOPageModel
    {
        [BindProperty]
        public AsraeCreateViewModel Asrae { get; set; }

        private readonly IAsraesAppService _asraesAppService;

        public CreateModalModel(IAsraesAppService asraesAppService)
        {
            _asraesAppService = asraesAppService;

            Asrae = new();
        }

        public async Task OnGetAsync()
        {
            Asrae = new AsraeCreateViewModel();

            await Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync()
        {

            await _asraesAppService.CreateAsync(ObjectMapper.Map<AsraeCreateViewModel, AsraeCreateDto>(Asrae));
            return NoContent();
        }
    }

    public class AsraeCreateViewModel : AsraeCreateDto
    {
    }
}