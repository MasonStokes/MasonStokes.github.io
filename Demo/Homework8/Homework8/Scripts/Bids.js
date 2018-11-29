
function bidRequest() {
    var input = document.getElementById("sellerName").value;
    var id = parseInt(input);
    var source = "/Bids/AllBids/" + id; //appends it to URI string.

    $.ajax({
        type: "GET",
        dataType: "json",
        url: source,
        success: itemBids,
        error: ajaxError
    });
}


function itemBids(bidList) {
    if (bidList.length == 0) {
        $("#message").empty();
        $("#message").append("No bids to display. Be the first to bid on this item.");
    }
    else {
        $("#message").empty();
        $(".bid-info").remove();
        for (var i = 0; i < bidList.length; i++) {
            $("table").append("<tr class=\"bid-info\"><td>" + bidList[i].Buyer + "</td><td>$" + Number(bidList[i].Amount).toLocaleString('en-US', { minimumFractionDigits: 2 }) + "</td></tr>");
        }
    }
}


function ajaxError() {
    alert("Error with ajax");
}


function main() {
    bidRequest(); 

    var interval = 1000 * 5;

    window.setInterval(bidRequest, interval);
}

$(document).ready(main());

//$(document).ready(function () {
//    $('#dataTable').DataTable({
//        "ajax": {
//            "url": "/Home/GetData",
//            "type": "GET",
//            "datatype": "json"
//        },
//        "columns": [
//            { "data": "Name" },
//            { "data": "Position" },
//            { "data": "Office" },
//            { "data": "Age" },
//            { "data": "Salary" }
//        ]
//    });
//}); 
