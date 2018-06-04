var currentOrderPrice = 0;

$(document).ready(function () {
    $(currentOrderPrice).change(function () {
        $("#Price").text('$' + function () {
            anime({
                targets: '#Price',
                value: currentOrderPrice,
                round: 1
            });
        });
    });
});

