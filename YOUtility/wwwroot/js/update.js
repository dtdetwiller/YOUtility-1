$("#btnUpdate").click(update_profile);

function update_profile() {
    var data = {
        address: "" + $("#address").val(),
        biography: "" + $("#biography").val(),
        username: "" + $("#username").val()
    }
    
    $.post("/Home/UpdateUser", data)
        .done(function (result) {
            console.log("Successful")
        })
        .fail(function (result) {
        })
        .always(function () {
        });
    return false;
}