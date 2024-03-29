$(document).ready(function () {
    GetAllRides();
});

function GetAllRides() {
    $.ajax({
        url: '/Rides/GetAllRides',
        type: 'GET',
        dataType: 'html',
        success: function (data) {
            $('#FormBody').html(data);
        },
        error: function () {
            alert("Cannot get users");
        }
    });
}

$('#addRide').on('click', function () {
    clearBox();
    $('#rideModal').modal('show');
    $('#AddData').show();
    $('#UpdateData').hide();
    $('#rideTitle').text('Add Ride');
});


function AddUser() {
  /*  var isValid = validateForm();

    if (!isValid) {
        return;
    }*/
    var objData = {
        RideName: $('#Name').val(),
        RidePriceAdult: $('#PriceAdult').val(),
        RidePriceChild: $('#PriceChild').val()
    };

    $.ajax({
        url: '/Rides/CreateRide',
        type: 'POST',
        data: objData,
        success: function (response) {
            if (response.success) {
                alert('Ride Created Successfully');
                $('#rideModal').modal('hide');
                GetAllRides();
            } else {
                alert('Ride not Created');
            }
        },
        error: function () {
            alert("Ride not Created Successfully");
        }
    });
};


function Edit(data) {
    $.ajax({
        url: '/Rides/GetRide/' + data,
        type: 'GET',
        dataType: 'json',
        success: function (response) {
            $('#rideModal').modal('show');
            $('#ID').val(response.id);
            $('#Name').val(response.rideName);
            $('#PriceAdult').val(response.ridePriceAdult);
            $('#PriceChild').val(response.ridePriceChild);

            $('#AddData').hide();
            $('#UpdateData').show();
        },
        error: function () {
            alert("Ride not Found");
        }
    });
}
function UpdateUser() {
    var objData = {
        id: $('#ID').val(), 
        RideName: $('#Name').val(),
        RidePriceAdult: $('#PriceAdult').val(),
        RidePriceChild: $('#PriceChild').val()
    };

    $.ajax({
        url: '/Rides/UpdateRide',
        type: 'PUT',
        data: objData,
        success: function (response) {
            if (response.success) {
                alert('Ride Updated Successfully');
                $('#rideModal').modal('hide');
                GetAllRides();
            } else {
                alert('Ride not Updated');
            }
        },
        error: function () {
            alert("Ride not Updated Successfully");
        }
    });
}
function clearBox() {
    $('#ID').val('');
    $('#Name').val('');
    $('#PriceAdult').val('');
    $('#PriceChild').val('');
}

function Delete(id) {
    if (confirm("Are you sure you want to delete this ride?")) {
        $.ajax({
            url: "/Rides/DeleteRide/" + id,
            type: "DELETE",
            success: function (result) {
                alert("Ride deleted successfully");
                GetAllRides();
                // Perform any additional actions after successful deletion
            },
            error: function (xhr, status, error) {
                alert("An error occurred while deleting the ride");
                console.log(xhr.responseText);
            }
        });
    }
}



