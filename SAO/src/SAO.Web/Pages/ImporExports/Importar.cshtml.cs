using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using SAO.ImporExports;

namespace SAO.Web.Pages.ImporExports
{
    public class ImportarModel : SAOPageModel
    {
        [BindProperty]
        public IFormFile XlsFile { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        [BindProperty]
        public List<ImporExportCreateDto> ListaLiecncias { get; set; } = new List<ImporExportCreateDto>();

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPostImport()
        {
            ErrorMessage = null;

            try
            {
                var stream = XlsFile.OpenReadStream();

                //After save excel file in wwwroot and then
                using (ExcelPackage package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();
                    if (worksheet == null)
                    {
                        //return or alert message here
                        ErrorMessage = "Ha ocurrido un error! No se carga  el archivo";
                    }
                    else
                    {
                        //read excel file data and add data in  model.StaffInfoViewModel.StaffList
                        var rowCount = worksheet.Dimension.Rows;
                        for (int row = 5; row <= rowCount; row++)
                        {
                            string Permiso = (worksheet.Cells[row, 1].Value ?? string.Empty).ToString().Trim();
                            string FechaE  = (worksheet.Cells[row, 2].Value ?? string.Empty).ToString().Trim();
                            string FechaS  = (worksheet.Cells[row, 3].Value ?? string.Empty).ToString().Trim();
                            string PesoN   = (worksheet.Cells[row, 4].Value ?? string.Empty).ToString().Trim(); 
                            string PesoU   = (worksheet.Cells[row, 5].Value ?? string.Empty).ToString().Trim(); 
                            string CantEnv = (worksheet.Cells[row, 6].Value ?? string.Empty).ToString().Trim(); 
                            string Factura = (worksheet.Cells[row, 7].Value ?? string.Empty).ToString().Trim(); 
                             
                            
                            ListaLiecncias.Add(new ImporExportCreateDto
                            {
                                NoPermiso = Permiso,
                                FechaEmision = DateTime.Parse(FechaE),
                                FechaSolicitud = DateTime.Parse(FechaS),
                                PesoNeto = Double.Parse(PesoN),
                                PesoUnitario= Double.Parse(PesoU),
                                CantEnvvase= int.Parse(CantEnv),
                                NoFactura = Factura,

                                 
                            });
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = "Ha ocurrido un error! " + ex.Message;
            }

            //return same view and pass view model
            return Page();
        }
    }
}