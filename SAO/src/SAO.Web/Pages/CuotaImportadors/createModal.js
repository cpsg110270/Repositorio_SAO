var abp = abp || {};

abp.modals.cuotaImportadorCreate = function () {
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
        
        $('#ImportadorLookupOpenButton').on('click', '', function () {
            lastNpDisplayNameId = 'Importador_NombreImportador';
            lastNpIdId = 'Importador_Id';
            _lookupModal.open({
                currentId: $('#Importador_Id').val(),
                currentDisplayName: $('#Importador_NombreImportador').val(),
                serviceMethod: function() {
                    
                    return window.sAO.cuotaImportadors.cuotaImportadors.getImportadorLookup;
                }
            });
        });
        
        
    };

    return {
        initModal: initModal
    };
};
