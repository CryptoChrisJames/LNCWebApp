$(document).on('click', '#addToCart', function () {
    $.ajax({
        url: '/Home/AddToCart',
        type: 'POST',
        dataType: 'json',
        data: {
            productID : $(this).attr('productid'),
            itempicture : $(this).attr('profilepicture'),
            name : $(this).attr('productname'),
            price : $(this).attr('price'),
        }
    });
});