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
    CartTotal += eval($("#ItemPrice").val());
    $("#CartTotal").html(CartTotal);
    $.ajax({
        url: '/api/Carts/AddToCart',
        type: 'POST',
        dataType: 'json',
        data: CartData,
        success: function () {
            console.log("Added to cart successfully. ");
        },
        error: function () {
            console.log("Error: item was not added to cart.");
        }
    });
});


//Ajax function for retrieving the respective cart 
//and displaying it for the user in the modal. 
$(document).on('click', '#CartButton', function () {
    var cartID = GetCartbyID;
    $.ajax({
        type: "POST",
        url: '/api/Carts/ShowCartPopup',
        data: { "cartID": cartID },
        success: function (data) {
            $("#myModalBody").html(data);
        }
    });
});


//Ajax function for removing the respective cart item
//selected by the user from the cart and updating the cart.
$(document).on('click', '#RemoveItem', function () {
    CartData = {
        cartid: $(this).attr('cartid'),
        cartitemid: $("#RemoveItem").val(),
    }
    CartTotal -= eval($("#ItemPrice").val());
    $("#CartTotal").html(CartTotal);
    $.ajax({
        type: "POST",
        url: '/api/Carts/RemoveItem',
        data: CartData,
        success: function (data) {
            $("#myModalBody").html(data);
        }
    });
});

//Ajax function for removing the entire respective cart.
$(document).on("click", "#EmptyCart", function () {
    var cartid = $(this).val();
    $.ajax({
        type: "POST",
        url: '/api/Carts/EmptyCart',
        data: { "cartid": cartid },
        success: function (data) {
            $("#myModalBody").html(data);
        }
    });
    CartTotal = 0;
    $("#CartTotal").html(CartTotal);
});

//Ajax function for removing the respective cart item
//selected by the user from the cart at checkout.
$(document).on('click', '#RemoveItemAtCheckout', function () {
    debugger;
    var CartData = {
        cartid: $(this).attr('cartid'),
        cartitemid: $(this).attr('cartitem'),
    }
    debugger;
    $.ajax({
        type: "POST",
        url: '/api/Carts/RemoveItemAtCheckout',
        data: CartData,
        success: function (data) {
            
        }
    });
});