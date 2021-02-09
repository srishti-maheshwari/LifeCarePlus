var ProductGrid = [];
var grid = null;
var formdata;
var freeprodlistcount = 0;
$(document).ready(function () {

    $(".preloader").hide();
    $("#IDno").focus();

    $("#IDno").focusout(function (e) {
        e.preventDefault();
        e.stopPropagation();
        var Value = $("#IDno").val();
        if (Value != "")
            GetCustomerInfo(Value);
    });

    $("#NewKitId").change(function (e) {
        var Value = $("#NewKitId").val();
        if (Value != "" && Value != 0) {
            $("#KitId_").val(Value);
            getGridDetail(Value);
        }
    });

    $("form[name=upgradeIdForm]").unbind("submit");
    $("form[name=upgradeIdForm]").bind('submit', function (e) {       
        var ListObjStr = JSON.stringify(ProductGrid);
        $("#productstring").val(ListObjStr);
        var kitID = $("#NewKitId").val();
        if (kitID <= 0)
            OpenDialog("dialogMessage", "Please select Kit.", "false");
        else
            SaveUpgradeIDForm();
    });

    $("#btnCancel").click(function () {
        resetform();
    });

    $("#OkOffer").click(function () {
        debugger;
        var promoid = $("#promoId").val();
        for (var i = 1 ; i < freeprodlistcount+1; i++)
        {
        var prodID = $('input[name="freeproduct' + i + '"]:checked').attr("value");
        var productName = $('input[name="freeproduct' + i + '"]:checked').attr("title");
        var qty = $('input[name="freeproduct' + i + '"]:checked').attr("data-qty");
        var mrp = $('input[name="freeproduct' + i + '"]:checked').attr("data-mrp");
        var dp = $('input[name="freeproduct' + i + '"]:checked').attr("data-dp");
        var rate = $('input[name="freeproduct' + i + '"]:checked').attr("data-rate");
        var tax = $('input[name="freeproduct' + i + '"]:checked').attr("data-tax");
        var barcode = $('input[name="freeproduct' + i + '"]:checked').attr("data-barcode");
        ProductGrid.push({ "Barcode": barcode, "ProductTye": "F", "PV": 0, "CV": 0, "DispStatus": "Y", "TaxPer": tax, "TaxAmt": 0, "Amount": 0, "Rate": 0, "ProductId": prodID, "ProductName": productName, "RP": 0, "DP": dp, "MRP": mrp, "BV": 0, "Quantity": qty });
    }
        if (promoid == 3)
        {
            for(var j=0;j<ProductGrid.length-1;j++)
            {
                if (ProductGrid[j].ProductTye == "P" && parseFloat(ProductGrid[j].TaxPer).toFixed(2) != parseFloat(tax).toFixed(2))
                {
                    ProductGrid[j].TaxPer = tax;
                    ProductGrid[j].Rate = ( parseFloat(ProductGrid[j].DP) * 100 / (100 + parseFloat(ProductGrid[j].TaxPer))).toFixed(2);
                    ProductGrid[j].Amount = (parseFloat(ProductGrid[j].Rate) * parseFloat(ProductGrid[j].Quantity)).toFixed(2);
                    ProductGrid[j].TaxAmt = ((parseFloat(ProductGrid[j].DP) * parseFloat(ProductGrid[j].Quantity)) - parseFloat(ProductGrid[j].Amount)).toFixed(2);
                }
            }
        }

        fillGrid();
        $("#dialogOffer").empty();
        $("#dialogOffer").dialog("close");
    });

});

function resetform() {

    $('#upgradeIdForm')[0].reset();
    ProductGrid = [];
    fillGrid();

}

function SaveUpgradeIDForm() {
    formdata = new FormData($("#upgradeIdForm")[0]);
    $(".preloader").show();
    $.ajax({
        url: '/Transaction/ActivateIdWithPackage',
        type: 'POST',
        data: formdata,
        processData: false,
        contentType: false,
        dataType: "json",
        success: function (objResult) {
            $(".preloader").hide();
            if (objResult != null) {
                if (objResult.ResponseStatus == "OK") {
                    resetform();
                    OpenDialog("dialogMessage", "Saved Successfully", "false");
                    window.location.href = "UpgradeID";
                    }
                else {
                    OpenDialog("dialogMessage", objResult.ResponseMessage, "false");
                }
            }
            $(".preloader").hide();
        }
    });
}

function GetCustomerInfo(IdNo) {
    $(".preloader").show();

    $.ajax({
        url: '/Transaction/GetCustomerKitDetail',
        type: 'POST',
        data: { "IdNo": IdNo },
        dataType: "json",
        success: function (objResult) {
            $(".preloader").hide();
            if (objResult != null) {
                if (objResult.MemberName == "") {
                    OpenDialog("dialogMessage", "Record does not exists.", "false");
                    $("#IDno").val('');
                }
                else
                {
                    $("#MemberName").val(objResult.MemberName);
                    $("#KitAmount").val(objResult.KitAmount);
                    $("#KitName").val(objResult.KitName);
                    $("#MacAdres").val(objResult.MacAdres);
                    $("#NewKitId").html("");
                    $("#NewKitId").append($("<option></option>").val(0).html("--Select Kit--"));
                    $.each(objResult.NewKitList, function (data, value) {
                        $("#NewKitId").append($("<option></option>").val(value.kitId).html(value.kitName));
                    })

                }

            }
            $(".preloader").hide();
        },
        error: function (xhr, data) {
            $(".preloader").hide();            
            console.log(xhr);
            console.log("Error:", data);
        }
    });
}

function getGridDetail(kitId)
{
    $(".preloader").show();
    $.ajax({
        url: '/Transaction/GetKitProductList',
        type: 'POST',
        data: { "kitId": kitId },
        dataType: "json",
        success: function (objResult) {

            if (objResult != null) {
                $("#KitAmount").val(objResult.KitAmount);
                $("#promoId").val(objResult.promoId);
                ProductGrid = [];
                if (objResult.objListProduct.length > 0) {
                    for (var i = 0; i < objResult.objListProduct.length; i++) {
                        ProductGrid.push({ "DiscAmt": objResult.objListProduct[i].DiscAmt, "Barcode": objResult.objListProduct[i].Barcode, "ProductTye": objResult.objListProduct[i].ProductTye, "PV": objResult.objListProduct[i].PV, "CV": objResult.objListProduct[i].CV, "DispStatus": objResult.objListProduct[i].DispStatus, "TaxPer": objResult.objListProduct[i].TaxPer, "TaxAmt": objResult.objListProduct[i].TaxAmt, "Amount": objResult.objListProduct[i].Amount, "Rate": objResult.objListProduct[i].Rate, "ProductId": objResult.objListProduct[i].IdNo, "ProductName": objResult.objListProduct[i].ProductName, "RP": objResult.objListProduct[i].RP, "DP": objResult.objListProduct[i].DP, "MRP": objResult.objListProduct[i].MRP, "BV": objResult.objListProduct[i].BV, "Quantity": objResult.objListProduct[i].Quantity });
                    }
                }
                //if (objResult.promoId == "3")
                //    $("#lblFreeProdCaption").text("Please choose any one product:");
                //else
                //    $("#lblFreeProdCaption").text("Please choose a free product from below options :");
                //$("#products").empty();
                //if (objResult.freeprodlist != "") {
                //    var selectionlist = objResult.freeprodlist.split("δ");
                //    freeprodlistcount = selectionlist.length;
                //    for (var i = 0 ; i < selectionlist.length; i++) {
                //        var plist = selectionlist[i].split(";");
                //        var ctrlname = "freeproduct" + (i + 1);
                //        $("#products").append(" <div class='col-lg-12'>");
                //        for (var j = 0 ; j < plist.length; j++) {
                //            var plistdesc = plist[j].split("~");
                //            var pqty = plistdesc[0];
                //            var pitemid = plistdesc[1];
                //            var pbarcode = plistdesc[2];
                //            var pmrp= plistdesc[3];
                //            var pdp = plistdesc[4];
                //            var ptax = plistdesc[5];
                //            var pitemname = plistdesc[6];
                //            $("#products").append("<label class='col-lg-4'><input type='radio' data-qty='" + pqty + "' title='" + pitemname + "' data-barcode='" + pbarcode + "' data-mrp='" + pmrp + "' data-dp='" + pdp + "'  data-tax='" + ptax + "' class='freeproduct' name='" + ctrlname + "' value='" + pitemid + "'>" + pitemname + "</label>");
                //        }
                //        $("#products").append(" </div>");
                //    }
                //    $("#dialogOffer").dialog({
                //        modal: true,
                //        width: "60%"
                //    });
                //}
                fillGrid();
            }
            $(".preloader").hide();
        },
        error: function (xhr, data) {
            $(".preloader").hide();
            console.log(xhr);
            console.log("Error:", data);
        }
    });
}

function fillGrid(type) {
    $("#noRecord").hide();
    if (grid != null) {
        grid.destroy();
        $('#grid').empty();
    }

    grid = $('#grid').grid({
        dataSource: ProductGrid,
        uiLibrary: 'bootstrap',
        headerFilter: true,
        columns: [
            { field: 'ProductId', title: 'Prod Id', width: 80, sortable: true, hidden: false, filterable: false },
            { field: 'ProductName', title: 'Product Name', width: 100, sortable: true, hidden: false, filterable: true },
            { field: 'MRP', title: 'MRP', width: 100, sortable: true, hidden: false, filterable: true },
            { field: 'DP', title: 'DP', width: 80, sortable: true, hidden: false, filterable: true },
            { field: 'DiscAmt', title: 'DiscAmt', width: 80, sortable: true, hidden: false, filterable: true },
            { field: 'Rate', title: 'Rate', width: 80, sortable: true, hidden: false, filterable: true },
            { field: 'TaxPer', title: 'Tax', width: 50, sortable: true, hidden: true, filterable: true },
            { field: 'RP', title: 'RP', width: 80, sortable: true, hidden: false, filterable: true },
            { field: 'BV', title: 'BV', width: 80, sortable: true, hidden: false, filterable: true },
            { field: 'Quantity', title: 'Quantity', width: 80, sortable: true, hidden: true, filterable: true },
        ],
        pager: { limit: 20, sizes: [15, 20, 35, 50, 65, 80, 95, 100] }
    });
}

function OpenDialog(dialogId, Message, isConfirmation) {
    $("#" + dialogId).empty();
    if (Message != "" || Message != null) {

        $("#" + dialogId).append('<p>' + Message + '</p>');
    }
    if (isConfirmation == "true") {
        $("#" + dialogId).dialog({
            modal: true,
            buttons: [{
                text: "Yes",
                id: "btnYes" + dialogId,
                click: function () {                    
                    IsYes = true;
                    $("#" + dialogId).dialog("close");
                }
            },
            {
                text: "No",
                id: "btnNo" + dialogId,
                click: function () {
                  IsYes = false;
                    $("#" + dialogId).dialog("close");
                }
            }]
        });
        $(".ui-dialog-titlebar-close").empty();
        $(".ui-dialog-titlebar-close").append('<i class="fa fa-close"></i>');
    }
    else {


        $("#" + dialogId).dialog({
            modal: true,
            buttons: [{
                text: "OK",
                id: "btn" + dialogId,
                click: function () {
                    $("#" + dialogId).dialog("close");
                }
            }]
        });
        $(".ui-dialog-titlebar-close").empty();
        $(".ui-dialog-titlebar-close").append('<i class="fa fa-close"></i>');
    }

}