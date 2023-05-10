var abp = abp || {};

abp.modals.imporExportEdit = function () {
    var initModal = function (publicApi, args) {
        var l = abp.localization.getResource("SAO");
        
        
        var lastNpIdId = '';
        var lastNpDisplayNameId = '';

        var _lookupModal = new abp.ModalManager({
            viewUrl: abp.appPath + "Shared/LookupModal",
            scriptUrl: "/Pages/Shared/lookupModal.js",
            modalClass: "navigationPropertyLookup"
        });

        $('.lookupCleanButton').on('click', '', function () {
            $(this).parent().find('input').val('');
        });

        _lookupModal.onClose(function () {
            var modal = $(_lookupModal.getModal());
            $('#' + lastNpIdId).val(modal.find('#CurrentLookupId').val());
            $('#' + lastNpDisplayNameId).val(modal.find('#CurrentLookupDisplayName').val());
        });
        
        publicApi.onOpen(function () {
            $('#ImporExport_ImportadorId').select2({
                dropdownParent: $('#ImporExportEditModal'),
                ajax: {
                    url: abp.appPath + 'api/app/impor-exports/importador-lookup',
                    type: 'GET',
                    data: function (params) {
                        return { filter: params.term, maxResultCount: 10 }
                    },
                    processResults: function (data) {
                        var mappedItems = _.map(data.items, function (item) {
                            return { id: item.id, text: item.displayName };
                        });

                        mappedItems.unshift({ id: '', text: ' - ' });
                        return { results: mappedItems };
                    }
                }
            });
        });
publicApi.onOpen(function () {
            $('#ImporExport_ExportadorId').select2({
                dropdownParent: $('#ImporExportEditModal'),
                ajax: {
                    url: abp.appPath + 'api/app/impor-exports/exportador-lookup',
                    type: 'GET',
                    data: function (params) {
                        return { filter: params.term, maxResultCount: 10 }
                    },
                    processResults: function (data) {
                        var mappedItems = _.map(data.items, function (item) {
                            return { id: item.id, text: item.displayName };
                        });

                        return { results: mappedItems };
                    }
                }
            });
        });
publicApi.onOpen(function () {
            $('#ImporExport_ProductoId').select2({
                dropdownParent: $('#ImporExportEditModal'),
                ajax: {
                    url: abp.appPath + 'api/app/impor-exports/producto-lookup',
                    type: 'GET',
                    data: function (params) {
                        return { filter: params.term, maxResultCount: 10 }
                    },
                    processResults: function (data) {
                        var mappedItems = _.map(data.items, function (item) {
                            return { id: item.id, text: item.displayName };
                        });

                        return { results: mappedItems };
                    }
                }
            });
        });
publicApi.onOpen(function () {
            $('#ImporExport_PermisoRenov').select2({
                dropdownParent: $('#ImporExportEditModal'),
                ajax: {
                    url: abp.appPath + 'api/app/impor-exports/impor-export-lookup',
                    type: 'GET',
                    data: function (params) {
                        return { filter: params.term, maxResultCount: 10 }
                    },
                    processResults: function (data) {
                        var mappedItems = _.map(data.items, function (item) {
                            return { id: item.id, text: item.displayName };
                        });

                        mappedItems.unshift({ id: '', text: ' - ' });
                        return { results: mappedItems };
                    }
                }
            });
        });

        
    };

    return {
        initModal: initModal
    };
};
