$(function () {
    
    $(document).ready(function () {
        $("#solTitle a").click(function () {
            //Do stuff when clicked
        });
    });

    LoadData();

    $("#gridContainer").dxDataGrid({
        dataSource: [],
        showBorders: true,
        selection: {
            mode: "multiple"
        },
        filterRow: {
            visible: true
        },
        paging: {
            pageSize: 10
        },
        pager: {
            showPageSizeSelector: true,
            allowedPageSizes: [5, 10, 20],
            showInfo: true
        },
        columns: [{ caption: 'ID', dataField: 'id', alignment: 'left', visible: false },
            { caption: 'Item Quantity', dataField: 'Item_Quantity', alignment: 'left', visible: true },
            { caption: 'Item Name', dataField: 'Item_Name', alignment: 'left', visible: true },
            { caption: 'Item Customization', dataField: 'Item_Customization', alignment: 'left', visible: true },
            { caption: 'Item Price', dataField: 'Item_Price', alignment: 'left', visible: true }
        ]
    });

});
function Logout() {
    location.href = "../Restaurant/Login";
}



function SelectedRow() {
    var array = $("#gridContainer").dxDataGrid("instance").getSelectedRowsData();
    //alert(array[0]);
    var TotalCost = 0;
    for (var i = 0; i < array.length; i++) {
        TotalCost = parseFloat(TotalCost) + parseFloat(array[i].Item_Price);
    }
    var strPayment = document.getElementById("CheckOut");
    var PaymentMethod = strPayment.options[strPayment.selectedIndex].value;
    var today = new Date();
    var dd = String(today.getDate()).padStart(2, '0');
    var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
    var yyyy = today.getFullYear();
    var r = Math.random();
    var OrderNumber = dd + mm + yyyy + r.toFixed(5) + TotalCost;
    
    if (PaymentMethod === "Counter") {
        alert("Please pay  :::::::::::" + TotalCost + "$     at Counter of order " + OrderNumber+"Thankyou...............!!");
        return;
    }
    var strYear= document.getElementById("Year");
    var Year = strYear.options[strYear.selectedIndex].value;
    var Cardnumber = document.getElementById("CardDetails").value;
    var CVV = document.getElementById("CVV").value;
    alert("Your online payment of " + TotalCost + " $ was completed please wait for your order #" + OrderNumber + "Thankyou....!");
    var Result = SavePayments(Year, Cardnumber, CVV, TotalCost, OrderNumber);
    //if (Result === 1) {     
         
    //    alert("Your online payment of " + TotalCost + " was completed please wait for your order #" + OrderNumber+"Thankyou....!");
    //}
    //return array[0];s
}



function LoadData() {
    $.ajax({
        type: "POST",
        url: "/Restaurant/GetData/",
        dataType: "json",
        data: '',
        success: function (result) {
            var Response = JSON.parse(result);

            $("#gridContainer").dxDataGrid('instance').option('dataSource', Response.Table);
        },
        error: function (ex) {

        }
    });
    return Response;
}


function SavePayments(Years, Cardnumbers, CVVs, TotalCosts, OrderNumber) {
    var dataset = {
        'Year': Years, 'Cardnumber': Cardnumbers, 'CVV': CVVs, 'TotalCost': TotalCosts,
        'OrderNumber': OrderNumber
    };    
    
    $.ajax({
        url: 'PaymentDetails', 
        type: 'post',
        data: dataset,
        dataType: 'json',
        success: function (data) {
            if (data === 1) {
               
                return 1;
            }
            else {
                
                return 0;
            }
        }
    });

   
}


function ImportingData(element) {

    var filesSelected = element.files;
    var file = filesSelected[0];
    if (file !== null && file !== "") {
        var vFileExt = file.name.split('.');
        if (vFileExt[1].toUpperCase() === "XLS" || vFileExt[1].toUpperCase() === "XLSX") {

            
                var formData = new FormData();
                formData.append('file', file);
                $.ajax({
                    url: xCarrierPath + "/Restaurant/ExcelImportData",
                    data: formData,
                    type: "POST",
                    cache: false,
                    processData: false,
                    contentType: false,
                    success: function (resultXML) {
                        ImportExeclData(file.name, element.id);
                    },
                })
            
            
        }
        else {
            alertMsg("warning", "Please upload valid excel file");
            $("#loadingImage").hide();

        }

    }
}



