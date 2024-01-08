$(function () {
    var l = abp.localization.getResource("SAO");
	
	var importadorService = window.sAO.importadors.importadors;
	
	
    var createModal = new abp.ModalManager({
        viewUrl: abp.appPath + "Importadors/CreateModal",
        scriptUrl: "/Pages/Importadors/createModal.js",
        modalClass: "importadorCreate"
    });

	var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + "Importadors/EditModal",
        scriptUrl: "/Pages/Importadors/editModal.js",
        modalClass: "importadorEdit"
    });

    //var addCuotaModal = new abp.ModalManager({
    ////    //viewUrl: abp.appPath + "CuotaImportadors/CreateModal",
    ////    // scriptUrl: "/Pages/CuotaImportadors/createModal.js",
    ////    //modalClass: "cuotaImportadorCreate"

    //    viewUrl: abp.appPath + "CuotaImportadors/Index",
    //    scriptUrl: "/Pages/CuotaImportadors/index.js",
    //     modalClass: "Index"
        
    //});
    alert("enla funcion");
    var getFilter = function () {
       
        return {
            
            filterText: $("#FilterText").val(),
            nombreImportador: $("#NombreImportadorFilter").val(),
            noRuc: $("#NoRUCFilter").val(),
            noImportador: $("#NoImportadorFilter").val()

        };
    };

    var dataTable = $("#ImportadorsTable").DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        scrollX: true,
        autoWidth: true,
        scrollCollapse: true,
        order: [[1, "asc"]],
        ajax: abp.libs.datatables.createAjax(importadorService.getList, getFilter),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l("Edit"),
                                visible: abp.auth.isGranted('SAO.Importadors.Edit'),
                                action: function (data) {
                                    editModal.open({
                                        id: data.record.id
                                    });
                                }
                            },
                            {
                                //text: l("AddCuota"),
                                //visible: abp.auth.isGranted('SAO.Importadors.Edit'),
                                //action: function (data) {
                                //    addCuotaModal.open({
                                //        importadorId: data.record.id
                                //    });



                                text: "Cuotas",
                                visible: abp.auth.isGranted('SAO.Importadors.Edit'),
                               
                                action: function (data) {
                                    cuotaImportador(data.record.id);
                                }
                                
                            },

                                                        
                            {
                                text: l("Delete"),
                                visible: abp.auth.isGranted('SAO.Importadors.Delete'),
                                confirmMessage: function () {
                                    return l("DeleteConfirmationMessage");
                                },
                                action: function (data) {
                                    importadorService.delete(data.record.id)
                                        .then(function () {
                                            abp.notify.info(l("SuccessfullyDeleted"));
                                            dataTable.ajax.reload();
                                        });
                                }
                            }
                        ]
                }
            },
			{ data: "noImportador" },
			{ data: "nombreImportador" },
			{ data: "noRUC" }
        ]
    }));



    function cuotaImportador(id) {
         window.location.href = "/CuotaImportadors/Index?ImportadorId=" + id;
    }

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $("#NewImportadorButton").click(function (e) {
        e.preventDefault();
        createModal.open();
    });

	$("#SearchForm").submit(function (e) {
        e.preventDefault();
        dataTable.ajax.reload();
    });

    $("#ExportToExcelButton").click(function (e) {
        e.preventDefault();

        importadorService.getDownloadToken().then(
            function(result){
                    var input = getFilter();
                    var url =  abp.appPath + 'api/app/importadors/as-excel-file' + 
                        abp.utils.buildQueryString([
                            { name: 'downloadToken', value: result.token },
                            { name: 'filterText', value: input.filterText }, 
                            { name: 'nombreImportador', value: input.nombreImportador }
                            ]);
                            
                    var downloadWindow = window.open(url, '_blank');
                    downloadWindow.focus();
            }
        )
    });

    $('#AdvancedFilterSectionToggler').on('click', function (e) {
        $('#AdvancedFilterSection').toggle();
    });

    $('#AdvancedFilterSection').on('keypress', function (e) {
        if (e.which === 13) {
            dataTable.ajax.reload();
        }
    });

    $('#AdvancedFilterSection select').change(function() {
        dataTable.ajax.reload();
    });
    
    
});

