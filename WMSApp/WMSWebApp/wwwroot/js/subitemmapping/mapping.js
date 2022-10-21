'use strict'
$(function () {
    $("#SubItemCode").select2({
        theme: 'bootstrap4',
        /* minimumInputLength: 2,*/
        /*templateResult: formatState, //this is for append country flag.*/
        ajax: {
            url: '/Grn/PendingPO',
            dataType: 'json',
            type: "GET",
            //data: function (term) {
            //    return {
            //        term: term
            //    };
            //},
            processResults: function (data) {
                console.log(data);
                return {
                    results: $.map(data, function (item) {
                        return {
                            text: item,
                            id: item,
                        }
                    })
                };
            }

        }
    });


});