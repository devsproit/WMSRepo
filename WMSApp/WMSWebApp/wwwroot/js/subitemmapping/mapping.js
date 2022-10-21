'use strict'
$(function () {
    $("#SubItemCode").select2({
        theme: 'bootstrap4',
        /* minimumInputLength: 2,*/
        /*templateResult: formatState, //this is for append country flag.*/
        ajax: {
            url: '/SubItemMapping/GetSubItem',
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
                            text: item.Name,
                            id: item.Code,
                        }
                    })
                };
            }

        }
    });


});


$(document).ready(function () {

    warehouse();
    $("#WareHouseId").change(function () {
        var element = $(this).find('option:selected');
        var myTag = element.attr("data-code");

       
    });
    $("#WareHouseId").trigger("change");
    $("#WareHouseAreaId").change(function () {
        var element = $(this).find('option:selected');
        var myTag = element.attr("data-code");

       
        area($("#WareHouseId").val(), $("#WareHouseAreaId").val());
    });
    $("#WareHouseAreaId").trigger("change");

    $("#LocationId").change(function () {
        var element = $(this).find('option:selected');
        var myTag = element.attr("data-code");

        

    });
    $("#LocationId").trigger("change");
});

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
            $("#WareHouseId").html(s);
        }
    });


    zones($("#WareHouseId").val());

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
            $("#WareHouseAreaId").html(s);
            $("#zoWareHouseAreaIdne").trigger("change");
        }
    });

    area($("#WareHouseId").val(), $("#WareHouseAreaId").val());

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
            $("#LocationId").html(s);
            $("#LocationId").trigger("change");
        }
    });

}