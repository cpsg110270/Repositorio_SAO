using SAO.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using SAO.SustanciaElementals;

namespace SAO.Web.Pages.SustanciaElementals
{
    public class EditModalModel : SAOPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public SustanciaElementalUpdateViewModel SustanciaElemental { get; set; }

        private readonly ISustanciaElementalsAppService _sustanciaElementalsAppService;

        public EditModalModel(ISustanciaElementalsAppService sustanciaElementalsAppService)
        {
            _sustanciaElementalsAppService = sustanciaElementalsAppService;

            SustanciaElemental = new();
        }

        public async Task OnGetAsync()
        {
            var sustanciaElemental = await _sustanciaElementalsAppService.GetAsync(Id);
            SustanciaElemental = ObjectMapper.Map<SustanciaElementalDto, SustanciaElementalUpdateViewModel>(sustanciaElemental);

        }

        public async Task<NoContentResult> OnPostAsync()
        {

            await _sustanciaElementalsAppService.UpdateAsync(Id, ObjectMapper.Map<SustanciaElementalUpdateViewModel, SustanciaElementalUpdateDto>(SustanciaElemental));
            return NoContent();
        }
    }

    public class SustanciaElementalUpdateViewModel : SustanciaElementalUpdateDto
    {
    }
}