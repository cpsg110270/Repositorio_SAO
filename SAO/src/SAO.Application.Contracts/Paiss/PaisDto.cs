using System;
using Volo.Abp.Application.Dtos;

namespace SAO.Paiss
{
    public class PaisDto : EntityDto<int>
    {
        public string NombrePais { get; set; }

    }
}