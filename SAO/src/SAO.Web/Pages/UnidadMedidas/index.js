$(function () {
    var l = abp.localization.getResource("SAO");
	
	var unidadMedidaService = window.sAO.unidadMedidas.unidadMedidas;
	
	
    var createModal = new abp.ModalManager({
        viewUrl: abp.appPath + "UnidadMedidas/CreateModal",
        scriptUrl: "/Pages/UnidadMedidas/createModal.js",
        modalClass: "unidadMedidaCreate"
    });

	var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + "UnidadMedidas/EditModal",
        scriptUrl: "/Pages/UnidadMedidas/editModal.js",
        modalClass: "unidadMedidaEdit"
    });

	var getFilter = function() {
        return {
            filterText: $("#FilterText").val(),
            abreviatura: $("#AbreviaturaFilter").val(),
			nombreUnidad: $("#NombreUnidadFilter").val()
        };
    };

    var dataTable = $("#UnidadMedidasTable").DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        scrollX: true,
        autoWidth: true,
        scrollCollapse: true,
        order: [[1, "asc"]],
        ajax: abp.libs.datatables.createAjax(unidadMedidaService.getList, getFilter),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l("Edit"),
                                visible: abp.auth.isGranted('SAO.UnidadMedidas.Edit'),
                                action: function (data) {
                                    editModal.open({
                                     id: data.record.id
                                     });
                                }
                            },
                            {
                                text: l("Delete"),
                                visible: abp.auth.isGranted('SAO.UnidadMedidas.Delete'),
                                confirmMessage: function () {
                                    return l("DeleteConfirmationMessage");
                                },
                                action: function (data) {
                                    unidadMedidaService.delete(data.record.id)
                                        .then(function () {
                                            abp.notify.info(l("SuccessfullyDeleted"));
                                            dataTable.ajax.reload();
                                        });
                                }
                            }
                        ]
                }
            },
			{ data: "abreviatura" },
			{ data: "nombreUnidad" }
        ]
    }));

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $("#NewUnidadMedidaButton").click(function (e) {
        e.preventDefault();
        createModal.open();
    });

	$("#SearchForm").submit(function (e) {
        e.preventDefault();
        dataTable.ajax.reload();
    });

    $("#ExportToExcelButton").click(function (e) {
        e.preventDefault();

        unidadMedidaService.getDownloadToken().then(
            function(result){
                    var input = getFilter();
                    var url =  abp.appPath + 'api/app/unidad-medidas/as-excel-file' + 
                        abp.utils.buildQueryString([
                            { name: 'downloadToken', value: result.token },
                            { name: 'filterText', value: input.filterText }, 
                            { name: 'abreviatura', value: input.abreviatura }, 
                            { name: 'nombreUnidad', value: input.nombreUnidad }
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
