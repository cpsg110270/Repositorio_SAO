$(function () {
    var l = abp.localization.getResource("SAO");
	
	var almacenService = window.sAO.almacens.almacens;
	
	
    var createModal = new abp.ModalManager({
        viewUrl: abp.appPath + "Almacens/CreateModal",
        scriptUrl: "/Pages/Almacens/createModal.js",
        modalClass: "almacenCreate"
    });

	var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + "Almacens/EditModal",
        scriptUrl: "/Pages/Almacens/editModal.js",
        modalClass: "almacenEdit"
    });

	var getFilter = function() {
        return {
            filterText: $("#FilterText").val(),
            nombreAlmacen: $("#NombreAlmacenFilter").val(),
			siglaAlmacen: $("#SiglaAlmacenFilter").val()
        };
    };

    var dataTable = $("#AlmacensTable").DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        scrollX: true,
        autoWidth: true,
        scrollCollapse: true,
        order: [[1, "asc"]],
        ajax: abp.libs.datatables.createAjax(almacenService.getList, getFilter),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l("Edit"),
                                visible: abp.auth.isGranted('SAO.Almacens.Edit'),
                                action: function (data) {
                                    editModal.open({
                                     id: data.record.id
                                     });
                                }
                            },
                            {
                                text: l("Delete"),
                                visible: abp.auth.isGranted('SAO.Almacens.Delete'),
                                confirmMessage: function () {
                                    return l("DeleteConfirmationMessage");
                                },
                                action: function (data) {
                                    almacenService.delete(data.record.id)
                                        .then(function () {
                                            abp.notify.info(l("SuccessfullyDeleted"));
                                            dataTable.ajax.reload();
                                        });
                                }
                            }
                        ]
                }
            },
			{ data: "nombreAlmacen" },
			{ data: "siglaAlmacen" }
        ]
    }));

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $("#NewAlmacenButton").click(function (e) {
        e.preventDefault();
        createModal.open();
    });

	$("#SearchForm").submit(function (e) {
        e.preventDefault();
        dataTable.ajax.reload();
    });

    $("#ExportToExcelButton").click(function (e) {
        e.preventDefault();

        almacenService.getDownloadToken().then(
            function(result){
                    var input = getFilter();
                    var url =  abp.appPath + 'api/app/almacens/as-excel-file' + 
                        abp.utils.buildQueryString([
                            { name: 'downloadToken', value: result.token },
                            { name: 'filterText', value: input.filterText }, 
                            { name: 'nombreAlmacen', value: input.nombreAlmacen }, 
                            { name: 'siglaAlmacen', value: input.siglaAlmacen }
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
