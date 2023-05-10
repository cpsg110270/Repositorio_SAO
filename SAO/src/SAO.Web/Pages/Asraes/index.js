$(function () {
    var l = abp.localization.getResource("SAO");
	
	var asraeService = window.sAO.asraes.asraes;
	
	
    var createModal = new abp.ModalManager({
        viewUrl: abp.appPath + "Asraes/CreateModal",
        scriptUrl: "/Pages/Asraes/createModal.js",
        modalClass: "asraeCreate"
    });

	var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + "Asraes/EditModal",
        scriptUrl: "/Pages/Asraes/editModal.js",
        modalClass: "asraeEdit"
    });

	var getFilter = function() {
        return {
            filterText: $("#FilterText").val(),
            codigo_ASHRAE: $("#Codigo_ASHRAEFilter").val(),
			descripcion: $("#DescripcionFilter").val()
        };
    };

    var dataTable = $("#AsraesTable").DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        scrollX: true,
        autoWidth: true,
        scrollCollapse: true,
        order: [[1, "asc"]],
        ajax: abp.libs.datatables.createAjax(asraeService.getList, getFilter),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l("Edit"),
                                visible: abp.auth.isGranted('SAO.Asraes.Edit'),
                                action: function (data) {
                                    editModal.open({
                                     id: data.record.id
                                     });
                                }
                            },
                            {
                                text: l("Delete"),
                                visible: abp.auth.isGranted('SAO.Asraes.Delete'),
                                confirmMessage: function () {
                                    return l("DeleteConfirmationMessage");
                                },
                                action: function (data) {
                                    asraeService.delete(data.record.id)
                                        .then(function () {
                                            abp.notify.info(l("SuccessfullyDeleted"));
                                            dataTable.ajax.reload();
                                        });
                                }
                            }
                        ]
                }
            },
			{ data: "codigo_ASHRAE" },
			{ data: "descripcion" }
        ]
    }));

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $("#NewAsraeButton").click(function (e) {
        e.preventDefault();
        createModal.open();
    });

	$("#SearchForm").submit(function (e) {
        e.preventDefault();
        dataTable.ajax.reload();
    });

    $("#ExportToExcelButton").click(function (e) {
        e.preventDefault();

        asraeService.getDownloadToken().then(
            function(result){
                    var input = getFilter();
                    var url =  abp.appPath + 'api/app/asraes/as-excel-file' + 
                        abp.utils.buildQueryString([
                            { name: 'downloadToken', value: result.token },
                            { name: 'filterText', value: input.filterText }, 
                            { name: 'codigo_ASHRAE', value: input.codigo_ASHRAE }, 
                            { name: 'descripcion', value: input.descripcion }
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
