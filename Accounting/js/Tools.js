function getUrlParameter(name) {
    name = name.replace(/[\[]/, '\\[').replace(/[\]]/, '\\]');
    var regex = new RegExp('[\\?&]' + name + '=([^&#]*)');
    var results = regex.exec(location.search);
    return results === null ? '' : decodeURIComponent(results[1].replace(/\+/g, ' '));
};

function BDatePickerByID(object) {
    $('#' + object.id).datepicker({
        format: "yyyy/mm/dd",
        autoclose: true,
        clearBtn: true,
        language: 'zh-TW'
    });
}