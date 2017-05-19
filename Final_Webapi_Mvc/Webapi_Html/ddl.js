$(document).ready(function () {
    $("#selMusicCategories").on("change", productGet);
    calldropdownlist();
   
});




function productGet() {
    // Get product id from data- attribute
    //var id = $(sku).data("id");

    // Store product id in hidden field
    var id = $("#selMusicCategories").val();
    console.log("selectedCategoryID = " + id);

    var Url = 'http://localhost:1790/products/' + id;

    // Call Web API to get a list of Products
    $.ajax({
        url: Url,
        // type: 'GET',
        
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        success: function (products) {
            productListSuccess(products);

            // Change Update Button Text
            //$("#updateButton").text("Update");
            //},
            //error: function (request, message, error) {
            //    handleException(request, message, error);
            //}
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

function calldropdownlist() {

    var hostUrl = 'http://localhost:1790/category';
    // Call Web API to get a list of Product
    $.ajax({
        url: hostUrl,
        type: 'GET',
        dataType: 'json',
        success: function (parMusicCategories) {
            fillSelectElement(parMusicCategories);
        },
        error: function (request, message, error) {
            handleException(request, message, error);
        }
    });
}

function clothingdropdown(parRetrievedData) {

    console.log("cb_CallCategoriesWebService_Success function called!");
    console.dir(parRetrievedData);

    fillSelectElement(parRetrievedData);
}

function fillSelectElement(parMusicCategories) {

    console.log("fillSelectElement function called!");
    console.dir(parMusicCategories);

    var categoryID,
        categoryName;

    //Loop through filled List object and fill Select element
    for (var i = 0; i < parMusicCategories.length; i++) {

        categoryID = parMusicCategories[i].CatID;
        categoryName = parMusicCategories[i].CatName;

        //Fill select element
        var selMusicCategories = document.getElementById("selMusicCategories");
        selMusicCategories.options[i] = new Option(categoryName, categoryID);
    }
}