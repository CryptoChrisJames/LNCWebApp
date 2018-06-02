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
        dataType: 'json',
        data: CartData,
        success: function (data) {
            debugger;
            alert(data);
        }
    });
});