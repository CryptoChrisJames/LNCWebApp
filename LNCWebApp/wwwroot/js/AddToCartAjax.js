var cart; 

$(document).on('click', '#addToCart', function () {
    var CartData = {
        productID: $(this).attr('productid'),
        itempicture: $(this).attr('profilepicture'),
        name: $(this).attr('productname'),
        price: $(this).attr('price'),
        cartid: $(this).attr('cartid')
    }
    $.ajax({
        url: '/api/Carts/AddToCart',
        type: 'POST',
        datatype: 'json',
        data: CartData,
        complete: function (data) {
            console.log(data.data.cartid)
        }, //Calling view-cart-update function
        error: function (jqXHR, textStatus, errorThrown) {
            alert('Error: ' + textStatus + ' ' + errorThrown);
        }
    });
});