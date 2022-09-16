$(document).ready(function () {
    //$("#spAmt").format({ format: "#,###.00", locale: "us" });
    //$("#stpAmt").format({ format: "#,###.00", locale: "us" });
    //$("#sopAmt").format({ format: "#,###.00", locale: "us" });
    // $(this).format({ format: "#,###.00", locale: "us" });
    var t = $('#tblpo').DataTable();
    $('#doctype').on('change', function () {
       /// debugger;
        if ($(this).val() === 'StockTransfer_PO') {
            $('#StockTransferPO').css("display", "block");
            $('#SalePO').css("display", "none");
            $('#SRNPO').css("display", "none");
            $('#ServiceOrderPO').css("display", "none");
            SalePOCleanData();
            SrnPoCleanData();
            ServiceOrderPoCleanData();
        } else if ($(this).val() === 'Sale_PO') {
            $('#SalePO').css("display", "block");
            $('#StockTransferPO').css("display", "none");
            $('#SRNPO').css("display", "none");
            $('#ServiceOrderPO').css("display", "none");
            SrnPoCleanData();
            ServiceOrderPoCleanData();
            StockTransferPOCleanData();
        } else if ($(this).val() === 'SRN_PO') {
            $('#SRNPO').css("display", "block");
            $('#SalePO').css("display", "none");
            $('#StockTransferPO').css("display", "none");
            $('#ServiceOrderPO').css("display", "none");
            SalePOCleanData();
            // SrnPoCleanData();
            ServiceOrderPoCleanData();
            StockTransferPOCleanData();

        } else if ($(this).val() === 'ServiceOrder_PO') {
            $('#SRNPO').css("display", "none");
            $('#SalePO').css("display", "none");
            $('#StockTransferPO').css("display", "none");
            $('#ServiceOrderPO').css("display", "block");
            SalePOCleanData();
            SrnPoCleanData();
            //   ServiceOrderPoCleanData();
            StockTransferPOCleanData();

        }
        else {
            $('#StockTransferPO').css("display", "none");
            $('#SalePO').css("display", "none");
            $('#SRNPO').css("display", "none");
            $('#ServiceOrderPO').css("display", "none");
            SalePOCleanData();
            SrnPoCleanData();
            ServiceOrderPoCleanData();
            StockTransferPOCleanData();
        }
    });

    $('#btnAdd').on('click', function () {
       // debugger;
        var dropdownText = $('#doctype :selected').text();
        if (dropdownText == "Sale PO") {
            var stockTransferPOCatagry = $('#spCategory :selected').text();
            var stockTransferPoSendingTo = $('#spSendingTo :selected').text();//ItId
            var stockTransferPoItem = $("#spItem :selected").text();
            var stockTransferPoSubitem = $('#spSubItem :selected').text();
            var stockTransferPoQty = $("#spQty").val();
            var stockTransferPoAmt = $("#spAmt").val();
            var stockTransferPoSerialNumber = $("#spSerialNumber").val();
            var serviceCategory = "Not Applicable";
            var salePo = "Not Applicable";
            var saleDate = "Not Applicable";
            var ServiceRequestNumber = "Not Applicable";
            var subItemCode = $("#spSubItem").val();
            var invNumber = "Not Applicable";
        } else if (dropdownText == "StockTransfer PO") {
            var stockTransferPOCatagry = $('#stpCategory :selected').text();
            var stockTransferPoSendingTo = $('#stpSendingTo :selected').text();//ItId
            var stockTransferPoItem = $("#stpItem :selected").text();
            var stockTransferPoSubitem = $('#stpSubItem :selected').text();
            var stockTransferPoQty = $("#stpQty").val();
            var stockTransferPoAmt = $("#stpAmt").val();
            var stockTransferPoSerialNumber = $("#stpSerialNumber").val();
            var serviceCategory = "Not Applicable";
            var salePo = "Not Applicable";
            var saleDate = "Not Applicable";
            var ServiceRequestNumber = "Not Applicable";
            var subItemCode = $("#stpSubItem").val();
            var invNumber = "Not Applicable";
        }
        else if (dropdownText == "SRN PO") {
            debugger;
            var stockTransferPOCatagry = $('#srnCategory :selected').text();
            var stockTransferPoSendingTo = $('#srnSendingTo :selected').text();//ItId
            var stockTransferPoItem = $("#srnItem :selected").text();
            var stockTransferPoSubitem = $('#srnSubItem :selected').text();
            var stockTransferPoQty = $("#srnQty").val();
            var stockTransferPoAmt = $("#ddlSrnCause :selected").text();
            var stockTransferPoSerialNumber = $("#srnSerialNumber").val();
            var serviceCategory = "Not Applicable";
            var salePo = "Not Applicable";
            var saleDate = "Not Applicable";
            var ServiceRequestNumber = "Not Applicable";
            var subItemCode = $("#srnSubItem").val();
            var invNumber = $("#invNumber").val();
        }
        else if (dropdownText == "ServiceOrder PO") {
            var stockTransferPOCatagry = $('#sopCategory :selected').text();
            var stockTransferPoSendingTo = $('#sopSendingTo :selected').text();//ItId
            var stockTransferPoItem = $("#sopItem :selected").text();
            var stockTransferPoSubitem = $('#sopSubItem :selected').text();
            var stockTransferPoQty = $("#sopQty").val(); ////////
            var stockTransferPoAmt = $("#sopAmt").val();
            var ServiceRequestNumber = $("#sopServiceRequestNumber").val();
            var stockTransferPoSerialNumber = $("#sopSerialNumber").val();
            var serviceCategory = $("#sopServiceCatagry").val();
            var salePo = $("#sopSalePo").val();
            var saleDate = $("#sopSaleDate").val();
            var subItemCode = $("#sopSubItem").val();
            var invNumber = "Not Applicable";

        }
        ////   var purchaseOrder = $("#txtPurchaseOrder").val();StockTransferPOCatagry



        t.row.add([stockTransferPOCatagry, stockTransferPoSendingTo, stockTransferPoItem, subItemCode, stockTransferPoSubitem, stockTransferPoQty, stockTransferPoAmt, stockTransferPoSerialNumber, serviceCategory, salePo, saleDate, ServiceRequestNumber,invNumber]).draw();
    });

    //$('input[id="spQty"]').on('keypress', function (event) {
    //    var regex = new RegExp("/^\d*[.]?\d*$/");
    //    var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
    //    if (!regex.test(key)) {
    //        event.preventDefault();
    //        return false;
    //    }
    //});
    //$('input[id="srnQty"]').on('keypress', function (event) {
    //    var regex = new RegExp("/[^0-9]/g");
    //    var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
    //    if (!regex.test(key)) {
    //        event.preventDefault();
    //        return false;
    //    }
    //});
    //$('input[id="stpQty"]').on('keypress', function (event) {
    //    var regex = new RegExp("^[a-zA-Z0-9]+$");
    //    var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
    //    if (!regex.test(key)) {
    //        event.preventDefault();
    //        return false;
    //    }
    //});
    //$('input[id="sopQty"]').on('keypress', function (event) {
    //    var regex = new RegExp("^[a-zA-Z0-9]+$");
    //    var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
    //    if (!regex.test(key)) {
    //        event.preventDefault();
    //        return false;
    //    }
    //});

    $("#finalsave").click(function () {
        debugger;
        var drpdown = $('#doctype :selected').text();

        if (drpdown == "Sale PO") {
            jsonObj = [];
            var table = $('#tblpo').DataTable();
            var data = table.data();
            for (var i = 0; i < data.length; i++) {
                myData = {};
                myData["SalePOCategory"] = data[i][0];
                myData["SalePOSendingTo"] = data[i][1];//senderCompany
                myData["SalePOItem"] = data[i][2];
                myData["SalePOSubItem"] = data[i][4];
                myData["SubItemCode"] = data[i][3]
                myData["SalePOQty"] = data[i][5];//
                myData["SalePOAmt"] = data[i][6];
                myData["SalePOSerialNumber"] = data[i][7];//
                myData["invNumber"] = data[i][12];
                jsonObj.push(myData);
            }
            var data = {
                //  PODate: $("#PoDate").val(),
                PONumber: $("#PONumber").val(),
                POCatrgory: drpdown

            };
            data.salePOCategories = jsonObj;
            data.stockTransferCategories = null;
            data.serviceOrderCategories = null;
            data.sRNPOCategories = null;
        }
        else if (drpdown == "StockTransfer PO") {
            jsonObj = [];
            var table = $('#tblpo').DataTable();
            var data = table.data();
            for (var i = 0; i < data.length; i++) {
                myData = {};
                myData["StockTransferPOCategory"] = data[i][0];
                myData["StockTransferPOSendingTo"] = data[i][1];//senderCompany
                myData["StockTransferPOItem"] = data[i][2];
                myData["StockTransferPOSubItem"] = data[i][4];
                myData["SubItemCode"] = data[i][3]
                myData["StockTransferPOQty"] = data[i][5];//
                myData["StockTransferPOAmt"] = data[i][6];
                myData["StockTransferPOSerialNumber"] = data[i][7];//
                myData["invNumber"] = data[i][8];
                jsonObj.push(myData);
            }
            console.log(jsonObj);
            var data = {
                //  PODate: $("#PoDate").val(),
                PONumber: $("#PONumber").val(),
                POCatrgory: drpdown

            };
            data.salePOCategories = null;
            data.stockTransferCategories = jsonObj;
            data.serviceOrderCategories = null;
            data.sRNPOCategories = null;

        }
        else if (drpdown == "ServiceOrder PO") {
            jsonObj = [];
            var table = $('#tblpo').DataTable();
            var data = table.data();
            for (var i = 0; i < data.length; i++) {
                myData = {};
                myData["ServiceOrderPOCategory"] = data[i][0];
                myData["ServiceOrderPOSendingTo"] = data[i][1];//senderCompany
                myData["ServiceOrderPOItem"] = data[i][2];
                myData["ServiceOrderPOSubitem"] = data[i][4];
                myData["SubItemCode"] = data[i][3]
                myData["ServiceOrderPOQty"] = data[i][5];//
                myData["ServiceOrderPOAmt"] = data[i][6];
                myData["ServiceOrderPOSerialNumber"] = data[i][7];//
                myData["invNumber"] = data[i][8];
                jsonObj.push(myData);
            }
            console.log(jsonObj);
            var data = {
                //  PODate: $("#PoDate").val(),
                PONumber: $("#PONumber").val(),
                POCatrgory: drpdown

            };
            data.salePOCategories = null;
            data.stockTransferCategories = null;
            data.serviceOrderCategories = jsonObj;
            data.sRNPOCategories = null;

        }
        else if (drpdown == "SRN PO") {
            jsonObj = [];
            var table = $('#tblpo').DataTable();
            var data = table.data();
            for (var i = 0; i < data.length; i++) {
                myData = {};
                myData["SrnPOCategory"] = data[i][0];
                myData["SrnPOSendingTo"] = data[i][1];//senderCompany
                myData["SrnPOItem"] = data[i][2];
                myData["SrnPOSubItem"] = data[i][4];
                myData["SubItemCode"] = data[i][3]
                myData["SrnPOQty"] = data[i][5];//
                myData["SrnPOSrnCause"] = data[i][6];
                myData["SrnSerialNumber"] = data[i][7];//
                myData["invNumber"] = data[i][8]; 
                jsonObj.push(myData);
            }
            console.log(jsonObj);
            var data = {
                PONumber: $("#PONumber").val(),
                POCatrgory: drpdown

            };
            data.salePOCategories = null;
            data.stockTransferCategories = null;
            data.serviceOrderCategories = null;
            data.sRNPOCategories = jsonObj;
        }

        //$.ajax(
        //    {
        //        type: 'POST',
        //        url: '/Po/Create',
        //        contentType: "application/json",
        //        dataType: 'json',
        //        data: JSON.stringify(data),
        //        success: function (result) {
        //            if (result.message)
        //                alert("data inserted");
        //            table.clear().draw();
        //            SalePOCleanData();
        //            SrnPoCleanData();
        //            ServiceOrderPoCleanData();
        //            StockTransferPOCleanData();
        //        },
        //        error: function (result) {
        //            alert('error');
        //        }
        //    });

        console.log(data);
        var settings = {
            "url": "/Po/Create",
            "method": "POST",
            /* "timeout": 0,*/
            "headers": {
                "Content-Type": "application/json; charset=UTF-8"
            },
            //"headers": {
            //    "Content-Type": "application/json"
            //},
            "data": JSON.stringify(data),

        };

        $.ajax(settings).done(function (response) {
            if (response.message == "Saved Successfully") {
                alert("data inserted");
                table.clear().draw();
                SalePOCleanData();
                SrnPoCleanData();
                ServiceOrderPoCleanData();
                StockTransferPOCleanData();
            }

        })
    });
    
    $('#sopSaleDate').datepicker({
        changeMonth: true,
        changeYear: true,
        format: "dd/mm/yyyy",
        language: "tr"
    });
  
});


$("#spCategory").change(function () {
   // debugger;
    var type = $('#doctype :selected').text();
    var catogry = $('#spCategory :selected').text();
    getSubItemId(catogry, type);
});
$("#srnCategory").change(function () {
   // debugger;
    var type = $('#doctype :selected').text();
    var catogry = $('#srnCategory :selected').text();
    getSubItemId(catogry, type);
});
$("#sopCategory").change(function () {
   // debugger;
    var type = $('#doctype :selected').text();
    var catogry = $('#sopCategory :selected').text();
    getSubItemId(catogry, type);
});


var getSubItemId = function (catogry, type) {
   // debugger;
    $.ajax({
        url: '/Po/GetSendingTo',//'@Url.Action("GetSubItem","Intrasit")',
        type: 'GET',
        data: {
            category: catogry,
        },
        success: function (data) {
            //debugger;
            //$('#SubItemId').find('option').remove()
            console.log(data);
            if (type == "Sale PO") {
                $('#spSendingTo').find('option').remove().end().append('<option value="0">Select</option>');
                if (data.length != 0) {
                    $(data).each(
                        function (index, item) {
                            $('#spSendingTo').append('<option value="' + item.Id + '">' + item.CustomerName + '</option>')
                        });
                } else {
                    //  $('#subItemId').find('option').remove().end().append('<option value="0">Select</option>');
                    //  $('#txtMDesc').val('');
                    // $('#subItemId option:eq(0)').attr('selected', 'selected')
                }
            }
            else if (type == "ServiceOrder PO") {
                $('#sopSendingTo').find('option').remove().end().append('<option value="0">Select</option>');
                if (data.length != 0) {
                    $(data).each(
                        function (index, item) {
                            $('#sopSendingTo').append('<option value="' + item.Id + '">' + item.CustomerName + '</option>')
                        });
                } else {
                    $('#sopSendingTo').find('option').remove().end().append('<option value="0">Select</option>');
                }

            }
            else if (type == "SRN PO") {
                //debugger;
                $('#srnSendingTo').find('option').remove().end().append('<option value="0">Select</option>');
                if (data.length != 0) {
                    $(data).each(
                        function (index, item) {
                            $('#srnSendingTo').append('<option value="' + item.Id + '">' + item.CustomerName + '</option>')
                        });
                } else {
                    $('#srnSendingTo').find('option').remove().end().append('<option value="0">Select</option>');
                }
            }

        },
        error: function () {
        }
    });
}

$("#spSubItem").change(function () {
   // debugger;
    var type = $('#doctype :selected').text();
    var catogry = $('#spSubItem :selected').text();
    getamt(catogry, type);
});
$("#stpSubItem").change(function () {
    //debugger;
    var type = $('#doctype :selected').text();
    var catogry = $('#stpSubItem :selected').text();
    getamt(catogry, type);
});
$("#sopSubItem").change(function () {
    // debugger;
    var type = $('#doctype :selected').text();
    var catogry = $('#sopSubItem :selected').text();
    getamt(catogry, type);
});
var getamt = function (catogry, type) {
    //debugger;
    $.ajax({
        url: '/Po/GetAmtTo',//'@Url.Action("GetSubItem","Intrasit")',
        type: 'GET',
        data: {
            subName: catogry,
            type: type
        },
        success: function (data) {
            //debugger;
            //$('#SubItemId').find('option').remove()
            console.log(data);
            if (type == "Sale PO") {
                if (data.length != 0) {
                    $("#spAmt").val(data.CustomerPrice);
                }
            }
            else if (type == "ServiceOrder PO") {
                if (data.length != 0) {
                    // $("#stpAmt").val("");
                    $("#sopAmt").val(data.CustomerPrice);
                }
            }
            else if (type == "StockTransfer PO") {
                if (data.length != 0) {
                    $("#stpAmt").val(data.BranchPrice);
                }
            }
        },
        error: function () {
        }
    });
}
//GetAmtTo



function ServiceOrderPoCleanData() {
    //  $('#sopCategory').find('option').remove().end().append('<option value="0">Select</option>');
    // $('#sopSendingTo').find('option').remove().end().append('<option value="0">Select</option>');
    //$('#sopItem').find('option').remove().end().append('<option value="0">Select</option>');
    //$('#sopSubItem').find('option').remove().end().append('<option value="0">Select</option>');
    $('#sopCategory').val(0);
    $('#sopSendingTo').val(0);
    $('#sopItem').val(0);
    $('#sopSubItem').val(0);
    $("#sopQty").val("");
    $("#sopServiceRequestNumber").val(0);
    $("#sopSalePo").val("");
    $("#sopSaleDate").val("");
    $("#sopAmt").val("");
    $("#sopSerialNumber").val("");
    var t = $('#tblpo').DataTable();
    t.clear().draw();

}
function SrnPoCleanData() {
    $('#srnCategory').val(0);
    // $('#srnCategory').find('option').remove().end().append('<option value="0">Select</option>');
    // $('#srnSendingTo').find('option').remove().end().append('<option value="0">Select</option>');
    //  $('#srnItem').find('option').remove().end().append('<option value="0">Select</option>');
    //$('#srnSubItem').find('option').remove().end().append('<option value="0">Select</option>');
    $('#srnSendingTo').val(0);
    $('#srnItem').val(0);
    $('#srnSubItem').val(0);
    $("#srnQty").val("");
    $("#ddlSrnCause").val(0);
    $("#srnSerialNumber").val("");
    $("#invNumber").val("");
    var t = $('#tblpo').DataTable();
    t.clear().draw();
}
function StockTransferPOCleanData() {
    $('#stpCategory').val(0);
    $('#stpSendingTo').val(0);
    $('#stpItem').val(0);
    $("#stpSubItem").val(0);
    //  $('#stpCategory').find('option').remove().end().append('<option value="0">Select</option>');
    // $('#stpSendingTo').find('option').remove().end().append('<option value="0">Select</option>');
    // $('#stpItem').find('option').remove().end().append('<option value="0">Select</option>');
    //   $('#stpSubItem').find('option').remove().end().append('<option value="0">Select</option>');
    $("#stpQty").val("");
    $("#stpAmt").val("");
    $("#stpSerialNumber").val("");
    var t = $('#tblpo').DataTable();
    t.clear().draw();

}
function SalePOCleanData() {

    $('#spCategory').val(0);
    // $('#spCategory').find('option').remove().end().append('<option value="0">Select</option>');
    // $('#spSendingTo').find('option').remove().end().append('<option value="0">Select</option>');
    // $('#spItem').find('option').remove().end().append('<option value="0">Select</option>');
    // $('#spSubItem').find('option').remove().end().append('<option value="0">Select</option>');
    $('#spSendingTo').val(0);
    $('#spItem').val(0);
    $('#spSubItem').val(0);
    $("#spQty").val("");
    $("#spAmt").val("");
    $("#spSerialNumber").val("");
    var t = $('#tblpo').DataTable();
    t.clear().draw();
}