using SAO.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SAO.Almacens;

namespace SAO.Web.Pages.Almacens
{
    public class CreateModalModel : SAOPageModel
    {
        [BindProperty]
        public AlmacenCreateViewModel Almacen { get; set; }

        private readonly IAlmacensAppService _almacensAppService;

        public CreateModalModel(IAlmacensAppService almacensAppService)
        {
            _almacensAppService = almacensAppService;

            Almacen = new();
        }

        public async Task OnGetAsync()
        {
            Almacen = new AlmacenCreateViewModel();

            await Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync()
        {

            await _almacensAppService.CreateAsync(ObjectMapper.Map<AlmacenCreateViewModel, AlmacenCreateDto>(Almacen));
            return NoContent();
        }
    }

    public class AlmacenCreateViewModel : AlmacenCreateDto
    {
    }
}