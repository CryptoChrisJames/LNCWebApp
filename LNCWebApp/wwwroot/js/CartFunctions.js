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


//Jquery function initializing and handling 
//the current display of the cart.
$(document).ready(function () {
    // Get the modal
    var modal = document.getElementById('myModal');

    // Get the button that opens the modal
    var btn = document.getElementById("CartButton");

    // Get the <span> element that closes the modal
    var span = document.getElementsByClassName("close")[0];

    // When the user clicks the button, open the modal 
    btn.onclick = function () {
        modal.style.display = "block";
    }

    // When the user clicks on <span> (x), close the modal
    span.onclick = function () {
        modal.style.display = "none";
    }

    // When the user clicks anywhere outside of the modal, close it
    window.onclick = function (event) {
        if (event.target === modal) {
            modal.style.display = "none";
        }
    }

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

