$(function () {
    var l = abp.localization.getResource("SAO");
	
	var puertoEntradaSalidaService = window.sAO.puertoEntradaSalidas.puertoEntradaSalidas;
	
	
    var createModal = new abp.ModalManager({
        viewUrl: abp.appPath + "PuertoEntradaSalidas/CreateModal",
        scriptUrl: "/Pages/PuertoEntradaSalidas/createModal.js",
        modalClass: "puertoEntradaSalidaCreate"
    });

	var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + "PuertoEntradaSalidas/EditModal",
        scriptUrl: "/Pages/PuertoEntradaSalidas/editModal.js",
        modalClass: "puertoEntradaSalidaEdit"
    });

	var getFilter = function() {
        return {
            filterText: $("#FilterText").val(),
            nombrePuerto: $("#NombrePuertoFilter").val()
        };
    };

    var dataTable = $("#PuertoEntradaSalidasTable").DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        scrollX: true,
        autoWidth: true,
        scrollCollapse: true,
        order: [[1, "asc"]],
        ajax: abp.libs.datatables.createAjax(puertoEntradaSalidaService.getList, getFilter),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l("Edit"),
                                visible: abp.auth.isGranted('SAO.PuertoEntradaSalidas.Edit'),
                                action: function (data) {
                                    editModal.open({
                                     id: data.record.id
                                     });
                                }
                            },
                            {
                                text: l("Delete"),
                                visible: abp.auth.isGranted('SAO.PuertoEntradaSalidas.Delete'),
                                confirmMessage: function () {
                                    return l("DeleteConfirmationMessage");
                                },
                                action: function (data) {
                                    puertoEntradaSalidaService.delete(data.record.id)
                                        .then(function () {
                                            abp.notify.info(l("SuccessfullyDeleted"));
                                            dataTable.ajax.reload();
                                        });
                                }
                            }
                        ]
                }
            },
			{ data: "nombrePuerto" }
        ]
    }));

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $("#NewPuertoEntradaSalidaButton").click(function (e) {
        e.preventDefault();
        createModal.open();
    });

	$("#SearchForm").submit(function (e) {
        e.preventDefault();
        dataTable.ajax.reload();
    });

    $("#ExportToExcelButton").click(function (e) {
        e.preventDefault();

        puertoEntradaSalidaService.getDownloadToken().then(
            function(result){
                    var input = getFilter();
                    var url =  abp.appPath + 'api/app/puerto-entrada-salidas/as-excel-file' + 
                        abp.utils.buildQueryString([
                            { name: 'downloadToken', value: result.token },
                            { name: 'filterText', value: input.filterText }, 
                            { name: 'nombrePuerto', value: input.nombrePuerto }
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
