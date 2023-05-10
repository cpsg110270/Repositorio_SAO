$(function () {
    var l = abp.localization.getResource("SAO");
	
	var tipoEnvaseService = window.sAO.tipoEnvases.tipoEnvases;
	
	
    var createModal = new abp.ModalManager({
        viewUrl: abp.appPath + "TipoEnvases/CreateModal",
        scriptUrl: "/Pages/TipoEnvases/createModal.js",
        modalClass: "tipoEnvaseCreate"
    });

	var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + "TipoEnvases/EditModal",
        scriptUrl: "/Pages/TipoEnvases/editModal.js",
        modalClass: "tipoEnvaseEdit"
    });

	var getFilter = function() {
        return {
            filterText: $("#FilterText").val(),
            desEnvase: $("#DesEnvaseFilter").val()
        };
    };

    var dataTable = $("#TipoEnvasesTable").DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        scrollX: true,
        autoWidth: true,
        scrollCollapse: true,
        order: [[1, "asc"]],
        ajax: abp.libs.datatables.createAjax(tipoEnvaseService.getList, getFilter),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l("Edit"),
                                visible: abp.auth.isGranted('SAO.TipoEnvases.Edit'),
                                action: function (data) {
                                    editModal.open({
                                     id: data.record.id
                                     });
                                }
                            },
                            {
                                text: l("Delete"),
                                visible: abp.auth.isGranted('SAO.TipoEnvases.Delete'),
                                confirmMessage: function () {
                                    return l("DeleteConfirmationMessage");
                                },
                                action: function (data) {
                                    tipoEnvaseService.delete(data.record.id)
                                        .then(function () {
                                            abp.notify.info(l("SuccessfullyDeleted"));
                                            dataTable.ajax.reload();
                                        });
                                }
                            }
                        ]
                }
            },
			{ data: "desEnvase" }
        ]
    }));

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $("#NewTipoEnvaseButton").click(function (e) {
        e.preventDefault();
        createModal.open();
    });

	$("#SearchForm").submit(function (e) {
        e.preventDefault();
        dataTable.ajax.reload();
    });

    $("#ExportToExcelButton").click(function (e) {
        e.preventDefault();

        tipoEnvaseService.getDownloadToken().then(
            function(result){
                    var input = getFilter();
                    var url =  abp.appPath + 'api/app/tipo-envases/as-excel-file' + 
                        abp.utils.buildQueryString([
                            { name: 'downloadToken', value: result.token },
                            { name: 'filterText', value: input.filterText }, 
                            { name: 'desEnvase', value: input.desEnvase }
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
