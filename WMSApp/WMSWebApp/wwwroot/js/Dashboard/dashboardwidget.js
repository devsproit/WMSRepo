$(document).ready(function () {
    function additionalData() {
        var data = {
            pono: "0",
        }
        addAntiForgeryToken(data);
        return data;
    }


    var initialLoad = true;
    $("#intrasit-grid").kendoGrid({

        toolbar: ["excel"],
        excel: {
            fileName: "Pending GRN.xlsx"
        },
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

    $("#stock-grid").kendoGrid({

        toolbar: ["excel"],
        excel: {
            fileName: "Stocks.xlsx"
        },
        dataSource: {

            transport: {
                read: {
                    url: "/Home/Stock",
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


        height: 350,
        pageable: {
            refresh: true,
            pageSizes: true
        },
        resizable: true,
        scrollable: true,
        columns: [{
            field: "Id",
            title: "Id",

            width: 50
        },
        {
            field: "ItemCode",
            title: "ItemCode",
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

        ],

    });


    $("#Inventorystock-grid").kendoGrid({

        toolbar: ["excel"],
        excel: {
            fileName: "Stock.xlsx",
            proxyURL: "/Home/StockList",
            filterable: true
        },
        dataSource: {

            transport: {
                read: {
                    url: "/Home/StockList",
                    type: "POST",
                    dataType: "json",
                    data: additionalData,
                    complete: function (result) {
                        console.log("Remote built-in transport", result);
                        if (result.status == 401) {
                            //document.location.href = "/AccessDenied/Index";
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
            //serverFiltering: true,
            serverSorting: true,
            requestStart: function () {
                if (initialLoad) //<-- if it's the initial load, manually start the spinner
                    kendo.ui.progress($("#Inventorystock-grid"), true);
            },
            requestEnd: function () {
                if (initialLoad)
                    kendo.ui.progress($("#Inventorystock-grid"), false);
                initialLoad = false; //<-- make sure the spinner doesn't fire again (that would produce two spinners instead of one)

            },

        },
        filterable: {
            mode: "row"    
        },
        pageable: {
            refresh: true,
            pageSizes: true
        },
        excel: {
            allPages: true
        },
        scrollable: false,
        columns: [{
            field: "Id",
            title: "ID",
            filterable: false,
            width: 40
        },
        {
            field: "ItemName",
            title: "ItemName",
            filterable: false,
            width: 100
        },
        {
            field: "ItemCode",
            title: "ItemCode",
            width: 100,
            filterable: {
                operators: {
                    string: {
                        showOperators: true
                    }
                    },                
                ui: itemCodeFilter
                }
        },
        {
            field: "Qty",
            title: "Qty",
            filterable: false,
            width: 40
        },
        {
             field: "Location",
             title: "Location",
             filterable: false,
             width: 100
        },
        {
             field: "LastUpdateDate",
             title: "Stock In Date",
             filterable: false,
             width: 80
        },
        {
             field: "Remark",
             title: "Remark",
             filterable: false,
             width: 60
        },
      ],

    });

    function itemCodeFilter(element) {        
        var gridDataSource = $("#Inventorystock-grid").data("kendoGrid").dataSource;
        element.kendoDropDownList({
            dataSource: gridDataSource.data().map(x => x['ItemCode']),
            optionLabel: "--Select Value--"
        });
    }
});


