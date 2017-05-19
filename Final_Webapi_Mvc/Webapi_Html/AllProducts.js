$(document).ready(function () {
  
    productList();

});


function productList() {

    var products = 'http://localhost:1790/products';
    // Call Web API to get a list of Product
    $.ajax({
        url: products,
        type: 'GET',
        dataType: 'json',
        success: function (products) {
            productListSuccess(products);
        },
        error: function (request, message, error) {
            handleException(request, message, error);
        }
    });
}



function productListSuccess(products) {
    // Iterate over the collection of data
    $.each(products, function (index, product) {
        // Add a row to the Product table
        productAddRow(product);
    });
}

function productAddRow(product) {
    //    // Check if <tbody> tag exists, add one if not
    // if ($("#productTable tbody").length == 0) {
    $("#productTable").append("<tbody></tbody>");
    //}
    //    // Append row to <table>
    $("#productTable tbody").append(
      productBuildTableRow(product));
}

function productBuildTableRow(product) {

    var ret =
      "<tr>" +
       "<td>" + product.Name + "</td>" +
       "<td>" + product.Description + "</td>"
        + "<td>" + "$" + product.Price + "</td>"+
      "</tr>";
    return ret;
}



function handleException(request, message,
                         error) {
    var msg = "";
    msg += "Code: " + request.status + "\n";
    msg += "Text: " + request.statusText + "\n";
    if (request.responseJSON != null) {
        msg += "Message" +
            request.responseJSON.Message + "\n";
    }
    alert(msg);
}




