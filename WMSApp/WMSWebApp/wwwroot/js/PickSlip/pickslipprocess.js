

//$(function () {
//    var docType = $("#doctype").val();
//    $("#grn").select2({
//        theme: 'bootstrap4',
//        /* minimumInputLength: 2,*/
//        /*templateResult: formatState, //this is for append country flag.*/
//        ajax: {
//            url: '/PickSlip/PoList?docType=' + docType,
//            dataType: 'json',
//            type: "GET",
//            //data: function (term) {
//            //    return {
//            //        term: term
//            //    };
//            //},
//            processResults: function (data) {
//                /*console.log(data);*/
//                return {
//                    results: $.map(data, function (item) {
//                        console.log(item);
//                        return {
//                            text: item.GRNId,
//                            id: item.GRNId,
//                        }
//                    })
//                };
//            }

//        }
//    });


//});
$(document).ready(function () {
    var sn = 1;

    $("#doctype").change(function () {
        console.log("doctype change");
        PoNumber($(this).val());
    });


    $("#grn").change(function () {
        itemList($(this).val(), $("#doctype").val());
    });
    $("#items").change(function () {
        console.log($("#items option:selected").data('areaid'));
        area($("#items option:selected").data('areaid'));
    });

    $("#save").click(function () {
        var items = $('#pick-grid').data().kendoGrid.dataSource.data();
        console.log(items);
        if (items.length > 0) {

            var list = [];
            for (var i = 0; i < items.length; i++) {
                list.push[items[i].init];
            }

            var settings = {
                "url": "/PickSlip/Save",
                "method": "POST",
                "timeout": 0,
                "headers": {
                    "Content-Type": "application/json"
                },
                "data": JSON.stringify(list),
            };

            $.ajax(settings).done(function (response) {
                hideloading();
                window.location = "/Grn/List";
            }).fail(function () {

                hideloading();
                alert("something went wrong please try again.");
            });
        }
        else {
            alert("Please Add item in list.");
            return false;
        }
    });

    $("#add-new").click(function () {
        var grnid = $("#grn").val();
        var category = $("#doctype").val();
        $.ajax({
            type: "GET",
            url: "/PickSlip/GetPoProduct?id=" + grnid + "&docType=" + category,
            data: "{}",
            success: function (data) {
                console.log(data);
                var grid = $("#pick-grid").data("kendoGrid");
                grid.dataSource.add(data);
                toastr.success('Item  successfully added into list.');
            }
        });



    });

    var initialLoad = true;
    $("#pick-grid").kendoGrid({


        dataSource: {

            transport: {
                read: {
                    url: "/PickSlip/List",
                    type: "POST",
                    dataType: "json",
                    data: additionalData,
                    complete: function (result) {
                        console.log("Remote built-in transport", result);
                        if (result.status == 401) {
                            /* document.location.href = "@Html.Raw(Url.Action("Index", "AccessDenied"))";*/
                        }
                    }

                }
            },
            schema: {
                data: "Data",
                total: "Total",
                errors: "Errors"
            },
            error: function (e) {
                //display_kendoui_grid_error(e);
                // Cancel the changes
                console.log(e)
                this.cancelChanges();
            },
            pageSize: 20,
            sortable: true,
            serverPaging: true,
            serverSorting: true,
            requestStart: function () {
                if (initialLoad) //<-- if it's the initial load, manually start the spinner
                    kendo.ui.progress($("#booking-grid"), true);
            },
            requestEnd: function () {
                if (initialLoad)
                    kendo.ui.progress($("#booking-grid"), false);
                initialLoad = false; //<-- make sure the spinner doesn't fire again (that would produce two spinners instead of one)

            },

        },
        //selectable: 'raw',
        //change: onChange,
        height: 350,
        pageable: {
            refresh: true,
            pageSizes: true
        },

        scrollable: false,
        columns: [{
            field: "Id",
            title: "Id",

            width: 50
        },
        {
            field: "PONumber",
            title: "PONumber",
            width: 100
        },
        {
            field: "SubItemCode",
            title: "SubItemCode",
            width: 100
        },

        {
            field: "SubItemName",
            title: "SubItemName",
            width: 120
        },
        {
            field: "AreaId",
            title: "AreaId",
        },
        {
            field: "Location",
            title: "Location",
            width: 300
        },
        {
            field: "Qty",
            title: "Qty",
            encoded: false,
            width: 120
        },
            //{
            //    field: "Unit",
            //    title: "Unit",
            //    width: 100
            //},


            //{
            //    field: "Amt",
            //    title: "Amt",
            //    width: 100
            //},


        ],

    });





});



function additionalData() {
    var data = {


    }
    addAntiForgeryToken(data);
    return data;
}

function itemList(id, doctype) {
    $.ajax({
        type: "GET",
        url: "/PickSlip/GetPoProduct?id=" + id + "&docType=" + doctype,
        data: "{}",
        success: function (data) {
            var s = '';
            for (var i = 0; i < data.length; i++) {
                s += '<option value="' + data[i].Id + '" data-code=' + data[i].SubItemCode + ' data-itemname=' + data[i].SubItemName +
                    ' data-areaid=' + data[i].AreaId + ' >' + data[i].SubItemName + '</option>';
            }
            $("#items").html(s);
            $("#items").trigger("change");
        }
    });

    /*area($("#items option:selected").data('areaid'));*/
}
function area(areaId) {
    console.log(areaId);
    $.ajax({
        type: "GET",
        url: "/PickSlip/GetLocation?areaId=" + areaId,
        data: "{}",
        success: function (data) {
            console.log(data);
            $("#location").val(data);

        }
    });

}

function PoNumber(docType) {

    $.ajax({
        type: "GET",
        url: "/PickSlip/PoList?docType=" + docType,
        data: "{}",
        success: function (data) {
            console.log(data);
            var s = '';
            for (var i = 0; i < data.length; i++) {
                s += '<option value="' + data[i].PoNumber + '" >' + data[i].PoNumber + '</option>';
            }
            $("#grn").html(s);
            $("#grn").trigger("change");
        }
    });
}
function pleaseWait() {
    $('#pleaseWait').modal({
        backdrop: 'static',
        keyboard: false
    })
}

function hideloading() {
    $('#pleaseWait').on('shown.bs.modal', function (e) {
        $(this).hide();
        $('.modal').modal('hide');
    })
}