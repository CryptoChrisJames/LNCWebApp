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