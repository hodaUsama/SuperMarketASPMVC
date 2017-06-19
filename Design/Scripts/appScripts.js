$('[dataautocomplete]').each(function () {
        $(this).autocomplete({ source: $(this).attr('dataautocomplete') });
});

$('[datepckr]').datepicker({ dateFormat: 'dd-M-yy' });

$(document).ready(function () {


    $("#btnAddAll").click(function () {
        $("#lstAll option").appendTo("#lstselected");
        $("#lstselected option").attr("selected", false);
    });
    
    $("#btnAdd").click(function () {
        $('#lstAll').find('option:selected').appendTo('#lstselected');
    });

    $("#btnRemove").click(function () {
        $("#lstselected option:selected").appendTo("#lstAll");
        $("#lstAll option").attr("selected", false);
    });

    $("#btnRemoveAll").click(function () {
        $("#lstselected option").appendTo("#lstAll");
        $("#lstAll option").attr("selected", false);
    });

    $("#FrmCreate").submit(function (e) {
        $("#lstselected option").prop("selected", "selected");

    });



   


});






