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
            { caption: 'Item Price', dataField: 'Item_Price', alignment: 'left', visible: true },
        ]
    });

});
function Logout() {
    location.href = "../Restaurant/Login";
}

function SelectedRow() {
    var array = $("#gridContainer").dxDataGrid("instance").getSelectedRowsData();
    alert(array[0]);
    $('#addlocationpopup').modal('show');

    //return array[0];
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