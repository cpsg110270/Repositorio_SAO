$(function () {
    var l = abp.localization.getResource("SAO");
	
	var sustanciaElementalService = window.sAO.sustanciaElementals.sustanciaElementals;
	
	
    var createModal = new abp.ModalManager({
        viewUrl: abp.appPath + "SustanciaElementals/CreateModal",
        scriptUrl: "/Pages/SustanciaElementals/createModal.js",
        modalClass: "sustanciaElementalCreate"
    });

	var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + "SustanciaElementals/EditModal",
        scriptUrl: "/Pages/SustanciaElementals/editModal.js",
        modalClass: "sustanciaElementalEdit"
    });

	var getFilter = function() {
        return {
            filterText: $("#FilterText").val(),
            codCas: $("#CodCasFilter").val(),
			desSustancia: $("#DesSustanciaFilter").val()
        };
    };

    var dataTable = $("#SustanciaElementalsTable").DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        scrollX: true,
        autoWidth: true,
        scrollCollapse: true,
        order: [[1, "asc"]],
        ajax: abp.libs.datatables.createAjax(sustanciaElementalService.getList, getFilter),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l("Edit"),
                                visible: abp.auth.isGranted('SAO.SustanciaElementals.Edit'),
                                action: function (data) {
                                    editModal.open({
                                     id: data.record.id
                                     });
                                }
                            },
                            {
                                text: l("Delete"),
                                visible: abp.auth.isGranted('SAO.SustanciaElementals.Delete'),
                                confirmMessage: function () {
                                    return l("DeleteConfirmationMessage");
                                },
                                action: function (data) {
                                    sustanciaElementalService.delete(data.record.id)
                                        .then(function () {
                                            abp.notify.info(l("SuccessfullyDeleted"));
                                            dataTable.ajax.reload();
                                        });
                                }
                            }
                        ]
                }
            },
			{ data: "codCas" },
			{ data: "desSustancia" }
        ]
    }));

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $("#NewSustanciaElementalButton").click(function (e) {
        e.preventDefault();
        createModal.open();
    });

	$("#SearchForm").submit(function (e) {
        e.preventDefault();
        dataTable.ajax.reload();
    });

    $("#ExportToExcelButton").click(function (e) {
        e.preventDefault();

        sustanciaElementalService.getDownloadToken().then(
            function(result){
                    var input = getFilter();
                    var url =  abp.appPath + 'api/app/sustancia-elementals/as-excel-file' + 
                        abp.utils.buildQueryString([
                            { name: 'downloadToken', value: result.token },
                            { name: 'filterText', value: input.filterText }, 
                            { name: 'codCas', value: input.codCas }, 
                            { name: 'desSustancia', value: input.desSustancia }
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
