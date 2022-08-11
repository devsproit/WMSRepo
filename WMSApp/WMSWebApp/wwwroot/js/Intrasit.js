$(document).ready(function () {
    var t = $('#tblJobBoard').DataTable();
    //File Upload

    $("#ItId").change(function () {
        getSubItemId();
    });
    var getSubItemId = function () {
        //debugger;
        $.ajax({
            url: '/Intrasit/GetSubItem',//'@Url.Action("GetSubItem","Intrasit")',
            type: 'GET',
            data: {
                subItemId: $('#ItId').val(),
            },
            success: function (data) {
                //debugger;
                //$('#SubItemId').find('option').remove()
                console.log(data);
                if (data.length != 0) {
                    $(data).each(
                        function (index, item) {
                            $('#subItemId').append('<option value="' + item.ItemId + '">' + item.SubItemName + '</option>')
                        });
                } else {
                   
                    $('#subItemId').find('option').remove().end().append('<option value="0">Select</option>');
                    $('#txtMDesc').val('');
                   // $('#subItemId option:eq(0)').attr('selected', 'selected')
                }
              
            },
            error: function () {
            }
        });
    }

    $("#subItemId").change(function () {
        getMaterialDescription();
    });
    var getMaterialDescription = function () {
        //debugger;
        $.ajax({
            url: '/Intrasit/GetMaterialDesc',//@Url.Action("GetMaterialDesc","Intrasit")
            type: 'GET',
            data: {
                subItemId: $('#subItemId').val(),
            },
            success: function (data) {
                //$('#SubItemId').find('option').remove()
                // console.log(data);
                if (data.length == 0) {
                    $('#txtMDesc').val("");
                }
                else {
                    $('#txtMDesc').val(data[0].MaterialDescription);
                }
            },
            error: function () {
            }
        });
    }
    $('input[name="unit"]').keyup(function (e) {
        if (/\D/g.test(this.value)) {
            // Filter non-digits from input value.
            this.value = this.value.replace(/\D/g, '');
        }
    });
    $('input[name="amt"]').on('keypress', function (event) {
        var regex = new RegExp("^[a-zA-Z0-9]+$");
        var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
        if (!regex.test(key)) {
            event.preventDefault();
            return false;
        }
    });
    
    $('#btnaddRow').on('click', function () {
        
        var purchaseOrder = $("#txtPurchaseOrder").val();
        var senderCompany = $('#CId :selected').text();
        var senderBranch = $('#BId :selected').text();//ItId
        var itemCode = $("#ItId :selected").text();
        var subItemCode = $('#subItemId :selected').text();
        var materialDescription = $("#txtMDesc").val();
        var qty = $("#txtqty").val();
        var unit = $("#txtunit").val();
        var amt = $("#txtAmt").val();
      
      
        t.row.add([purchaseOrder, senderCompany, senderBranch, itemCode, subItemCode, materialDescription, qty, unit, amt]).draw();
    });
    $('#btnsubmit').on('click', function () {

        jsonObj = [];
        var table = $('#tblJobBoard').DataTable();
        var data = table.data();
        for (var i = 0; i < data.length; i++) {
            myData = {};
            myData["purchaseOrder"] = data[i][0];
            myData["senderCompany"] = data[i][1];//senderCompany
            myData["Branch"] = data[i][2];
            myData["itemCode"] = data[i][3];
            myData["subItemCode"] = data[i][4];//
            myData["materialDescription"] = data[i][5];
            myData["qty"] = data[i][6];//
            myData["unit"] = data[i][7];
            myData["amt"] = data[i][8];
            jsonObj.push(myData);
        }
        console.log(jsonObj);
        var params = {};
        params.intrasitc = null,
            params.intrasitcList = jsonObj,
            params.listSenderBranch = null,
            params.listSenderCompany = null,
            params.CId = 0,
            params.BId = 0,
            params.ItId = 0,
            params.SubItemId = 0

        $.ajax(
            {
                type: 'POST',
                url: '/Intrasit/Create',
                contentType: "application/json;charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(params),
                success: function (result) {
                    if (result.message)
                        alert("data inserted");
                    table.clear().draw();
                    $("#txtPurchaseOrder").val("");
                    $('#CId').val('0');
                    $('#BId').val('0');//ItId
                    $("#ItId").val('0');
                    $('#subItemId').val('0');
                    $("#txtMDesc").val("");
                    $("#txtqty").val("");
                    $("#txtunit").val("");
                    $("#txtAmt").val("");
                   // $('#txtPurchaseOrder').val(PurchaseOrder);
                },
                error: function (result) {
                    alert('error');
                }
            });
    });

  //  $("#txtAmt").on
 //   $("#txtAmt").html(Number($("#txtAmt").val()).toLocaleString('en'));
    //function ReplaceNumberWithCommas(yourNumber) {
    //    //Seperates the components of the number
    //    var components = yourNumber.toString().split(".");
    //    //Comma-fies the first part
    //    components[0] = components[0].replace(/\B(?=(\d{3})+(?!\d))/g, ",");
    //    //Combines the two sections
    //    return components.join(".");
    //}

});