//using System;
//using System.Collections.Generic;
//using System.Linq;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Http;
//using OfficeOpenXml;
//using SAO.ImporExports;

//namespace SAO.Web.Pages.ImporExports
//{
//    public class ImportarModel : SAOPageModel
//    {
//        [BindProperty]
//        public IFormFile XlsFile { get; set; }

//        [TempData]
//        public string ErrorMessage { get; set; }

//        [BindProperty]
//        public List<TipoCambioCreateDto> ListaTipoCambio { get; set; } = new List<TipoCambioCreateDto>();

//        public IActionResult OnGet()
//        {
//            return Page();
//        }

//        public IActionResult OnPostImport()
//        {
//            ErrorMessage = null;

//            try
//            {
//                var stream = XlsFile.OpenReadStream();

//                //After save excel file in wwwroot and then
//                using (ExcelPackage package = new ExcelPackage(stream))
//                {
//                    ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();
//                    if (worksheet == null)
//                    {
//                        //return or alert message here
//                        ErrorMessage = "Ha ocurrido un error! No se carg  el archivo";
//                    }
//                    else
//                    {
//                        //read excel file data and add data in  model.StaffInfoViewModel.StaffList
//                        var rowCount = worksheet.Dimension.Rows;
//                        for (int row = 5; row <= rowCount; row++)
//                        {
//                            string Fecha = (worksheet.Cells[row, 1].Value ?? string.Empty).ToString().Trim();
//                            string Valor = (worksheet.Cells[row, 2].Value ?? string.Empty).ToString().Trim();

//                            ListaTipoCambio.Add(new TipoCambioCreateDto
//                            {
//                                Fecha = DateTime.Parse(Fecha),
//                                valor = decimal.Parse(Valor),
//                            });
//                        }

//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                ErrorMessage = "Ha ocurrido un error! " + ex.Message;
//            }

//            //return same view and pass view model
//            return Page();
//        }
//    }
//}