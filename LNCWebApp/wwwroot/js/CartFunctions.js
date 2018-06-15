//Ajax function for adding the selected cart item to the user's cart. 
//Passes the item to the API, connected by a cart ID.
//Returns if the function was successful or not. 
$(document).on('click', '#addToCart', function () {
    var CartData = {
        productID: $(this).attr('productid'),
        itempicture: $(this).attr('profilepicture'),
        name: $(this).attr('productname'),
        price: $(this).attr('price'),
        cartid: $(this).attr('cartid')
    };
    $.ajax({
        url: '/api/Carts/AddToCart',
        type: 'POST',
        dataType: 'json',
        data: CartData,
        success: function () {
            console.log("Added to cart successfully. ")
        },
        error: function () {
            console.log("Error: item was not added to cart.")
        }
    });
});



//Ajax function for opening a dialouge box that show's the user's current. 
$(document).on('click', '#CartButton', function () {
    var cartID = GetCartbyID;
    var RenderPartial = function (data) {
        debugger;
        var view = JSON.parse(data);
        debugger;
        $("#viewCart").html(view);
    };
    $.ajax({
        type: "POST",
        dataType: 'json',
        url: '/api/Carts/ShowCartPopup',
        data: { 'cartID': cartID },
        success: function (data) {
            RenderPartial(data);
        }
    });
        
});