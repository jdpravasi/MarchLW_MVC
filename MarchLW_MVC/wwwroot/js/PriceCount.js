$(document).ready(function () {
    updateTotalPayable();
});

$(".ticketRow").on("change", function () {
    updateTotalPayable();
});

function updateTotalPayable() {
    var totalPayable = 0;
    $(".ticketRow").each(function () {
        var row = $(this);
        var adultCount = row.find(".adultDropdown").val();
        var childCount = row.find(".childDropdown").val();
        var adultPrice = row.find(".adultTicketPrice").val();
        var childPrice = row.find(".childticketPrice").val();
        var totalPrice = (adultCount * adultPrice) + (childCount * childPrice);
        totalPayable += totalPrice;
    });

    $("#totalPayable").text(totalPayable.toFixed(2));
}

$(".ticketRow").on("change", function () {
    var row = $(this);
    var adultCount = row.find(".adultDropdown").val();
    var childCount = row.find(".childDropdown").val();
    var adultPrice = row.find(".adultTicketPrice").val();
    var childPrice = row.find(".childticketPrice").val();

    var totalPrice = (adultCount * adultPrice) + (childCount * childPrice);
    row.find(".totalPriceValue").text(totalPrice.toFixed(2));
});
