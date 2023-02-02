'use strict'
$(function () {
    $("#pickslip").select2({
        theme: 'bootstrap4',
        /* minimumInputLength: 2,*/
        /*templateResult: formatState, //this is for append country flag.*/
        ajax: {
            url: '/Invoice/GetPickSlip',
            dataType: 'json',
            type: "GET",
            data: function (term) {
                return {
                    term: term.term
                };
            },
            processResults: function (data) {
                console.log(data);
                return {
                    results: $.map(data, function (item) {
                        return {
                            text: item.Id,
                            id: item.Id,
                        }
                    })
                };
            }

        }
    });


});

$(document).ready(function () {
    var sn = 1;




    $("#pickslip").change(function () {
        itemList($(this).val());
    });
    $("#items").change(function () {
        console.log($("#items option:selected").data('areaid'));
        area($("#items option:selected").data('areaid'));
    });

    $("#finalsave").click(function () {
       
        var items = $('#pick-grid').data().kendoGrid.dataSource.data();
        if ($("#pickslip").val().length > 0) {

        }
        else {
            hideloading();
            alert("Select pickslip to Invocie");
            return false;
        }

        if ($("#invoiceNo").val().length > 0) {

        }
        else {
            hideloading();
            alert("Please enter Invocie.");
            return false;
        }
        console.log(items);
        if (items.length > 0) {
            pleaseWait();
            var list = {
                PickSlipId: $("#pickslip").val(),
                InvoiceId: $("#invoiceNo").val()
            }

            var settings = {
                "url": "/Invoice/Save",
                "method": "POST",
                "timeout": 0,
                "headers": {
                    "Content-Type": "application/json"
                },
                "data": JSON.stringify(list),
            };

            $.ajax(settings).done(function (response) {
                hideloading();
                window.location = "/Invoice/List";
            }).fail(function () {

                hideloading();
                alert("something went wrong please try again.");
            });
        }

    });



    var initialLoad = true;
    $("#pick-grid").kendoGrid({


        dataSource: {

            transport: {
                read: {
                    url: "/Invoice/InvoiceList",
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
                    kendo.ui.progress($("#pick-grid"), true);
            },
            requestEnd: function () {
                if (initialLoad)
                    kendo.ui.progress($("#pick-grid"), false);
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
            width: 250
        },
        {
            field: "Qty",
            title: "Qty",

            width: 50
        },
        {
            field: "Unit",
            title: "Unit",
            width: 50
        },


        {
            field: "Amount",
            title: "Amount",
            width: 50
        },
        {
            field: "AreaId",
            title: "AreaId",
            hidden: true,
            width: 50
        },
        {
            field: "MaterialDescription",
            title: "MaterialDescription",
            width: 50,
            hidden: true,

        },
        {
            field: "AreaCode",
            title: "AreaCode",
            width: 50,
            hidden: true,

        },
        {
            field: "AreaName",
            title: "AreaName",
            width: 50,
            hidden: true,

        },
        {
            field: "ZoneCode",
            title: "ZoneCode",
            width: 50,
            hidden: true,

        },
        {
            field: "ZoneName",
            title: "ZoneName",
            width: 50,
            hidden: true,

        },
        {
            field: "Warehouse",
            title: "Warehouse",
            width: 50,
            hidden: true,

        },
        {
            field: "WarehouseCode",
            title: "WarehouseCode",
            width: 50,
            hidden: true,

        },
        {
            field: "PoNumber",
            title: "PoNumber",
            width: 50,
            hidden: true,

        },
        

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
    pleaseWait();
    $.ajax({
        type: "GET",
        url: "/Invoice/GetItems?id=" + id,
        data: "{}",
        success: function (data) {
            var grid = $("#pick-grid").data('kendoGrid');

            grid.dataSource.data([]);
            grid.setDataSource([]);
            var sn = 1;
            for (var i = 0; i < data.length; i++) {
                var newRow = {
                    Id: sn,

                    SubItemCode: data[i].SubItemCode,
                    SubItemName: data[i].SubItemName,
                    Qty: data[i].Qty,
                    Amount: data[i].Amount,
                    AreaId: data[i].AreaId,
                    MaterialDescription: data[i].MaterialDescription,
                    AreaCode: data[i].AreaCode,
                    AreaName: data[i].AreaName,
                    ZoneCode: data[i].ZoneCode,
                    ZoneName: data[i].ZoneName,
                    Warehouse: data[i].Warehouse,
                    WarehouseCode: data[i].WarehouseCode,
                    PoNumber: data[i].PoNumber
                };

                var grid = $("#pick-grid").data("kendoGrid");
                grid.dataSource.add(newRow);
                sn++;
            }

            hideloading();
            toastr.success('PickSlip Item successfully added into list.');
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
function onChange(arg) {
    //var selected = $.map(this.select(), function (item) {
    //    return $(item);
    //});
    var rows = arg.sender.select(),
        items = [];

    rows.each(function (arg) {
        var grid = $('#pick-grid').data('kendoGrid');
        var dataItem = grid.dataItem(this);
        items.push(dataItem);
    });
    fildetails(items);
    console.log(items[0]['Amt']);
}

function fildetails(items) {
    $("#Material").val(items[0]['MaterialDescription']);
    $("#MaterialCode").val(items[0]['SubItemCode']);
    $("#qtyu,#Qtysuk,#QtyD,#QtyO,#QtyI").val(items[0]['Qty']);
    $("#sendercompany").val(items[0]["SenderCompany"]);
    $("#sender").val(items[0]["Branch"]);
    $("#WarehouseId").val(items[0]["Warehouse"]);
    $("#zone").val(items[0]["ZoneName"]);
    $("#Warea").val(items[0]["AreaName"]);
    $("#wcode").val(items[0]["WarehouseCode"]);
    $("#zcode").val(items[0]["ZoneCode"]);
    $("#acode").val(items[0]["AreaCode"]);
    $("#ponumber_details").val(items[0]["PoNumber"]);




}

function pleaseWait() {
    $('#pleaseWait').modal({
        backdrop: 'static',
        keyboard: false
    })
}

function hideloading() {
    $('#pleaseWait').modal('hide');
    $('#pleaseWait').on('shown.bs.modal', function (e) {

        $('.modal').modal('hide');
    })
}