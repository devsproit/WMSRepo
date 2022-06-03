'use strict'
$(function () {
    $("#ponumber").select2({
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

$(document).ready(function () {

    warehouse();
    $("#WarehouseId").change(function () {
        var element = $(this).find('option:selected');
        var myTag = element.attr("data-code");

        $('#wcode').val(myTag);
    });
    $("#WarehouseId").trigger("change");


    $("#zone").change(function () {
        var element = $(this).find('option:selected');
        var myTag = element.attr("data-code");

        $('#zcode').val(myTag);
        area($("#WarehouseId").val(), $("#zone").val());
    });
    $("#zone").trigger("change");

    $("#Warea").change(function () {
        var element = $(this).find('option:selected');
        var myTag = element.attr("data-code");

        $('#acode').val(myTag);
       
    });
    $("#Warea").trigger("change");



    $('#ponumber').on('change', function (e) {
        $("#ponumber_details").val($("#ponumber").val());
        var grid = $('#myGrid').data('kendoGrid');
        grid.dataSource.page(1);
    });


    var initialLoad = true;
    $("#myGrid").kendoGrid({


        dataSource: {

            transport: {
                read: {
                    url: "/Grn/PODetails",
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
        selectable: 'raw',
        change: onChange,
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
            field: "Qty",
            title: "Qty",
            encoded: false,
            width: 120
        },
        {
            field: "Unit",
            title: "Unit",
            width: 100
        },
        {
            field: "MaterialDescription",
            title: "MaterialDescription",
            width: 300
        },

        {
            field: "Amt",
            title: "Amt",
            width: 100
        },


        ],

    });


});
function onChange(arg) {
    //var selected = $.map(this.select(), function (item) {
    //    return $(item);
    //});
    var rows = arg.sender.select(),
        items = [];

    rows.each(function (arg) {
        var grid = $('#myGrid').data('kendoGrid');
        var dataItem = grid.dataItem(this);
        items.push(dataItem);
    });
    fildetails(items);
    console.log(items[0]['Amt']);
}

function additionalData() {
    var data = {
        pono: $("#ponumber").val(),

    }
    addAntiForgeryToken(data);
    return data;
}

function fildetails(items) {
    $("#Material").val(items[0]['MaterialDescription']);
    $("#MaterialCode").val(items[0]['SubItemCode']);
    $("#qtyu,#Qtysuk,#QtyD,#QtyO,#QtyI").val(items[0]['Qty']);
}

function warehouse() {
    $.ajax({
        type: "GET",
        url: "/Grn/Warehouse",
        async: false,
        success: function (data) {
            var s = '';
            for (var i = 0; i < data.length; i++) {
                s += '<option value="' + data[i].Id + '" data-code=' + data[i].WarehouseCode + '>' + data[i].WarehouseName + '</option>';
            }
            $("#WarehouseId").html(s);
        }
    });


    zones($("#WarehouseId").val());

}

function zones(warehouseId) {
    $.ajax({
        type: "GET",
        url: "/Grn/WarehouseZone?warehouseid=" + warehouseId,
        data: "{}",
        success: function (data) {
            var s = '';
            for (var i = 0; i < data.length; i++) {
                s += '<option value="' + data[i].Id + '" data-code=' + data[i].ZoneCode + '>' + data[i].ZoneName + '</option>';
            }
            $("#zone").html(s);
            $("#zone").trigger("change");
        }
    });
    
    area($("#WarehouseId").val(), $("#zone").val());

}
function area(warehouseId, zoneid) {
    $.ajax({
        type: "GET",
        url: "/Grn/WarehouseArea?warehouseid=" + warehouseId + '&zoneid=' + zoneid,
        data: "{}",
        success: function (data) {
            var s = '';
            for (var i = 0; i < data.length; i++) {
                s += '<option value="' + data[i].Id + '" data-code=' + data[i].AreaCode + '  data-size=' + data[i].Size + '>' + data[i].AreaName + '</option>';
            }
            $("#Warea").html(s);
            $("#Warea").trigger("change");
        }
    });
   
}

