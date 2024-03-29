
$(document).ready(function () {
});


$("#searchBox").on("keyup", function () {
    var searchString = $(this).val();
    $.get("/Tickets/Search", { searchString: searchString }, function (data) {
        $("#suggestions").empty();
        data.forEach(function (item) {
            var suggestion = $("<p class='btn btn-warning m-1'></p>").text(item.rideName);
            suggestion.on("click", function () {
                $.get("/Tickets/GenerateRow", { name: item.rideName }, function (result) {
                    $("#ticktTableBody").append(result);
                });
                $("#suggestions").empty();
            });
            $("#suggestions").append(suggestion);
        });
    });
});
$("#printButton").click(function () {
    var rideIds = [];
    $(".ticketRow").each(function () {
        var rideId = $(this).find(".rideID").val();
        rideIds.push(rideId);
    });

    var formData = {
        CustomerName: $("#nameInput").val(),
        CustomerEmail: $("#emailInput").val(),
        NumberOfAdults: parseInt($("#adultDropdown").val()),
        NumberOfChildren: parseInt($("#childDropdown").val()),
        TotalPrice: parseFloat($("#totalPayable").text()),
        RideIds: rideIds
    };

    $.ajax({
        url: "/Tickets/Create",
        type: "POST",
        data: formData,
        dataType: "json",
        success: function (response) {
            if (response.status === "Created") {
                alert("Ticket created successfully!");
                window.print();
                location.reload();
            } else {
                alert("Ticket creation failed!");
            }
        },
        error: function () {
            alert("An error occurred while sending the request.");
        },
        complete: function (xhr, status) {
        console.log("The request is complete!");
        console.log("HTTP status: " + xhr.status);
        console.log("Status: " + status);
        }
    });
});

