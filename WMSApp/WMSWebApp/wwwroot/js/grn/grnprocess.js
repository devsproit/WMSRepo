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

var grndata = [];

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
    $("#doctype").on('change', function () {
        if ($(this).val() === "PO") {
            $("#ponumber-area").show();
        }
        else {
            $("#ponumber-area").hide();
        }
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
                        //debugger;
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

        scrollable: true,
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

    $("#addtoarea").click(function () {
        var grid = $("#myGrid").data("kendoGrid");
        if (validation()) {


            var selectedItem = grid.dataItem(grid.select());
            if (grndata.length > 0) {
                var hasMatch = false;
                for (var i = 0; i < grndata.length; i++) {
                    var loc = grndata[i];
                    if (loc["ItemId"] == selectedItem["Id"]) {
                        hasMatch = true;
                        loc["ItemId"] = selectedItem["Id"];
                        loc["zone"] = $("#zone").val();
                        loc["WarehouseId"] = $("#WarehouseId").val();
                        loc["Warea"] = $("#Warea").val();
                        loc["Remark"] = $("#Remark").val();
                        loc["wcode"] = $("#wcode").val();
                        loc["zcode"] = $("#zcode").val();
                        loc["acode"] = $("#acode").val();
                        loc["Ponumber"] = $("#ponumber").val();
                        loc["invoice"] = $("#invoiceNo").val();
                        toastr.success('Item ' + selectedItem["Id"] + ' successfully updated on area code' + loc["acode"] + ' in zone ' + loc["zone"]);
                    }
                    //else {
                    //    var location = {};
                    //    location["ItemId"] = selectedItem["Id"];
                    //    location["zone"] = $("#zone").val();
                    //    location["WarehouseId"] = $("#WarehouseId").val();
                    //    location["Warea"] = $("#Warea").val();
                    //    location["Remark"] = $("#Remark").val();
                    //    location["wcode"] = $("#wcode").val();
                    //    location["zcode"] = $("#zcode").val();
                    //    location["acode"] = $("#acode").val();
                    //    location["Ponumber"] = $("#ponumber").val();
                    //    location["invoice"] = $("invoiceNo").val();
                    //    grndata.push(location);
                    //}
                }
                if (!hasMatch) {
                    var location = {};
                    location["ItemId"] = selectedItem["Id"];
                    location["zone"] = $("#zone").val();
                    location["WarehouseId"] = $("#WarehouseId").val();
                    location["Warea"] = $("#Warea").val();
                    location["Remark"] = $("#Remark").val();
                    location["wcode"] = $("#wcode").val();
                    location["zcode"] = $("#zcode").val();
                    location["acode"] = $("#acode").val();
                    location["Ponumber"] = $("#ponumber").val();
                    location["invoice"] = $("#invoiceNo").val();
                    grndata.push(location);
                    toastr.success('Item ' + selectedItem["Id"] + ' successfully added on area code' + location["acode"] + ' in zone ' + location["zone"]);
                }
            } else {
                var location = {};
                location["ItemId"] = selectedItem["Id"];
                location["zone"] = $("#zone").val();
                location["WarehouseId"] = $("#WarehouseId").val();
                location["Warea"] = $("#Warea").val();
                location["Remark"] = $("#Remark").val();
                location["wcode"] = $("#wcode").val();
                location["zcode"] = $("#zcode").val();
                location["acode"] = $("#acode").val();
                location["Ponumber"] = $("#ponumber").val();
                location["invoice"] = $("#invoiceNo").val();
                grndata.push(location);
                toastr.success('Item ' + selectedItem["Id"] + ' successfully added on area code' + location["acode"] + ' in zone ' + location["zone"]);
            }



            console.log(location);
        }
    });

    $("#finalsave").click(function () {
        if (validation()) {
            var grid = $('#myGrid').data('kendoGrid');
            if (grid._data.length !== grndata.length) {
                alert("Please add all items into warehouse areas.");
                return false;
            }
            else {
                pleaseWait();
                var settings = {
                    "url": "/Grn/Complete",
                    "method": "POST",
                    "timeout": 0,
                    "headers": {
                        "Content-Type": "application/json"
                    },
                    "data": JSON.stringify(grndata),
                };

                $.ajax(settings).done(function (response) {
                    hideloading();
                    window.location = "/Grn/List";
                }).fail(function () {

                    hideloading();
                    alert("something went wrong please try again.");
                });
            }
        }
        else {

        }
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
    $("#sendercompany").val(items[0]["SenderCompany"]);
    $("#sender").val(items[0]["Branch"]);
    
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

function validation() {
    var grid = $('#myGrid').data('kendoGrid');
    if ($("#doctype").val() === "PO") {

        if ($("#ponumber").val() == undefined || $("#ponumber").val() === "") {
            alert("Please select PO number.");
            return false;
        }
        if ($("#invoiceNo").val() === undefined || $("#invoiceNo").val() === "") {
            alert("Please enter invoice no.");
            return false;
        }
    }
    else {

        if ($("#invoiceNo").val() === undefined || $("#invoiceNo").val() === "") {
            alert("Please enter invoice no.");
            return false;
        }

        if (grid._data.length == 0) {
            alert("Please add items.");
            return false;
        }

    }


    return true;
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
